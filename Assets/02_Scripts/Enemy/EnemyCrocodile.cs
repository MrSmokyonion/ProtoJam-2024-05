using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrocodile : EnemyBase
{
    protected override Vector2 Dir
    {
        get => dir;
        set
        {
            dir = value;
            if (dir.x > 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
    }

    [Header("사격 스텟")]
    public float attackRange = 4.0f;
    public float fireCoolTime = 2.0f;    private float currentCoolTime = 0.0f;

    Transform firePosition;

    readonly int Hash_Shoot = Animator.StringToHash("Shoot");


    protected override void Awake()
    {
        base.Awake();
        firePosition = transform.GetChild(0).GetChild(0);
    }

    protected override void OnMoveUpdate(float time)
    {
        if (player != null && player.IsAlive && CurrentHp > 0 && currentCoolTime < 0)
        {
            Vector2 vec = player.transform.position - transform.position;
            Dir = vec.normalized;
            float distance = vec.sqrMagnitude;

            if(distance < attackRange * attackRange)
            {
                FireBullet();
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.MovePosition(rb.position + speed * time * Dir);
            }
        }
        currentCoolTime -= time;
    }

    void FireBullet()
    {
        currentCoolTime = fireCoolTime;

        animator.SetTrigger(Hash_Shoot);

        Vector3 shootVector = player.transform.position - firePosition.position;

        float angle = 0;

        angle  = Vector3.SignedAngle(-firePosition.right, shootVector, transform.forward);

        Factory.Ins.GetObject(PoolObjectType.EnemyBullet, firePosition.position, angle);
    }
}
