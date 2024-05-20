using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_03_PlayerUI : TestBase
{
    Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        player.CurrentEx++;
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        player.CurrentHp -= 10.0f;
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        player.CurrentHp += 20.0f;
    }
}
