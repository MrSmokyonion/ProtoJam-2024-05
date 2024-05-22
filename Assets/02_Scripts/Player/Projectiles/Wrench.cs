using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : Projectile
{
    List<EnemyBase> containEnemy;

    public float rotationSpeed = 200.0f;

    private void Awake()
    {
        containEnemy = new();
    }

    protected override void OnMoveUpdate(float time)
    {
        transform.Translate(currentSpeed * time * dir, Space.World);
        transform.Rotate(-Vector3.forward, rotationSpeed * time);
    }


    /// <summary>
    /// ù ��ġ �浹
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        foreach (var enemy in containEnemy)
        {
            // ���鿡�� ����� �ֱ�
        }

        containEnemy.Clear();

        StartCoroutine(LifeOver());
    }


    /// <summary>
    /// ��ġ ���� ���� �ȿ� �ִ� ����
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            containEnemy.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            containEnemy.Remove(enemy);
        }
    }
}
