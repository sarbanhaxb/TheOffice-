using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions playerInputActions;

    public static GameInput Instance { get; private set; }
    public event EventHandler OnPlayerAction;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new InputSystem_Actions();
        playerInputActions.Enable();
        playerInputActions.Player.Action.started += PlayerActionStart;
        playerInputActions.Player.ContextMenu.started += PlayerContextMenuStart;
        playerInputActions.Player.Interact.started += PlayerInteractActionAtart;
    }

    private void PlayerActionStart(InputAction.CallbackContext context) => OnPlayerAction?.Invoke(this, EventArgs.Empty);
    private void PlayerContextMenuStart(InputAction.CallbackContext context) => OnPlayerAction?.Invoke(this, EventArgs.Empty);
    private void PlayerInteractActionAtart(InputAction.CallbackContext context) => OnPlayerAction?.Invoke(this, EventArgs.Empty);
    public Vector2 GetMovementVector() => playerInputActions.Player.Move.ReadValue<Vector2>();
    public Vector3 GetMousePosition() => Mouse.current.position.ReadValue();
}
