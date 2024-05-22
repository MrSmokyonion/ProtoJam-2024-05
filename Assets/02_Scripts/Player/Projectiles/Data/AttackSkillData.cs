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


    public Parent parent;
    public PoolObjectType skillType;

    /// <summary>
    /// ���ݷ�
    /// </summary>
    public int damage = 10;

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

    public float lifeTime = 5.0f;
}
