using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ü �Ӽ��� ������ Ŭ���� PooledObject�� ��ӹ޴´�.
/// </summary>
public class Projectile : PooledObject
{
    AttackSkillData skillData;

    /// <summary>
    /// ������ �ִ� ���� ������
    /// </summary>
    public int damage;

    /// <summary>
    /// ����ü ������� �ð�
    /// </summary>
    protected float lifeTime;

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

        StartCoroutine(LifeOver(5.0f));     // (����ü���� 5���̻� �ѱ��� �ʴ´�) or �� �� ų���� �״´�
    }

    /// <summary>
    /// ���� �ɶ����� ����� �ʱ�ȭ �Լ�
    /// </summary>
    protected virtual void OnInitialize()
    {
        dir = transform.right;
        currentPenetration = penetration;
        lifeTime = 5.0f;
    }

    /// <summary>
    /// �����͸� �����ϴ� �Լ�(�����ϰ� �ٷ� ��Ų��)
    /// </summary>
    /// <param name="data">��ų ������</param>
    /// <param name="damage">���ݷ�</param>
    /// <param name="lifeTime">�����ð�</param>
    public void SetSkillData(AttackSkillData data, int damage, float lifeTime)
    {
        this.skillData = data;  
        this.lifeTime = lifeTime;
        this.damage = damage;
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

    protected int RandomDamage(int damage)
    {
        int min = (int)(damage * 0.8f);
        int max = (int)(damage * 1.2f);

        damage = Random.Range(min, max + 1);

        return damage;
    }

}
