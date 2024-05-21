using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill 
{
    /// <summary>
    /// 공격력
    /// </summary>
    public float damage = 10;

    /// <summary>
    /// 발사 횟수
    /// </summary>
    protected int fireCount = 0;
    public int FireCount => fireCount;

    /// <summary>
    /// 연속 발사 주기
    /// </summary>
    protected float fireRate = 0.1f;
    public float FireRate => fireRate;

    /// <summary>
    /// 다음 발사까지 걸리는 시간
    /// </summary>
    protected float fireDelay = 2.0f;
    public float FireDelay => fireDelay;    
}
