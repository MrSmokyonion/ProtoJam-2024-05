using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų�� �����ϴ� �߻�Ŭ����(���������� ��ġ ����� ���⼭ �̷�����)
/// </summary>
public abstract class SkillSpawner : MonoBehaviour
{
    [SerializeField] protected AttackSkillData skillData;

    /// <summary>
    /// ���� ��ų�� ����
    /// </summary>
    int spawnerLevel;
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
    /// �⺻ ������
    /// </summary>
    int damage;

    /// <summary>
    /// ������ ������ ������(Player ������ ���� ����)
    /// </summary>
    protected int finalDamage;

    /// <summary>
    /// �⺻ ���� ���� �ӵ�
    /// </summary>
    float attackSpeed;

    /// <summary>
    /// ���� �ӵ�(Player ������ ���� ����)
    /// </summary>
    protected float finalAttackSpeed;

    /// <summary>
    /// ����ü �����ð�
    /// </summary>
    float lifeTime;

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

        damage = skillData.damage;
        attackSpeed = skillData.FireRate;
        lifeTime = skillData.lifeTime;
    }

    public virtual void SpawnSkill()
    {
        Factory.Ins.GetObject(skillData.skillType, player.transform.position, player.GetFireAngle());
    }

    /// <summary>
    /// ������ ���� ���ݷ�, ����, ����ü ���ݹ����� �����ϴ� �Լ�
    /// </summary>
    public virtual void SetSpwanerLevel()
    {
        switch (SpawnerLevel)
        {
            case 2:
                IncreaseAttackDamage(5);
                break;
            case 3:
                IncreaseAttackDamage(5);
                IncreaseAdditionalScale(10.0f);
                break;
            case 4:
                DecreaseAttackSpeed(10.0f);
                break;
            case 5:
                IncreaseAttackDamage(10);
                break;
            default:
                break;
        }
    }  

    // �ڵ����� ���� �Լ��� ==========================================

    public void StartSkillAttack()
    {
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
    public void IncreaseAttackDamage(int value)
    {
        damage += value;
    }

    /// <summary>
    /// ���� �ӵ� ���� �Լ�
    /// </summary>
    /// <param name="value">%���� ����</param>
    public void DecreaseAttackSpeed(float value)
    {
        attackSpeed -= attackSpeed * value / 100f;
    }

    /// <summary>
    /// StartAttack���� �����Ҷ� ����� �Լ� 
    /// </summary>
    public void UpdateAttackSpeed()
    {
        finalAttackSpeed = attackSpeed * GameManager.Ins.Player.attackSpeed;
    }

    /// <summary>
    /// StartAttack���� �����Ҷ� ����� �Լ� 
    /// </summary>
    public void UpdateAttackPower()
    {
        finalDamage = (int) (damage * GameManager.Ins.Player.attackDamage * 0.01f);
    }


}
