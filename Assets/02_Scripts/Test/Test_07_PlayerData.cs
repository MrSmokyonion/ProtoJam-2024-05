using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_07_PlayerData : TestBase
{
    Player player;

    public ItemType type;
    public int currentUpgrade = 0;

    private void Start()
    {
        player = GameManager.Ins.Player;
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        DataEditor.SaveItemCurrentUpgrade(type, currentUpgrade);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        player.PlayerStateSetting();
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
        player.CurrentEx += 9;
    }
}
