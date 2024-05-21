using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill 
{
    /// <summary>
    /// ���ݷ�
    /// </summary>
    public float damage = 10;

    /// <summary>
    /// �߻� Ƚ��
    /// </summary>
    protected int fireCount = 0;
    public int FireCount => fireCount;

    /// <summary>
    /// ���� �߻� �ֱ�
    /// </summary>
    protected float fireRate = 0.1f;
    public float FireRate => fireRate;

    /// <summary>
    /// ���� �߻���� �ɸ��� �ð�
    /// </summary>
    protected float fireDelay = 2.0f;
    public float FireDelay => fireDelay;    
}
