using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBase : MonoBehaviour
{
    PlayerInputAction action;

    private void Awake()
    {
        action = new();
        
    }

    private void OnEnable()
    {
        action.Test.Enable();
        action.Test.Test1.performed += Test1;
        action.Test.Test2.performed += Test2;
        action.Test.Test3.performed += Test3;
        action.Test.Test4.performed += Test4;
        action.Test.Test5.performed += Test5;
    }

    private void OnDisable()
    {
        action.Test.Test5.performed -= Test5;
        action.Test.Test4.performed -= Test4;
        action.Test.Test3.performed -= Test3;
        action.Test.Test2.performed -= Test2;
        action.Test.Test1.performed -= Test1;
        action.Test.Disable();
    }

    protected virtual void Test1(UnityEngine.InputSystem.InputAction.CallbackContext context) { }
    protected virtual void Test2(UnityEngine.InputSystem.InputAction.CallbackContext context) { }
    protected virtual void Test3(UnityEngine.InputSystem.InputAction.CallbackContext context) { }
    protected virtual void Test4(UnityEngine.InputSystem.InputAction.CallbackContext context) { }
    protected virtual void Test5(UnityEngine.InputSystem.InputAction.CallbackContext context) { }


}
