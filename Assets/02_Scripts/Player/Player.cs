using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // +++ 스텟 관련 +++ --------------------------------
    [Header("스텟 관련")]

    // 체력 관련 =========
    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHp = 100.0f;

    /// <summary>
    /// 현재 체력
    /// </summary>
    public float currentHp = 100.0f;

    public float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = value;
            if(currentHp > maxHp)
            {
                currentHp = maxHp;
            }
            onHealthChange?.Invoke(currentHp, maxHp);

            if (currentHp <= 0)
            {
                currentHp = 0;
                OnDie();        // 사망
            }
        }
    }

    /// <summary>
    /// 초당 체력 재생력
    /// </summary>
    public float regeneration = 0.0f;

    // 공방 관련 =====================
    /// <summary>
    /// 공격력
    /// </summary>
    public float attackDamage = 10.0f;

    /// <summary>
    /// 방어력
    /// </summary>
    public float defence = 0.0f;

    /// <summary>
    /// 자동공격 시간(작을수록 빨라짐)
    /// </summary>
    public float attackDelayTime = 1.0f;

    /// <summary>
    /// 회피율
    /// </summary>
    public float dodgeRate = 0.0f;

    /// <summary>
    /// 디버프 감소율
    /// </summary>
    public float debuffGuard = 0.0f;

    /// <summary>
    /// 배관 고치는 시간
    /// </summary>
    public float HealingSpeed = 1.0f;

    // 경험치 관련 -----------------------------
    /// <summary>
    /// 플레이어 레벨
    /// </summary>
    public int level = 1;

    /// <summary>
    /// 현재 경험치
    /// </summary>
    public int currentEx = 0;

    public int CurrentEx
    {
        get => currentEx;
        set
        {
            currentEx = value;
            if(currentEx >= maxEx)
            {
                // 레벨업, current 초기화, max 증가
            }
            onExChange?.Invoke(currentEx, maxEx);
        }
    }

    /// <summary>
    /// 최대 경험치
    /// </summary>
    public int maxEx = 5;   

    /// <summary>
    /// 경험치 증가량(기본 1.0)
    /// </summary>
    public float exIncreaseRate = 1.0f;

    // 이동 관련 --------------------------------

    /// <summary>
    /// 플레이어의 이동 속도
    /// </summary>
    public float moveSpeed = 10.0f;

    /// <summary>
    /// 플레이어의 이동 방향
    /// </summary>
    Vector2 dir;

    Vector2 Dir
    {
        get => dir;
        set
        {
            dir = value;
            if(dir.x != 0)
            {
                if(dir.x > 0)
                {
                    attackAxie.rotation = Quaternion.Euler(0, 0, -90.0f);
                }
                else
                {
                    attackAxie.rotation = Quaternion.Euler(0, 0, 90.0f);
                }
            }
        }
    }
    



    // +++델리게이트 관련+++ ----------------------------------

    /// <summary>
    /// 체력 값 바뀔때마다 불리는 델리게이트
    /// </summary>
    public System.Action<float, float> onHealthChange;

    /// <summary>
    /// 경험치 값 바뀔때마다 불리는 델리게이트
    /// </summary>
    public System.Action<int, int> onExChange;

    /// <summary>
    /// 레벨 값 바뀔때마다 불리는 델리게이트
    /// </summary>
    public System.Action<int> onLevelChange;

    // +++기타 자료+++ --------------------------------

    /// <summary>
    /// 공격 축
    /// </summary>
    Transform attackAxie;

    /// <summary>
    /// 기본 공격 위치
    /// </summary>
    Transform attackArea;

    // +++ 컴포넌트 관련 +++ -------------------------------------
    Rigidbody2D rb;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        attackAxie = transform.GetChild(0);
        attackArea = attackAxie.GetChild(0);
    }

    private void Start()
    {
        CurrentEx = 0;

        StartCoroutine(AutoAttack());
    }

    private void OnEnable()
    {
        playerController.onMove = OnMove;
    }

    private void OnMove(Vector2 input)
    {
        Dir = input;
    }

    void FixedUpdate()
    {
        rb.MovePosition(moveSpeed * Time.fixedDeltaTime * Dir + rb.position);
    }

    /// <summary>
    /// 일정시간 간격으로 자동공격하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator AutoAttack()
    {
        while(currentHp > 0)
        {
            yield return new WaitForSeconds(attackDelayTime);
            Attack();
        }
    }

    /// <summary>
    /// 공격 범위 안에 있는 적을 공격하기(사실 공격 투사체를 소환하는 방식)
    /// </summary>
    void Attack()
    {
        Debug.Log($"플레이어 자동 공격");
        Factory.Ins.GetObject(PoolObjectType.PlayerAttack, attackArea.position);
    }

    /// <summary>
    /// 사망시 실행하는 코드
    /// </summary>
    void OnDie()
    {

    }
    
}
