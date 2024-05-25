using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05_EnemyBase : TestBase
{
    Player player;

    public AttackSkillData.SkillType type;
    public PoolObjectType enemyType;

    public Material material;
    public float brightCount = 0;
    private void Start()
    {
        player = GameManager.Ins.Player;
    }
    protected override void Test1(InputAction.CallbackContext context)
    {
        player.AddSkill(type);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        GameObject temp = Factory.Ins.GetObject(enemyType, transform.position);           // 위치 지정해서 소환
        
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        material.SetFloat("_Float", brightCount);
    }


    protected override void Test5(InputAction.CallbackContext context)
    {
        player.CurrentHp -= 100;
    }
}
