using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : PooledObject
{
    [Header("적 스텟")] [SerializeField] protected float maxHp = 10.0f;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float damage = 1;

    protected float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = value;
            if (!(currentHp > 0))
            {
                OnDie();
            }
        }
    }

    [SerializeField] protected float speed;
    public int experience;

    protected Vector2 dir;

    protected virtual Vector2 Dir
    {
        get => dir;
        set { dir = value; }
    }

    /// <summary>
    /// Enemy가 사망할때 불리는 델리게이트
    /// </summary>
    public System.Action onDie;

    readonly int Hash_IsDead = Animator.StringToHash("IsDead");

    protected Rigidbody2D rb;
    protected Animator animator;
    Collider2D col;

    protected Player player;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        animator.writeDefaultValuesOnDisable = true;
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        OnInitalized();
    }


    protected virtual void OnInitalized()
    {
        if (player == null)
        {
            player = GameManager.Ins.Player;
        }

        CurrentHp = maxHp;
        gameObject.layer = 7;
        col.enabled = true;
        EnemyManager.Ins.RegisterEnemy(transform);
    }

    /// <summary>
    /// 체력 스텟을 결정하는 함수(기본값 1이며 배수로 증가함 ex) 1.2입력시 1.2배 체력 증가)
    /// </summary>
    /// <param name="hp"></param>
    public void SettingState(float hpRate)
    {
        CurrentHp = maxHp * hpRate;
    }

    private void FixedUpdate()
    {
        OnMoveUpdate(Time.fixedDeltaTime);
    }

    protected virtual void OnMoveUpdate(float time)
    {
        if (player != null && player.IsAlive && CurrentHp > 0)
        {
            Dir = (player.transform.position - transform.position).normalized;
            transform.Translate(speed * time * Dir);
        }
        // rb.MovePosition(rb.position + speed * time * Dir);
    }

    public void OnHitted(float damage)
    {
        // Debug.Log($"{name} : 플레이어에게 {damage}를 받았다!");
        CurrentHp -= damage;
        SoundManager.instance.PlaySFX(SoundManager.SOUND_LIST.SFX_ENEMY_HIT);
    }

    /// <summary>
    /// 넉백 공격이 포함된 공격
    /// </summary>
    /// <param name="damage">공격력</param>
    /// <param name="knockback">넉백 방향</param>
    public void OnHitted(float damage, Vector3 knockback)
    {
        OnHitted(damage);

        rb.AddForce(knockback * 100, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Attack(player);
        }
    }

    protected virtual void Attack(Player player)
    {
        // 플레이어에게 데미지 주기
        player.OnHitted(damage * 0.1f);
    }

    /// <summary>
    /// 플레이어에 의해 죽었을 때
    /// </summary>
    protected virtual void OnDie()
    {
        animator.SetTrigger(Hash_IsDead);
        Dir = Vector2.zero;
        col.enabled = false;
        player.CurrentEx += experience;
        EnemyManager.Ins.RemoveEnemy(transform);

        StartCoroutine(LifeOver());
    }

    protected override IEnumerator LifeOver(float delay = 0)
    {
        float playTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(playTime - 0.1f);
        gameObject.SetActive(false);
    }
}