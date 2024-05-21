using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 투사체 속성을 가지는 클래스 PooledObject를 상속받는다.
/// </summary>
public class Projectile : PooledObject
{

    public float damage = 10.0f;

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
    }

    /// <summary>
    /// 스폰 될때마다 실행될 초기화 함수
    /// </summary>
    protected virtual void OnInitialize()
    {
        dir = transform.right;
        currentPenetration = penetration;

        StartCoroutine(LifeOver(5.0f));     // (투사체들은 5초이상 넘기지 않는다) or 맵 밖 킬존에 죽는다
    }

    void FixedUpdate()
    {
        OnMoveUpdate(Time.fixedDeltaTime);      
    }

    /// <summary>
    /// 프레임마다 움직임을 제어하는 Update구문을 자식들이 편집할수 있게 추상화 시킨 함수
    /// </summary>
    /// <param name="time">fixedTime</param>
    protected virtual void OnMoveUpdate(float time)
    {
        transform.Translate(speed * time * dir);
    }

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

}
