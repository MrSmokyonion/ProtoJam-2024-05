using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05_EnemyBase : TestBase
{
    Player player;

    public AttackSkillData.SkillType type;
    public PoolObjectType enemyType;

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
        Factory.Ins.GetObject(enemyType, transform.position);
    }
}
