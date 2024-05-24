using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType : int
{
    PlayerAttack,       // �÷��̾� �⺻ ����

    EnemyPoop,               // ��
    EnemyDuck,               // ����
    EnemyCrocodile,          // �Ǿ�
    EnemyBullet,        // �Ǿ� �Ѿ�


    PlungerAttack = 100,// �վ ����
    ManHoleAttack,
    ShellAttack,
    WrenchAttack,
    CoffeeCanAttack,
    TrafficConeAttack,
}


public class Factory : Singleton<Factory>
{
    /// <summary>
    /// pool���� �����ϴ� �迭
    /// </summary>
    //ObjectPool<PooledObject>[] pools;

    PlayerAttackPool playerAttackPool;
    PlungerPool plungerPool;
    ManHolePool manHolePool;
    WrenchPool wrenchPool;

    EnemyPoopPool enemyPoopPool;
    EnemyDuckPool enemyDuckPool;
    EnemyCrocodilePool enemyCrocodilePool;
    EnemyBulletPool enemyBulletPool;

    protected override void OnPreInitalize()
    {
        base.OnPreInitalize();

        playerAttackPool = GetComponentInChildren<PlayerAttackPool>();
        plungerPool = GetComponentInChildren<PlungerPool>();
        manHolePool = GetComponentInChildren<ManHolePool>();
        wrenchPool = GetComponentInChildren<WrenchPool>();
        enemyPoopPool = GetComponentInChildren<EnemyPoopPool>();
        enemyDuckPool = GetComponentInChildren<EnemyDuckPool>();
        enemyCrocodilePool = GetComponentInChildren<EnemyCrocodilePool>();
        enemyBulletPool = GetComponentInChildren<EnemyBulletPool>();

        playerAttackPool.Initialize();
        plungerPool.Initialize();
        manHolePool.Initialize();
        wrenchPool.Initialize();
        enemyPoopPool.Initialize();
        enemyDuckPool.Initialize();
        enemyCrocodilePool.Initialize();
        enemyBulletPool.Initialize();

        //pools = new ObjectPool<PooledObject>[transform.childCount];

        //for (int i = 0; i < pools.Length; i++)
        //{
        //    System.Type type = System.Type.GetType(((PoolObjectType)i).ToString() + "Pool");
        //    System.Activator.CreateInstance(type);
        //    pools[i] = transform.GetChild(i).GetComponent<type>();
        //}
    }

    protected override void OnInitalize()
    {
        base.OnInitalize();

    //    foreach(var pool in pools)
    //    {
    //        pool.Initialize();
    //    }
    }


    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.PlayerAttack:
                result = playerAttackPool.GetObject().gameObject;
                break;
            
            case PoolObjectType.EnemyPoop:
                result = enemyPoopPool.GetObject().gameObject;
                break;
            case PoolObjectType.EnemyDuck:
                result = enemyDuckPool.GetObject().gameObject;
                break;
            case PoolObjectType.EnemyCrocodile:
                result = enemyCrocodilePool.GetObject().gameObject;
                break;
            case PoolObjectType.EnemyBullet:
                result = enemyBulletPool.GetObject().gameObject;
                break;

            case PoolObjectType.PlungerAttack:
                result = plungerPool.GetObject().gameObject;
                break;
            case PoolObjectType.ManHoleAttack:
                result = manHolePool.GetObject().gameObject;
                break;
            case PoolObjectType.WrenchAttack:
                result = wrenchPool.GetObject().gameObject;
                break;
            default:
                break;
        }

        return result;
    }

    /// <summary>
    /// ������Ʈ�� Ǯ���� �������鼭 ��ġ�� ������ �����ϴ� �����ε� �Լ�
    /// </summary>
    /// <param name="type">������ ������Ʈ Ÿ��</param>
    /// <param name="position">������ ��ġ(����)</param>
    /// <param name="angle">ȸ�� ��ų ����</param>
    /// <returns>������ ������Ʈ</returns>
    public GameObject GetObject(PoolObjectType type, Vector3 position, float angle = 0.0f)
    {
        GameObject temp = GetObject(type);
        temp.transform.position = position;
        temp.transform.Rotate(angle * Vector3.forward);
        return temp;
    }

}
