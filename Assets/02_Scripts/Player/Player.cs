using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // +++ ���� ���� +++ --------------------------------
    [Header("ü�� ����")]

    // ü�� ���� ======================================
    /// <summary>
    /// �ִ� ü��
    /// </summary>
    [SerializeField] private float maxHp = 20.0f;

    public float MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

    [Tooltip("�ִ�� ������ ���ִ� �ִ� ü�·�")]
    /// <summary>
    /// �ִ�� ������ ���ִ� �ִ� ü�·�
    /// </summary>
    [SerializeField] private float plusMaxHp = 40.0f;

    /// <summary>
    /// ���� ü��
    /// </summary>
    [SerializeField] private float currentHp = 20.0f;

    /// <summary>
    /// ���� ü�� ���� ������Ƽ
    /// </summary>
    public float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = Mathf.Min(value, MaxHp);        // �ִ� ü�� �̻����δ� �Ȱ���.

            onHealthChange?.Invoke(currentHp, maxHp);

            if (currentHp <= 0)
            {
                currentHp = 0;
                OnDie();        // ���
            }
        }
    }

    // ���� ���� ==================================================================

    //[Header("���� ����")]
    ///// <summary>
    ///// ����(�޴� ���� %���� ����)
    ///// </summary>
    //[SerializeField] private int defence = 0;

    //[Tooltip("�ִ�� ������ �� �ִ� �ִ� ����")]
    ///// <summary>
    ///// �ִ�� ������ �� �ִ� �ִ� ����
    ///// </summary>
    //[SerializeField] private int plusMaxDefence = 30;


    // ���ݷ� ���� ============================================================

    [Header("���ݷ� ����")]
    /// <summary>
    /// ���ݷ� ������
    /// </summary>
    [SerializeField] private int attackDamage = 0;

    /// <summary>
    /// ���ݷ� ������ ������Ƽ(��ų �����ʿ��� ���ݷ� ����� �� �����Ѵ�)
    /// </summary>
    public int AttackDamage => attackDamage;

    [Tooltip("�ִ� ������ �� �ִ� ���ݷ� ������(%���)")]
    /// <summary>
    /// �ִ� ������ �� �ִ� ���ݷ� ������(%���)
    /// </summary>
    [SerializeField] private int plusAttackMaxDamage = 100;

    // ���� ����(����ġ) ==================================================================

    [Header("���� ����")]

    /// <summary>
    /// �߰� ���޷�
    /// </summary>
    public int extraPaymentRate = 0;

    /// <summary>
    /// �߰� ���޷��� �ִ� ������
    /// </summary>
    [SerializeField] private int plusMaxExtraPlaymentRate = 30;

    // ü�� ����� ���� ==================================================================

    [Header("ü�� ����� ����")]
    /// <summary>
    /// �ʴ� ü�� �����
    /// </summary>
    [SerializeField] private float regeneration = 0.0f;

    /// <summary>
    /// �ִ� ������ �� �ִ� ü�� �����
    /// </summary>
    [SerializeField] private float plusMaxRegeneration = 4;

    /// <summary>
    /// ü�� ȸ�� �ֱ�
    /// </summary>
    public float regenerationCoolTime = 5.0f;

    // �̵� ���� ==================================================================

    [Header("�̵� ����")]
    /// <summary>
    /// �÷��̾��� �⺻ �̵� �ӵ�(������ �ʴ� �� �� �⺻ �ӵ� �����뺯��)
    /// </summary>
    public float moveStaticSpeed = 10.0f;

    /// <summary>
    /// �÷��̾��� �̵� �ӵ�
    /// </summary>
    public float moveSpeed = 1.0f;

    /// <summary>
    /// �ִ� �����Ҽ� �ִ� �̵��ӵ� ������
    /// </summary>
    [SerializeField] private float plusMaxMoveSpeed = 2.0f;

    // ��Ÿ�� ���� ==================================================================

    [Header("��Ÿ�� ����")]
    
    /// <summary>
    /// ��ų ��Ÿ�� ���ҷ�(%���� ����Ѵ�)
    /// </summary>
    [SerializeField] private int skillCoolTime = 0;
    public float SkillCoolTimeRate => skillCoolTime;

    /// <summary>
    /// �ִ� ��Ÿ�� �����Ҽ� �ִ� ������
    /// </summary>
    [SerializeField] private int plusMaxSkillCoolTime = 30;

    // 0522 ȸ�Ƕ� ����� ���ݵ�
    // ȸ�� ���� ==================================================================

    //[Header("ȸ�� ����")]
    ///// <summary>
    ///// ȸ����
    ///// </summary>
    //[SerializeField] private int dodgeRate = 0;
    ///// <summary>
    ///// �ִ� �����Ҽ� �ִ� ȸ���� ������
    ///// </summary>
    //[SerializeField] private int plusMaxDodgeRate = 20;

    // ���� �ӵ� ���� ==================================================================

    //[Header("���� �ӵ� ����")]
    ///// <summary>
    ///// ���� ��󿡰� ������� ������ �ּ� �ð�(�������� ������)
    ///// </summary>
    //public float attackSpeed = 1.0f;

    // ���õ� ���� ==================================================================

    //[Header("���õ� ����")]
    ///// <summary>
    ///// ��� ��ġ�� �ð�
    ///// </summary>
    //public float FixingTime = 10.0f;

    ///// <summary>
    ///// ��� ��ġ�� �ð��� �ּҷ� �پ��� ��
    ///// </summary>
    //[SerializeField] private int plusMaxFixingTime = 5;

    // ����ġ ���� ==================================================================

    [Header("Level ����")]
    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public int level = 1;

    /// <summary>
    /// �ִ� ����(������ ����)
    /// </summary>
    public const int maxLevel = 15;

    /// <summary>
    /// ���� ����ġ
    /// </summary>
    public float currentEx = 0;

    public float CurrentEx
    {
        get => currentEx;
        set
        {

            float ex = value - currentEx;       // ex�� �߰��Ǵ� ����ġ

            currentEx = currentEx + ex + (ex * extraPaymentRate) * 0.01f;      // ����ġ �߰� ������


            if(currentEx >= maxEx)
            {
                // ������, current �ʱ�ȭ, max ����

                onLevelChange?.Invoke(level);
            }
            onExChange?.Invoke(currentEx, maxEx);
        }
    }

    /// <summary>
    /// �ִ� ����ġ(������ ���� �䱸 ��ġ �޶���)
    /// </summary>
    public float maxEx = 5.0f;



    // �̵����� ���� ==================================================================

    /// <summary>
    /// �÷��̾��� �̵� ����
    /// </summary>
    Vector2 dir;

    /// <summary>
    /// �÷��̾ ���ϰ� �ִ� ����(8����, zero�� ���� ����)
    /// ����ü �����Ҷ� �ַ� ������
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
                // ���⿡ ���� ĳ���� ������ �ٲٱ�
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

    // +++��ų ���� ���� +++ -----------------------------------

    // ��ų���� �����ϴ� ��ųʸ�
    Dictionary<AttackSkillData.SkillType, int> skillInventory;


    // +++��������Ʈ ����+++ ----------------------------------

    /// <summary>
    /// ü�� �� �ٲ𶧸��� �Ҹ��� ��������Ʈ
    /// </summary>
    public System.Action<float, float> onHealthChange;

    /// <summary>
    /// ����ġ �� �ٲ𶧸��� �Ҹ��� ��������Ʈ
    /// </summary>
    public System.Action<float, float> onExChange;

    /// <summary>
    /// ���� �� �ٲ𶧸��� �Ҹ��� ��������Ʈ
    /// </summary>
    public System.Action<int> onLevelChange;


    // +++�ִϸ��̼� �ؽ� �ڷ�+++ --------------------------------

    readonly int Hash_IsDead = Animator.StringToHash("IsDead");
    readonly int Hash_IsWalk = Animator.StringToHash("IsWalk");
    readonly int Hash_IsAttack = Animator.StringToHash("IsAttack");

    // +++��Ÿ �ڷ�+++ --------------------------------

    /// <summary>
    /// ���� ��
    /// </summary>
    Transform attackAxie;

    /// <summary>
    /// �⺻ ���� ��ġ
    /// </summary>
    Transform attackArea;

    // +++ ������Ʈ ���� +++ -------------------------------------
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
            skillInventory.Remove(spawner.skillType);       // ��ųʸ��� Ű�� �ٲܷ��� �ٽ� �־�� �Ѵ�
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

    }

    void LevelUp(int level)
    {
        if (level >= maxLevel)
        {
            Debug.Log("�̹� �ִ� �����Դϴ�.");
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
    /// �÷��̾��� �ٶ󺸴� ���⿡ ���� ������ ���ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public float GetFireAngle()
    {
        float result = Vector3.SignedAngle(transform.right, headDir, transform.forward);
        return result;
    }

    /// <summary>
    /// ����� �����ϴ� �ڵ�
    /// </summary>
    void OnDie()
    {
        playerController.onMove = null;
        animator.SetTrigger(Hash_IsDead);
    }

    /// <summary>
    /// PlayerPrefs���� �����͸� �ҷ��´�.
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
        Debug.Log($"�÷��̾� ������ �ҷ����� �Ϸ�");
    }

    /// <summary>
    /// �÷��̾��� ������ ���׷��̵��ϴ� �Լ�
    /// </summary>
    /// <param name="type">���׷��̵��� Ÿ��</param>
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

    

    // �⺻���� �����κ��̿����� ��� ����
    ///// <summary>
    ///// �����ð� �������� �ڵ������ϴ� �ڷ�ƾ
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
    ///// �⺻ �����ϱ�(��� ���� ����ü�� ��ȯ�ϴ� ���)
    ///// </summary>
    //void Attack()
    //{
    //    Debug.Log($"�÷��̾� �ڵ� ����");
    //    Factory.Ins.GetObject(PoolObjectType.PlayerAttack, attackArea.position);
    //}
}
