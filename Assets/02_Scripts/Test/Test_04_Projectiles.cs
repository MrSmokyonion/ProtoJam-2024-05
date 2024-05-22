using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Projectiles : TestBase
{
    Player player;

    public AttackSkillData.SkillType type;

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
        
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        
    }
}
