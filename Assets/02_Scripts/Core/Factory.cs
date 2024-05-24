using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType : int
{
    PlayerAttack,       // 플레이어 기본 공격

    EnemyPoop,               // 똥
    EnemyDuck,               // 오리
    EnemyCrocodile,          // 악어
    EnemyBullet,        // 악어 총알


    PlungerAttack = 100,// 뚫어뻥 공격
    ManHoleAttack,
    ShellAttack,
    WrenchAttack,
    CoffeeCanAttack,
    TrafficConeAttack,
}


public class Factory : Singleton<Factory>
{
    /// <summary>
    /// pool들을 관리하는 배열
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
    /// 오브젝트를 풀에서 가져오면서 위치와 각도를 설정하는 오버로딩 함수
    /// </summary>
    /// <param name="type">생성할 오브젝트 타입</param>
    /// <param name="position">생성할 위치(월드)</param>
    /// <param name="angle">회전 시킬 각도</param>
    /// <returns>생성한 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type, Vector3 position, float angle = 0.0f)
    {
        GameObject temp = GetObject(type);
        temp.transform.position = position;
        temp.transform.Rotate(angle * Vector3.forward);
        return temp;
    }

}
