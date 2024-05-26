using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManHole : Projectile
{
    /// <summary>
    /// 오브젝트의 풀을 결정함(팩토리 하위에 있는 풀)
    /// </summary>
    Transform pool = null;

    /// <summary>
    /// 오브젝트의 풀은 한번만 설정됨
    /// </summary>
    public Transform Pool
    {
        set
        {
            if (pool == null)
            {
                pool = value;
            }
        }
    }

    Transform target;

    /// <summary>
    /// 풀에게 소환 끝났다고 알리는 델리게이트
    /// </summary>
    public System.Action onDone;

    const float rotateSpeed = 200.0f;

    public override void OnInitialize(AttackSkillData data, float damage, float lifeTime)
    {
        base.OnInitialize(data, damage, lifeTime);
        target = GameManager.Ins.Player.transform;
    }

    protected override void OnMoveUpdate(float time)
    {
        transform.RotateAround(target.position, new Vector3(0f, 0f, 1f), rotateSpeed * time);
        transform.Rotate(new Vector3(0, 0, -1), rotateSpeed * time);
    }

    protected override IEnumerator LifeOver(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        
        transform.SetParent(pool);
        onDone?.Invoke();
        onDone = null;

        gameObject.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            // enemy에게 데미지 주기
            enemy.OnHitted(damage);
        }
    }
}
