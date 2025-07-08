using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {  get; private set; }
    public GameInput inputSystem{ get; private set; }

    private PlayerCurrentState _currentState;

    [Header("��������� ��������")]
    [SerializeField] private float acceleration = 15f; // ��������� ��� �������
    [SerializeField] private float deceleration = 20f; // ���������� ��� ���������
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

        // ������ ������� ��������
        Vector2 targetVelocity = inputVector.normalized * moveSpeed;

        // ������� ��������� ��������
        _currentVelocity = Vector2.Lerp(
            _currentVelocity,
            targetVelocity,
            (isMoving ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        // ���������� ��������
        _rb.MovePosition(_rb.position + _currentVelocity * Time.fixedDeltaTime);

        // ��������� ��������� ������ ��� ���������
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
