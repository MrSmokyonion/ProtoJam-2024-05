using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬을 스폰하는 추상클래스(최종적으로 수치 계산은 여기서 이뤄진다)
/// </summary>
public abstract class SkillSpawner : MonoBehaviour
{
    [SerializeField] protected AttackSkillData skillData;

    /// <summary>
    /// 스폰 스킬의 레벨
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
    /// 기본 데미지
    /// </summary>
    int damage;

    /// <summary>
    /// 스폰될 무기의 데미지(Player 레벨에 따라 증가)
    /// </summary>
    protected int finalDamage;

    /// <summary>
    /// 기본 무기 스폰 속도
    /// </summary>
    float attackSpeed;

    /// <summary>
    /// 스폰 속도(Player 레벨에 따라 증가)
    /// </summary>
    protected float finalAttackSpeed;

    /// <summary>
    /// 투사체 생존시간
    /// </summary>
    float lifeTime;

    /// <summary>
    /// 투사체 크기(공격범위 조절용)
    /// </summary>
    protected float additionalScale;

    /// <summary>
    /// 플레이어
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
    /// 레벨에 따라 공격력, 공속, 투사체 공격범위를 결정하는 함수
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

    // 자동공격 시작 함수들 ==========================================

    public void StartSkillAttack()
    {
        StartCoroutine(StartAttack());
    }
    protected abstract IEnumerator StartAttack();

    // 단순 수치 계산 함수들 ===========================================

    /// <summary>
    /// 스폰 레벨 증가 함수
    /// </summary>
    public void IncreaseLevel()
    {
        SpawnerLevel++;
    }

    /// <summary>
    /// 공격 범위 증가 함수
    /// </summary>
    /// <param name="value">증가량(%입력)</param>
    public void IncreaseAdditionalScale(float value)
    {
        additionalScale += value;
    }

    /// <summary>
    /// 공격력 증가 함수
    /// </summary>
    /// <param name="value">%으로 증가</param>
    public void IncreaseAttackDamage(int value)
    {
        damage += value;
    }

    /// <summary>
    /// 공격 속도 감소 함수
    /// </summary>
    /// <param name="value">%으로 감소</param>
    public void DecreaseAttackSpeed(float value)
    {
        attackSpeed -= attackSpeed * value / 100f;
    }

    /// <summary>
    /// StartAttack에서 시작할때 적용될 함수 
    /// </summary>
    public void UpdateAttackSpeed()
    {
        finalAttackSpeed = attackSpeed * GameManager.Ins.Player.attackSpeed;
    }

    /// <summary>
    /// StartAttack에서 시작할때 적용될 함수 
    /// </summary>
    public void UpdateAttackPower()
    {
        finalDamage = (int) (damage * GameManager.Ins.Player.attackDamage * 0.01f);
    }


}
