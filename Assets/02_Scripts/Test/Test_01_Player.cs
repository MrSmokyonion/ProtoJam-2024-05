using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Player : TestBase
{
    Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        
    }
}
