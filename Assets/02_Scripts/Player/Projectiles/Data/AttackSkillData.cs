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


    public Parent parent;
    public PoolObjectType skillType;

    /// <summary>
    /// 공격력
    /// </summary>
    public int damage = 10;

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

    public float lifeTime = 5.0f;
}
