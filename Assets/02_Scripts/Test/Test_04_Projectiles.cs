using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Projectiles : TestBase
{
    PlungerSpawner plungerSpawner;

    private void Start()
    {
        plungerSpawner = FindAnyObjectByType<PlungerSpawner>(); 
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        plungerSpawner.StartSkillAttack();
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        plungerSpawner.IncreaseLevel();
    }
}
