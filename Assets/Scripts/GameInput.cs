using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public InputSystem_Actions playerInputActions;
    public static GameInput Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        var gameInputObj = new GameObject("GameInput");
        gameInputObj.AddComponent<GameInput>();
        DontDestroyOnLoad(gameInputObj);
    }

    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Enable();
    }
    public Vector2 GetMovementVector() => playerInputActions.Player.Move.ReadValue<Vector2>();
    public Vector3 GetMousePosition() => Mouse.current.position.ReadValue();
}