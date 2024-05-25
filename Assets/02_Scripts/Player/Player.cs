using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // +++ 스텟 관련 +++ --------------------------------
    [Header("체력 스텟")]

    // 체력 관련 ======================================
    /// <summary>
    /// 최대 체력
    /// </summary>
    [SerializeField] private float maxHp = 20.0f;

    public float MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

    [Tooltip("최대로 증가할 수있는 최대 체력량")]
    /// <summary>
    /// 최대로 증가할 수있는 최대 체력량
    /// </summary>
    [SerializeField] private float plusMaxHp = 40.0f;

    /// <summary>
    /// 현재 체력
    /// </summary>
    [SerializeField] private float currentHp = 20.0f;

    /// <summary>
    /// 현재 체력 관련 프로퍼티
    /// </summary>
    public float CurrentHp
    {
        get => currentHp;
        set
        {
            if (IsAlive)
            {
                currentHp = Mathf.Min(value, MaxHp);        // 최대 체력 이상으로는 안간다.

                if (currentHp <= 0)
                {
                    currentHp = 0;
                    OnDie();        // 사망
                }

                onHealthChange?.Invoke(currentHp, maxHp);
            }
        }
    }

    public bool IsAlive => currentHp > 0;

    // 방어력 관련 ==================================================================

    //[Header("방어력 스텟")]
    ///// <summary>
    ///// 방어력(받는 피해 %으로 감소)
    ///// </summary>
    //[SerializeField] private int defence = 0;

    //[Tooltip("최대로 증가할 수 있는 최대 방어력")]
    ///// <summary>
    ///// 최대로 증가할 수 있는 최대 방어력
    ///// </summary>
    //[SerializeField] private int plusMaxDefence = 30;


    // 공격력 관련 ============================================================

    [Header("공격력 스텟")]
    /// <summary>
    /// 공격력 증가량
    /// </summary>
    [SerializeField] private int attackDamage = 0;

    /// <summary>
    /// 공격력 증가량 프로퍼티(스킬 스포너에서 공격력 계산할 때 접근한다)
    /// </summary>
    public int AttackDamage => attackDamage;

    [Tooltip("최대 증가할 수 있는 공격력 증가량(%계산)")]
    /// <summary>
    /// 최대 증가할 수 있는 공격력 증가량(%계산)
    /// </summary>
    [SerializeField] private int plusAttackMaxDamage = 100;

    // 월급 관련(경험치) ==================================================================

    [Header("월급 스텟")]

    /// <summary>
    /// 추가 월급률
    /// </summary>
    public int extraPaymentRate = 0;

    /// <summary>
    /// 추가 월급률의 최대 증가량
    /// </summary>
    [SerializeField] private int plusMaxExtraPlaymentRate = 30;

    // 체력 재생력 관련 ==================================================================

    [Header("체력 재생력 스텟")]
    /// <summary>
    /// 초당 체력 재생력
    /// </summary>
    [SerializeField] private float regeneration = 0.0f;

    /// <summary>
    /// 최대 증가할 수 있는 체력 재생력
    /// </summary>
    [SerializeField] private float plusMaxRegeneration = 4;

    /// <summary>
    /// 체력 회복 주기
    /// </summary>
    public float regenerationCoolTime = 5.0f;

    // 이동 관련 ==================================================================

    [Header("이동 스텟")]
    /// <summary>
    /// 플레이어의 기본 이동 속도(변하지 않는 값 및 기본 속도 조절용변수)
    /// </summary>
    public float moveStaticSpeed = 10.0f;

    /// <summary>
    /// 플레이어의 이동 속도
    /// </summary>
    public float moveSpeed = 1.0f;

    /// <summary>
    /// 최대 증가할수 있는 이동속도 증가량
    /// </summary>
    [SerializeField] private float plusMaxMoveSpeed = 2.0f;

    // 쿨타임 관련 ==================================================================

    [Header("쿨타임 스텟")]
    
    /// <summary>
    /// 스킬 쿨타임 감소량(%으로 계산한다)
    /// </summary>
    [SerializeField] private int skillCoolTime = 0;
    public float SkillCoolTimeRate => skillCoolTime;

    /// <summary>
    /// 최대 쿨타임 감소할수 있는 증가량
    /// </summary>
    [SerializeField] private int plusMaxSkillCoolTime = 30;

    // 0522 회의때 사라진 스텟들
    // 회피 관련 ==================================================================

    //[Header("회피 스텟")]
    ///// <summary>
    ///// 회피율
    ///// </summary>
    //[SerializeField] private int dodgeRate = 0;
    ///// <summary>
    ///// 최대 증가할수 있는 회피율 증가량
    ///// </summary>
    //[SerializeField] private int plusMaxDodgeRate = 20;

    // 공격 속도 관련 ==================================================================

    //[Header("공격 속도 스텟")]
    ///// <summary>
    ///// 같은 대상에게 대미지를 입히는 최소 시간(작을수록 빨라짐)
    ///// </summary>
    //public float attackSpeed = 1.0f;

    // 숙련도 관련 ==================================================================

    //[Header("숙련도 스텟")]
    ///// <summary>
    ///// 배관 고치는 시간
    ///// </summary>
    //public float FixingTime = 10.0f;

    ///// <summary>
    ///// 배관 고치는 시간이 최소로 줄어드는 양
    ///// </summary>
    //[SerializeField] private int plusMaxFixingTime = 5;

    // 경험치 관련 ==================================================================

    [Header("Level 관련")]
    /// <summary>
    /// 플레이어 레벨
    /// </summary>
    public int level = 1;

    /// <summary>
    /// 최대 레벨(변하지 않음)
    /// </summary>
    public const int maxLevel = 15;

    /// <summary>
    /// 현재 경험치
    /// </summary>
    public float currentEx = 0;

    public float CurrentEx
    {
        get => currentEx;
        set
        {
            if (IsAlive)
            {
                float ex = value - currentEx;       // ex는 추가되는 경험치

                currentEx = currentEx + ex + (ex * extraPaymentRate) * 0.01f;      // 경험치 추가 증가량


                if (currentEx >= maxEx)
                {
                    // 레벨업, current 초기화, max 증가

                    onLevelChange?.Invoke(level);
                }
                onExChange?.Invoke(currentEx, maxEx);
            }
        }
    }

    /// <summary>
    /// 최대 경험치(레벨에 따라 요구 수치 달라짐)
    /// </summary>
    public float maxEx = 5.0f;



    // 이동방향 관련 ==================================================================

    /// <summary>
    /// 플레이어의 이동 방향
    /// </summary>
    Vector2 dir;

    /// <summary>
    /// 플레이어가 향하고 있는 방향(8방향, zero일 경우는 없다)
    /// 투사체 생성할때 주로 참조함
    /// </summary>
    Vector2 headDir = Vector2.right;

    Vector2 Dir
    {
        get => dir;
        set
        {
            dir = value;

            animator.SetBool(Hash_IsWalk, dir != Vector2.zero);

            if(dir != Vector2.zero) 
            { 
                headDir = dir;
            }

            if(dir.x != 0)
            {
                // 방향에 따라 캐릭터 방향을 바꾸기
                if(dir.x > 0)
                {
                    
                    animator.transform.localScale = new Vector3(-1,1,1);
                    attackAxie.rotation = Quaternion.Euler(0, 0, -90.0f);
                    
                }
                else
                {
                    
                    animator.transform.localScale = new Vector3(1, 1, 1);
                    attackAxie.rotation = Quaternion.Euler(0, 0, 90.0f);
                }
            }
        }
    }

    // +++스킬 관리 관련 +++ -----------------------------------

    // 스킬들을 관리하는 딕셔너리
    Dictionary<AttackSkillData.SkillType, int> skillInventory;


    // +++델리게이트 관련+++ ----------------------------------

    /// <summary>
    /// 체력 값 바뀔때마다 불리는 델리게이트
    /// </summary>
    public System.Action<float, float> onHealthChange;

    /// <summary>
    /// 경험치 값 바뀔때마다 불리는 델리게이트
    /// </summary>
    public System.Action<float, float> onExChange;

    /// <summary>
    /// 레벨 값 바뀔때마다 불리는 델리게이트
    /// </summary>
    public System.Action<int> onLevelChange;


    // +++애니메이션 해싱 자료+++ --------------------------------

    readonly int Hash_IsDead = Animator.StringToHash("IsDead");
    readonly int Hash_IsWalk = Animator.StringToHash("IsWalk");
    readonly int Hash_IsAttack = Animator.StringToHash("IsAttack");

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
    Animator animator;

    

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        attackAxie = transform.GetChild(0);
        attackArea = attackAxie.GetChild(0);
    }

    private void Start()
    {
        OnInitialized();
        // StartCoroutine(AutoAttack());
    }

    private void OnEnable()
    {
        playerController.onMove = OnMove;
    }

    private void OnInitialized()
    {
        onLevelChange += LevelUp;

        skillInventory = new();

        PlayerStateSetting();

        CurrentHp = MaxHp;

        CurrentEx = 0;

        StartCoroutine(RegenerationHpCoroutine());
    }

    private void OnMove(Vector2 input)
    {
        Dir = input;
    }

    void FixedUpdate()
    {
        rb.MovePosition(moveSpeed * moveStaticSpeed * Time.fixedDeltaTime * Dir + rb.position);
    }

    public void AddSkill(AttackSkillData.SkillType skillType)
    {
        SkillSpawner spawner;
        switch(skillType)
        {
            default:
            case AttackSkillData.SkillType.Plunger:
                spawner = GetComponentInChildren<PlungerSpawner>();
                break;
            case AttackSkillData.SkillType.ManHole:
                spawner = GetComponentInChildren<ManHoleSpawner>();
                break;
            case AttackSkillData.SkillType.Wrench:
                spawner = GetComponentInChildren<WrenchSpawner>();
                break;
        }

        if(skillInventory.ContainsKey(skillType))
        {
            spawner.IncreaseLevel();
            skillInventory.Remove(spawner.skillType);       // 딕셔너리는 키값 바꿀려면 다시 넣어야 한다
            skillInventory.Add(spawner.skillType, spawner.SpawnerLevel);
        }
        else
        {
            skillInventory.Add(spawner.skillType, 1);
            spawner.StartSkillAttack();
        }
    }



    IEnumerator RegenerationHpCoroutine()
    {
        while(CurrentHp > 0)
        {
            yield return new WaitForSeconds(regenerationCoolTime);
            RegenerationHp();
        }
    }

    void RegenerationHp()
    {
        CurrentHp += regeneration;
    }

    public void OnHitted(float damage)
    {
        CurrentHp -= damage;
    }

    void LevelUp(int level)
    {
        if (level >= maxLevel)
        {
            Debug.Log("이미 최대 레벨입니다.");
            return;
        }
        
        this.level++;

        switch (level)
        {
            case 1:
                maxEx = 10.0f;
                break;
            case 2:
                maxEx = 13.0f;
                break;
            case 3:
                maxEx = 17.0f;
                break;
            case 4:
                maxEx = 25.0f;
                break;
            case 5:
                maxEx = 35.0f;
                break;
            case 6:
                maxEx = 47.0f;
                break;
            case 7:
                maxEx = 50.0f;
                break;
            case 8:
                maxEx = 60.0f;
                break;
            case 9:
                maxEx = 75.0f;
                break;
            case 10:
                maxEx = 90.0f;
                break;
            default:
                break;
        }

        CurrentEx = 0;
    }

    /// <summary>
    /// 플레이어의 바라보는 방향에 대한 각도를 구하는 함수
    /// </summary>
    /// <returns></returns>
    public float GetFireAngle()
    {
        float result = Vector3.SignedAngle(transform.right, headDir, transform.forward);
        return result;
    }

    /// <summary>
    /// 사망시 실행하는 코드
    /// </summary>
    void OnDie()
    {
        playerController.onMove = null;
        animator.SetTrigger(Hash_IsDead);
    }

    /// <summary>
    /// PlayerPrefs에서 데이터를 불러온다.
    /// </summary>
    public void PlayerStateSetting()
    {
        int count = System.Enum.GetValues(typeof(ItemType)).Length;
        for (int i = 0;  i < count; i++) 
        {
            ItemType type = (ItemType)i;
            int currentUpgrade = DataEditor.LoadItemCurrentUpgrade(type);

            for(int j = 0; j < currentUpgrade; j++)
            {
                UpgradeState(type);
            }
        }
        Debug.Log($"플레이어 데이터 불러오기 완료");
    }

    /// <summary>
    /// 플레이어의 스텟을 업그레이드하는 함수
    /// </summary>
    /// <param name="type">업그레이드할 타입</param>
    public void UpgradeState(ItemType type)
    {
        switch (type)
        {
            case ItemType.Health:
                MaxHp += plusMaxHp * 0.1f;
                CurrentHp += plusMaxHp * 0.1f;
                break;
            case ItemType.Damage:
                attackDamage += (int)(plusAttackMaxDamage * 0.1f);
                break;
            case ItemType.ExperienceRate:
                extraPaymentRate += (int)(plusMaxExtraPlaymentRate * 0.1f);
                break;
            case ItemType.Regeneration:
                regeneration += plusMaxRegeneration * 0.1f;
                break;
            case ItemType.Movement:
                moveSpeed += plusMaxMoveSpeed * 0.1f;
                break;
            case ItemType.CoolTime:
                skillCoolTime += (int)(plusMaxSkillCoolTime * 0.1f);
                break;
            default:
                break;
        }
    }

    

    // 기본공격 구현부분이였으나 사용 안함
    ///// <summary>
    ///// 일정시간 간격으로 자동공격하는 코루틴
    ///// </summary>
    ///// <returns></returns>
    //IEnumerator AutoAttack()
    //{
    //    while(currentHp > 0)
    //    {
    //        yield return new WaitForSeconds(attackSpeedLegacy);
    //        Attack();
    //    }
    //}

    ///// <summary>
    ///// 기본 공격하기(사실 공격 투사체를 소환하는 방식)
    ///// </summary>
    //void Attack()
    //{
    //    Debug.Log($"플레이어 자동 공격");
    //    Factory.Ins.GetObject(PoolObjectType.PlayerAttack, attackArea.position);
    //}
}
