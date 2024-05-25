using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 투사체 속성을 가지는 클래스 PooledObject를 상속받는다.
/// </summary>
public class Projectile : PooledObject
{
    /// <summary>
    /// 적에게 주는 최종 데미지
    /// </summary>
    protected float damage;

    /// <summary>
    /// 투사체 사라지는 시간
    /// </summary>
    protected float lifeTime;

    /// <summary>
    /// 관통횟수(0이면 관통 안함)
    /// </summary>
    private int penetration = 0;
    int currentPenetration = 0;
    
    /// <summary>
    /// 투사체의 현재 속도
    /// </summary>
    protected float currentSpeed = 5.0f;

    /// <summary>
    /// 투사체의 이동방향(기본 방향 객체 기준 오른쪽)
    /// </summary>
    protected Vector2 dir;


    protected override void OnEnable()
    {
        base.OnEnable();
    }

    /// <summary>
    /// 스폰 될때마다 실행될 초기화 함수
    /// </summary>
    public virtual void OnInitialize(AttackSkillData data, float damage, float lifeTime)
    {
        dir = transform.right;
        currentPenetration = penetration;
        this.currentSpeed = data.Speed;
        this.damage = damage;
        StartCoroutine(LifeOver(lifeTime));
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            // enemy에게 데미지 주기
            enemy.OnHitted(damage);
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
