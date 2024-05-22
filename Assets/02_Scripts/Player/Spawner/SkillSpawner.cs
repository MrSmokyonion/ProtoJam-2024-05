using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų�� �����ϴ� �߻�Ŭ����(���������� ��ġ ����� ���⼭ �̷�����)
/// </summary>
public abstract class SkillSpawner : MonoBehaviour
{
    [SerializeField] protected AttackSkillData skillData;

    public AttackSkillData.SkillType skillType;

    /// <summary>
    /// ���� ��ų�� ����
    /// </summary>
    int spawnerLevel = 0;
    public int SpawnerLevel
    {
        get => spawnerLevel;
        private set
        {
            spawnerLevel = value;
            SetSpwanerLevel();
        }
    }


    /// <summary>
    /// ��ų �⺻ ������
    /// </summary>
    float skillDamage;

    /// <summary>
    /// ��ȭ�� ���� �߰��� ��ų ������
    /// </summary>
    protected float plusSkillDamage;

    /// <summary>
    /// ���� ������ ������(�⺻ ������ + �÷��̾� �нú� ������ ���� + �߰� ���ݷ�)
    /// </summary>
    protected float finalDamage;

    /// <summary>
    /// ��ų ���� Ƚ��
    /// </summary>
    protected int spawnCount = 1;

    /// <summary>
    /// ���� ���� �ӵ�(Player ������ ���� ������ ���� ����)
    /// </summary>
    protected float finalSpawnSpeed;

    /// <summary>
    /// �����ӵ� ���ҷ�
    /// </summary>
    protected float decreaseSpawnSpeed;

    /// <summary>
    /// ���� ����ü �����ð�(�÷��̾��ʿ��� ����޴°� ���� ������ ������ ���� �޶���)
    /// </summary>
    protected float lifeTime;

    /// <summary>
    /// ����ü ũ��(���ݹ��� ������)
    /// </summary>
    protected float additionalScale;

    /// <summary>
    /// �÷��̾�
    /// </summary>
    protected Player player;


    private void Start()
    {
        OnInitialize();
    }

    protected void OnInitialize()
    {
        if(player == null)
        {
            player = GameManager.Ins.Player;
        }
        lifeTime = skillData.LifeTime;
        decreaseSpawnSpeed = 0;
        skillType = skillData.skillType;
    }

    public virtual void SpawnSkill()
    {
        GameObject temp = Factory.Ins.GetObject(skillData.GetPoolType(), player.transform.position, player.GetFireAngle());
        temp.GetComponent<Projectile>().OnInitialize(skillData, finalDamage, lifeTime);
    }

    /// <summary>
    /// ������ ���� ���ݷ�, ����, ����ü ���ݹ����� �����ϴ� �Լ�
    /// </summary>
    public virtual void SetSpwanerLevel()
    {
        Debug.Log($"{gameObject.name} ���� �� : {SpawnerLevel}");
        switch (SpawnerLevel)
        {
            case 2:
                // 1. ���ݷ� 10�߰�
                IncreaseAttackDamage(10);
                Debug.Log("���ݷ� 10 ����");
                break;
            case 3:
                // 2. ����ü ���� ����
                IncreaseSpawnCount();
                Debug.Log("����ü ���� ����");
                break;
            case 4:
                // 3. ����ü �ð� ����
                IncreaseLifeTime(5);
                Debug.Log("����ü �ð� ����");
                break;
            case 5:
                // 4. ��Ÿ�� ����
                DecreaseAttackSpeed(50);
                Debug.Log("��Ÿ�� ����");
                break;
            case 6:
                // 5. ���ݷ� 10 �߰�
                IncreaseAttackDamage(10);
                Debug.Log("���ݷ� 10 ����");
                break;
            case 7:
                // 6. ����ü ����
                IncreaseSpawnCount();
                Debug.Log("����ü ����");
                break;
            case 8:
                // 7. ���ݷ� 20 �߰�
                IncreaseAttackDamage(20);
                Debug.Log("���ݷ� 20 �߰�");
                break;
            default:
                break;
        }
    }  

    // �ڵ����� ���� �Լ��� ==========================================

    public void StartSkillAttack()
    {
        SpawnerLevel = 1;
        StartCoroutine(StartAttack());
    }
    protected abstract IEnumerator StartAttack();

    // �ܼ� ��ġ ��� �Լ��� ===========================================

    /// <summary>
    /// ���� ���� ���� �Լ�
    /// </summary>
    public void IncreaseLevel()
    {
        SpawnerLevel++;
    }

    /// <summary>
    /// ����ü ���� �ð� ����
    /// </summary>
    /// <param name="time">������</param>
    public void IncreaseLifeTime(float time)
    {
        lifeTime += time;
    }

    /// <summary>
    /// ���� Ƚ�� ���� �Լ�
    /// </summary>
    /// <param name="count">������(�⺻ 1)</param>
    public void IncreaseSpawnCount(int count = 1)
    {
        spawnCount += count;
    }

    /// <summary>
    /// ���� ���� ���� �Լ�
    /// </summary>
    /// <param name="value">������(%�Է�)</param>
    public void IncreaseAdditionalScale(float value)
    {
        additionalScale += value;
    }

    /// <summary>
    /// ���ݷ� ���� �Լ�
    /// </summary>
    /// <param name="value">%���� ����</param>
    public void IncreaseAttackDamage(float value)
    {
        plusSkillDamage += value;
    }

    /// <summary>
    /// ���� �ӵ� ���� �Լ�
    /// </summary>
    /// <param name="value">%���� ����</param>
    public void DecreaseAttackSpeed(float value)
    {
        decreaseSpawnSpeed += skillData.FireDelay * value * 0.01f;
    }

    /// <summary>
    /// StartAttack���� �����Ҷ� ����� �Լ� 
    /// </summary>
    public void UpdateAttackSpeed()
    {
        finalSpawnSpeed = skillData.FireDelay - ((skillData.FireDelay * player.SkillCoolTimeRate) + decreaseSpawnSpeed);
        
    }

    /// <summary>
    /// StartAttack���� �����Ҷ� ����� �Լ� 
    /// </summary>
    public void UpdateAttackPower()
    {
        finalDamage = skillData.Damage + (skillData.Damage * player.AttackDamage) + plusSkillDamage;
    }


}
