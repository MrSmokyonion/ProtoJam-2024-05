using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬을 스폰하는 추상클래스(최종적으로 수치 계산은 여기서 이뤄진다)
/// </summary>
public abstract class SkillSpawner : MonoBehaviour
{
    [SerializeField] protected AttackSkillData skillData;

    public AttackSkillData.SkillType skillType;

    /// <summary>
    /// 스폰 스킬의 레벨
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
    /// 스킬 기본 데미지
    /// </summary>
    float skillDamage;

    /// <summary>
    /// 강화에 의해 추가될 스킬 데미지
    /// </summary>
    protected float plusSkillDamage;

    /// <summary>
    /// 최종 무기의 데미지(기본 데미지 + 플레이어 패시브 데미지 증가 + 추가 공격력)
    /// </summary>
    protected float finalDamage;

    /// <summary>
    /// 스킬 스폰 횟수
    /// </summary>
    protected int spawnCount = 1;

    /// <summary>
    /// 최종 스폰 속도(Player 레벨과 스폰 레벨에 따라 증가)
    /// </summary>
    protected float finalSpawnSpeed;

    /// <summary>
    /// 스폰속도 감소량
    /// </summary>
    protected float decreaseSpawnSpeed;

    /// <summary>
    /// 최종 투사체 생존시간(플레이어쪽에서 영향받는거 없고 스포너 레벨에 따라 달라짐)
    /// </summary>
    protected float lifeTime;

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
    /// 레벨에 따라 공격력, 공속, 투사체 공격범위를 결정하는 함수
    /// </summary>
    public virtual void SetSpwanerLevel()
    {
        Debug.Log($"{gameObject.name} 레벨 업 : {SpawnerLevel}");
        switch (SpawnerLevel)
        {
            case 2:
                // 1. 공격력 10추가
                IncreaseAttackDamage(10);
                Debug.Log("공격력 10 증가");
                break;
            case 3:
                // 2. 투사체 개수 증가
                IncreaseSpawnCount();
                Debug.Log("투사체 개수 증가");
                break;
            case 4:
                // 3. 투사체 시간 증가
                IncreaseLifeTime(5);
                Debug.Log("투사체 시간 증가");
                break;
            case 5:
                // 4. 쿨타임 감소
                DecreaseAttackSpeed(50);
                Debug.Log("쿨타임 감소");
                break;
            case 6:
                // 5. 공격력 10 추가
                IncreaseAttackDamage(10);
                Debug.Log("공격력 10 증가");
                break;
            case 7:
                // 6. 투사체 증가
                IncreaseSpawnCount();
                Debug.Log("투사체 증가");
                break;
            case 8:
                // 7. 공격력 20 추가
                IncreaseAttackDamage(20);
                Debug.Log("공격력 20 추가");
                break;
            default:
                break;
        }
    }  

    // 자동공격 시작 함수들 ==========================================

    public void StartSkillAttack()
    {
        SpawnerLevel = 1;
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
    /// 투사체 유지 시간 증가
    /// </summary>
    /// <param name="time">증가량</param>
    public void IncreaseLifeTime(float time)
    {
        lifeTime += time;
    }

    /// <summary>
    /// 스폰 횟수 증가 함수
    /// </summary>
    /// <param name="count">증가량(기본 1)</param>
    public void IncreaseSpawnCount(int count = 1)
    {
        spawnCount += count;
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
    public void IncreaseAttackDamage(float value)
    {
        plusSkillDamage += value;
    }

    /// <summary>
    /// 공격 속도 감소 함수
    /// </summary>
    /// <param name="value">%으로 감소</param>
    public void DecreaseAttackSpeed(float value)
    {
        decreaseSpawnSpeed += skillData.FireDelay * value * 0.01f;
    }

    /// <summary>
    /// StartAttack에서 시작할때 적용될 함수 
    /// </summary>
    public void UpdateAttackSpeed()
    {
        finalSpawnSpeed = skillData.FireDelay - ((skillData.FireDelay * player.SkillCoolTimeRate) + decreaseSpawnSpeed);
        
    }

    /// <summary>
    /// StartAttack에서 시작할때 적용될 함수 
    /// </summary>
    public void UpdateAttackPower()
    {
        finalDamage = skillData.Damage + (skillData.Damage * player.AttackDamage) + plusSkillDamage;
    }


}
