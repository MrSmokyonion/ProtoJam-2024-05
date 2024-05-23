using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : Projectile
{
    List<EnemyBase> containEnemy;

    public float rotationSpeed = 200.0f;

    CircleCollider2D knockbackCol;

    private void Awake()
    {
        containEnemy = new();
        knockbackCol = GetComponent<CircleCollider2D>();
    }

    public override void OnInitialize(AttackSkillData data, float damage, float lifeTime)
    {
        base.OnInitialize(data, damage, lifeTime);
        knockbackCol.enabled = false;

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

        knockbackCol.enabled = true;

        //foreach (var enemy in containEnemy)
        //{
        //    // ���鿡�� ����� �ֱ�
        //    enemy.OnHitted(damage, dir);
        //}

        //containEnemy.Clear();

        StartCoroutine(LifeOver(0.1f));

    }


    /// <summary>
    /// ��ġ ���� ���� �ȿ� �ִ� ����
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            enemy.OnHitted(damage, dir);
        }

        //if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        //{
        //    containEnemy.Add(enemy);
        //}
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
    //    {
    //        containEnemy.Remove(enemy);
    //    }
    //}
}
