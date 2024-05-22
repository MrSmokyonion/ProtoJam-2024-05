using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 투사체 속성을 가지는 클래스 PooledObject를 상속받는다.
/// </summary>
public class Projectile : PooledObject
{
    AttackSkillData skillData;

    /// <summary>
    /// 적에게 주는 최종 데미지
    /// </summary>
    public int damage;

    /// <summary>
    /// 투사체 사라지는 시간
    /// </summary>
    protected float lifeTime;

    /// <summary>
    /// 관통횟수(0이면 관통 안함)
    /// </summary>
    public int penetration = 0;
    int currentPenetration = 0;
    
    /// <summary>
    /// 투사체의 속도
    /// </summary>
    public float speed = 5.0f;

    /// <summary>
    /// 투사체의 이동방향(기본 방향 객체 기준 오른쪽)
    /// </summary>
    protected Vector2 dir;


    protected override void OnEnable()
    {
        base.OnEnable();

        OnInitialize();

        StartCoroutine(LifeOver(5.0f));     // (투사체들은 5초이상 넘기지 않는다) or 맵 밖 킬존에 죽는다
    }

    /// <summary>
    /// 스폰 될때마다 실행될 초기화 함수
    /// </summary>
    protected virtual void OnInitialize()
    {
        dir = transform.right;
        currentPenetration = penetration;
        lifeTime = 5.0f;
    }

    /// <summary>
    /// 데이터를 세팅하는 함수(스폰하고 바로 시킨다)
    /// </summary>
    /// <param name="data">스킬 데이터</param>
    /// <param name="damage">공격력</param>
    /// <param name="lifeTime">생존시간</param>
    public void SetSkillData(AttackSkillData data, int damage, float lifeTime)
    {
        this.skillData = data;  
        this.lifeTime = lifeTime;
        this.damage = damage;
    }

    void FixedUpdate()
    {
        OnMoveUpdate(Time.fixedDeltaTime);      
    }

    /// <summary>
    /// 프레임마다 움직임을 제어하는 Update구문을 자식들이 편집할수 있게 추상화 시킨 함수
    /// </summary>
    /// <param name="time">fixedTime</param>
    protected virtual void OnMoveUpdate(float time)    {    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            // enemy에게 데미지 주기

            if (--currentPenetration < 0)
            {
                StartCoroutine(LifeOver());
            }
        }
    }

    protected int RandomDamage(int damage)
    {
        int min = (int)(damage * 0.8f);
        int max = (int)(damage * 1.2f);

        damage = Random.Range(min, max + 1);

        return damage;
    }

}
