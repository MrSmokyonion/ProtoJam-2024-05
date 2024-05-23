using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PooledObject
{
    public float speed = 5.0f;
    public float damage;

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector3.left, Space.Self);        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            // Player���� ������ �ֱ�
            StartCoroutine(LifeOver());
        }
    }
}
