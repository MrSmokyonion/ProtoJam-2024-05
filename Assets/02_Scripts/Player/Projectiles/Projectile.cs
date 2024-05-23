using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ü �Ӽ��� ������ Ŭ���� PooledObject�� ��ӹ޴´�.
/// </summary>
public class Projectile : PooledObject
{
    /// <summary>
    /// ������ �ִ� ���� ������
    /// </summary>
    private float damage;

    /// <summary>
    /// ����ü ������� �ð�
    /// </summary>
    protected float lifeTime;

    /// <summary>
    /// ����Ƚ��(0�̸� ���� ����)
    /// </summary>
    private int penetration = 0;
    int currentPenetration = 0;
    
    /// <summary>
    /// ����ü�� ���� �ӵ�
    /// </summary>
    protected float currentSpeed = 5.0f;

    /// <summary>
    /// ����ü�� �̵�����(�⺻ ���� ��ü ���� ������)
    /// </summary>
    protected Vector2 dir;


    protected override void OnEnable()
    {
        base.OnEnable();
    }

    /// <summary>
    /// ���� �ɶ����� ����� �ʱ�ȭ �Լ�
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
    /// �����Ӹ��� �������� �����ϴ� Update������ �ڽĵ��� �����Ҽ� �ְ� �߻�ȭ ��Ų �Լ�
    /// </summary>
    /// <param name="time">fixedTime</param>
    protected virtual void OnMoveUpdate(float time)    {    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            // enemy���� ������ �ֱ�
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
