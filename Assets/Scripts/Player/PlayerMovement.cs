using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {  get; private set; }
    public GameInput inputSystem{ get; private set; }

    private PlayerCurrentState _currentState;

    [Header("Настройки движения")]
    [SerializeField] private float acceleration = 15f; // Ускорение при разгоне
    [SerializeField] private float deceleration = 20f; // Замедление при остановке
    [SerializeField] private float moveSpeed = 10f;

    private Rigidbody2D _rb;
    private Vector2 _currentVelocity;

    private static readonly HashSet<PlayerStates> BlockedStates = new()
    {
        PlayerStates.Present,
        PlayerStates.Working,
        PlayerStates.Smoking,
        PlayerStates.DrinkingWater,
        PlayerStates.DrinkingCoffee,
        PlayerStates.Microwaving,
        PlayerStates.Tired,
        PlayerStates.Eating
    };

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        inputSystem = gameObject.AddComponent<GameInput>();
        _currentState = GetComponent<PlayerCurrentState>();
    }

    private void FixedUpdate()
    {
        if (!BlockedStates.Contains(PlayerCurrentState.Instance.GetCurrentState()))
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        bool isMoving = inputVector.magnitude > 0.1f;

        // Расчет целевой скорости
        Vector2 targetVelocity = inputVector.normalized * moveSpeed;

        // Плавное изменение скорости
        _currentVelocity = Vector2.Lerp(
            _currentVelocity,
            targetVelocity,
            (isMoving ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        // Применение движения
        _rb.MovePosition(_rb.position + _currentVelocity * Time.fixedDeltaTime);

        // Обновляем состояние ТОЛЬКО при изменении
        PlayerStates newState = isMoving ? PlayerStates.Moving : PlayerStates.Idle;
        if (newState != _currentState.GetCurrentState())
        {
            _currentState.SetState(newState);
        }
    }
    public Vector3 GetPlayerScreenPosition() => Camera.main.WorldToScreenPoint(transform.position);
    public void SetBaseMoveSpeed(float speed) => moveSpeed = speed;
    public float GetBaseMoveSpeed() => moveSpeed;
}
