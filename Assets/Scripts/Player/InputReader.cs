using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public event Action AimEvent;
    public event Action CoverEvent;
    public event Action EscEvent;
    public event Action KickEvent;
    public event Action MoveEvent;
    public event Action ReloadEvent;
    public event Action ShootEvent;
    public event Action TakeAllEvent;
    public event Action UseEvent;
    public event Action RunStartEvent;
    public event Action RunEndEvent;

    public Vector2 MovementValue { get; private set; }



    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }


    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AimEvent?.Invoke();
        }
        else if (context.canceled)
        {
            AimEvent?.Invoke();
        }
    }

    public void OnCover(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CoverEvent?.Invoke();
        }
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EscEvent?.Invoke();
        }
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            KickEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MovementValue = context.ReadValue<Vector2>();
            MoveEvent?.Invoke();
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReloadEvent?.Invoke();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootEvent?.Invoke();
        }
    }

    public void OnTakeAll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TakeAllEvent?.Invoke();
        }
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UseEvent?.Invoke();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RunStartEvent?.Invoke();
        }
        if (context.canceled)
        {
            RunEndEvent?.Invoke();
        }
    }

    private Controls _controls;
    public Controls Controls { get => _controls; set => _controls = value; }
}
