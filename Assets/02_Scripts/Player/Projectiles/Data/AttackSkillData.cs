using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = 1)]
public class AttackSkillData : ScriptableObject
{
    /// <summary>
    /// 움직임방식을 결정하는 enum(Self면 알아서 날아가고, Player면 Player기준으로 움직인다)
    /// </summary>
    public enum Parent
    {
        Self,
        Player,
    }

    public enum SkillType
    {
        Plunger = 0,      
        ManHole,
        Shell,
        Wrench,
        CoffeeCan,
        TrafficCone,
    }

    public Parent parent;
    public SkillType skillType;

    /// <summary>
    /// 공격력
    /// </summary>
    [SerializeField] protected float damage = 10;
    public float Damage => damage;

    /// <summary>
    /// 투사체 속도
    /// </summary>
    [SerializeField] protected float speed = 5.0f;
    public float Speed => speed;

    /// <summary>
    /// 연속 발사 주기
    /// </summary>
    [SerializeField] protected float fireRate = 0.1f;
    public float FireRate => fireRate;

    /// <summary>
    /// 다음 발사까지 걸리는 시간
    /// </summary>
    [SerializeField] protected float fireDelay = 2.0f;
    public float FireDelay => fireDelay;

    /// <summary>
    /// 투사체 생존 시간
    /// </summary>
    [SerializeField] protected float lifeTime = 5.0f;
    public float LifeTime => lifeTime;  


    /// <summary>
    /// 스킬 타입을 PoolObject타입으로 변환하기 위한 함수(비효율적이므로 나중에 수정해야함)
    /// </summary>
    /// <returns></returns>
    public PoolObjectType GetPoolType()
    {
        // PoolObjectType poolObjectType = PoolObjectType.PlungerAttack;
        
        PoolObjectType poolObjectType = (PoolObjectType) (skillType + 100);
        
        //switch (skillType)
        //{
        //    case SkillType.Plunger:
        //        poolObjectType = PoolObjectType.PlungerAttack;
        //        break;
        //    case SkillType.ManHole:
        //        poolObjectType = PoolObjectType.ManHoleAttack;
        //        break;
        //    case SkillType.Shell:
        //        poolObjectType = PoolObjectType.ShellAttack;
        //        break;
        //    case SkillType.Wrench:
        //        poolObjectType = PoolObjectType.WrenchAttack;
        //        break;
        //    case SkillType.CoffeeCan:
        //        poolObjectType = PoolObjectType.CoffeeCanAttack;
        //        break;
        //    case SkillType.TrafficCone:
        //        poolObjectType = PoolObjectType.TrafficConeAttack;
        //        break;
        //}
        return poolObjectType;
    }
}
