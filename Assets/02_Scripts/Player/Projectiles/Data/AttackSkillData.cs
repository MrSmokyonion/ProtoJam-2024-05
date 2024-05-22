using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = 1)]
public class AttackSkillData : ScriptableObject
{
    /// <summary>
    /// �����ӹ���� �����ϴ� enum(Self�� �˾Ƽ� ���ư���, Player�� Player�������� �����δ�)
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
    /// ���ݷ�
    /// </summary>
    [SerializeField] protected float damage = 10;
    public float Damage => damage;

    /// <summary>
    /// ����ü �ӵ�
    /// </summary>
    [SerializeField] protected float speed = 5.0f;
    public float Speed => speed;

    /// <summary>
    /// ���� �߻� �ֱ�
    /// </summary>
    [SerializeField] protected float fireRate = 0.1f;
    public float FireRate => fireRate;

    /// <summary>
    /// ���� �߻���� �ɸ��� �ð�
    /// </summary>
    [SerializeField] protected float fireDelay = 2.0f;
    public float FireDelay => fireDelay;

    /// <summary>
    /// ����ü ���� �ð�
    /// </summary>
    [SerializeField] protected float lifeTime = 5.0f;
    public float LifeTime => lifeTime;  


    /// <summary>
    /// ��ų Ÿ���� PoolObjectŸ������ ��ȯ�ϱ� ���� �Լ�(��ȿ�����̹Ƿ� ���߿� �����ؾ���)
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
