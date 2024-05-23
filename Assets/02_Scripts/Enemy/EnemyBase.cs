using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : PooledObject
{
    [SerializeField] protected float maxHp = 10.0f;
    [SerializeField] protected float currentHp;
    protected float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = value;
            if(!(currentHp > 0))
            {
                OnDie();
            }
        }
    }

    [SerializeField] protected float speed;
    public float experience;

    protected Vector2 dir;
    protected virtual Vector2 Dir
    {
        get => dir;
        set
        {
            dir = value;
        }
    }

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
        if(player == null)
        {
            player = GameManager.Ins.Player;
        }
        currentHp = maxHp;
        gameObject.layer = 7;
        col.enabled = true;
        
    }

    private void FixedUpdate()
    {
        OnMoveUpdate(Time.fixedDeltaTime);
    }

    protected virtual void OnMoveUpdate(float time)
    {
        if(player != null && CurrentHp > 0)
        {
            Dir = (player.transform.position - transform.position).normalized;
        }


        transform.Translate(speed * time * Dir);
        // rb.MovePosition(rb.position + speed * time * Dir);
    }

    public void OnHitted(float damage)
    {
        // Debug.Log($"{name} : 플레이어에게 {damage}를 받았다!");
        CurrentHp -= damage;
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
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            Attack(player);
        }
    }

    protected virtual void Attack(Player player)
    {
        // 플레이어에게 데미지 주기
    }

    /// <summary>
    /// 플레이어에 의해 죽었을 때
    /// </summary>
    protected virtual void OnDie()
    {
        animator.SetTrigger(Hash_IsDead);
        Dir = Vector2.zero;
        col.enabled = false;
        StartCoroutine(LifeOver());
    }

    protected override IEnumerator LifeOver(float delay = 0)
    {
        float playTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(playTime - 0.1f);
        gameObject.SetActive(false);
    }

}
