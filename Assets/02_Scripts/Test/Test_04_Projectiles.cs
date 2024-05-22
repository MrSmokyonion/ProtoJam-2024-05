using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Projectiles : TestBase
{
    PlungerSpawner plungerSpawner;
    ManHoleSpawner manHoleSpawner;

    private void Start()
    {
        plungerSpawner = FindAnyObjectByType<PlungerSpawner>();
        manHoleSpawner = FindAnyObjectByType<ManHoleSpawner>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        plungerSpawner.StartSkillAttack();
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        plungerSpawner.IncreaseLevel();
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        manHoleSpawner.StartSkillAttack();
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        manHoleSpawner.IncreaseLevel();
    }
}
