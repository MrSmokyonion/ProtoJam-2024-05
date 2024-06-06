using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_09_Store : TestBase
{

    protected override void Test1(InputAction.CallbackContext context)
    {
        // 코인 100개 씩 증가
        DataEditor.SaveMoney(500);
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
        // 스텟들 리셋
        DataEditor.DeleteAllData();
    }
}
