using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackZone : PooledObject
{
    public float damage;
    public Vector3 dir;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(0.01f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBase enemy))
        {
            enemy.OnHitted(damage, dir);
        }
    }
}
