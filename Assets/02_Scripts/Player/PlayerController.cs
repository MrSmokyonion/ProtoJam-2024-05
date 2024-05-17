using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �Է¿� �����ϴ� Ŭ����
/// </summary>
public class PlayerController : MonoBehaviour
{
    PlayerInputAction action;

    /// <summary>
    /// ����Ű ��������Ʈ
    /// </summary>
    public Action<Vector2> onMove;

    /// <summary>
    /// Ȯ��Ű ��������Ʈ
    /// </summary>
    public Action onEnter;

    /// <summary>
    /// ���Ű ��������Ʈ
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

        // �� �����̶� ���� �ڵ�, ��ư ���� �ٸ� �߰� �۾��ϸ� ����� ����
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
