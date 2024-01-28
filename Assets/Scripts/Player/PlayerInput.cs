using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour {
    PlayerInputActions _actions;

    public event Action<Vector2> OnPlayerMove;
    public event Action<bool> OnPlayerShoot;

    void OnEnable() {
        _actions = new();
        _actions.Enable();

        _actions.Movement.Move.started += OnMove;
        _actions.Movement.Move.performed += OnMove;
        _actions.Movement.Move.canceled += OnMove;

        _actions.Attack.Shoot.started += OnShoot;
        _actions.Attack.Shoot.performed += OnShoot;
        _actions.Attack.Shoot.canceled += OnShoot;
    }

    void OnDisable() {
        _actions.Disable();

        _actions.Movement.Move.started -= OnMove;
        _actions.Movement.Move.performed -= OnMove;
        _actions.Movement.Move.canceled -= OnMove;

        _actions.Attack.Shoot.started -= OnShoot;
        _actions.Attack.Shoot.performed -= OnShoot;
        _actions.Attack.Shoot.canceled -= OnShoot;
    }

    void OnMove(InputAction.CallbackContext context) {
        OnPlayerMove?.Invoke(context.ReadValue<Vector2>());
    }

    void OnShoot(InputAction.CallbackContext context) {
        OnPlayerShoot?.Invoke(context.ReadValueAsButton());
    }
}
