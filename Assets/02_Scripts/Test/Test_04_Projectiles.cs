using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Projectiles : TestBase
{

    protected override void Test1(InputAction.CallbackContext context)
    {
        Factory.Ins.GetObject(PoolObjectType.PlungerAttack);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        FindAnyObjectByType<Player>().StartPlungerAttack();
    }
}
