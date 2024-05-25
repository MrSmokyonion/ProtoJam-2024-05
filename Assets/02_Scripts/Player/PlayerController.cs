using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 입력에 관려하는 클래스
/// </summary>
public class PlayerController : MonoBehaviour
{
    PlayerInputAction action;

    /// <summary>
    /// 방향키 델리게이트
    /// </summary>
    public Action<Vector2> onMove;

    /// <summary>
    /// 확인키 델리게이트
    /// </summary>
    public Action onEnter;

    /// <summary>
    /// 취소키 델리게이트
    /// </summary>
    public Action onCancel;


    void Awake()
    {
        action = new();
    }

    private void OnEnable()
    {
        action.Player.Enable();
        action.Player.Move.performed += OnMove;
        action.Player.Move.canceled += OnMove;
        action.Player.Enter.performed += OnEnter;
        action.Player.Cancel.performed += OnCancel;
    }

    private void OnDisable()
    {
        action.Player.Cancel.performed -= OnCancel;
        action.Player.Enter.performed -= OnEnter;
        action.Player.Move.canceled -= OnMove;
        action.Player.Move.performed -= OnMove;
        action.Player.Disable();
    }
    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 result = context.ReadValue<Vector2>();

        // 위 한줄이랑 같은 코드, 버튼 뗄때 다른 추가 작업하면 사용할 예정
        //Vector2 result = Vector2.zero;
        //if (!context.canceled)
        //{
        //    result = context.ReadValue<Vector2>();
        //}

        onMove?.Invoke(result);
    }

    private void OnEnter(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        onEnter?.Invoke();
    }

    private void OnCancel(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        onCancel?.Invoke();
    }


}
