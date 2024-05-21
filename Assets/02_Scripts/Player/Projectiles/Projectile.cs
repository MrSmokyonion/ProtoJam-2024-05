using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ü �Ӽ��� ������ Ŭ���� PooledObject�� ��ӹ޴´�.
/// </summary>
public class Projectile : PooledObject
{

    public float damage = 10.0f;

    /// <summary>
    /// ����Ƚ��(0�̸� ���� ����)
    /// </summary>
    public int penetration = 0;
    int currentPenetration = 0;
    
    /// <summary>
    /// ����ü�� �ӵ�
    /// </summary>
    public float speed = 5.0f;

    /// <summary>
    /// ����ü�� �̵�����(�⺻ ���� ��ü ���� ������)
    /// </summary>
    protected Vector2 dir;


    protected override void OnEnable()
    {
        base.OnEnable();

        OnInitialize();
    }

    /// <summary>
    /// ���� �ɶ����� ����� �ʱ�ȭ �Լ�
    /// </summary>
    protected virtual void OnInitialize()
    {
        dir = transform.right;
        currentPenetration = penetration;

        StartCoroutine(LifeOver(5.0f));     // (����ü���� 5���̻� �ѱ��� �ʴ´�) or �� �� ų���� �״´�
    }

    void FixedUpdate()
    {
        OnMoveUpdate(Time.fixedDeltaTime);      
    }

    /// <summary>
    /// �����Ӹ��� �������� �����ϴ� Update������ �ڽĵ��� �����Ҽ� �ְ� �߻�ȭ ��Ų �Լ�
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
            // enemy���� ������ �ֱ�

            if (--currentPenetration < 0)
            {
                StartCoroutine(LifeOver());
            }
        }
        
    }

}
