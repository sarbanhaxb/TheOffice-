using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisual : MonoBehaviour, EntityVisual
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private PlayerCurrentState _currentState;

    private const string SPEED = "Speed";
    private const string MOVE_Y = "MoveY";
    private const string IS_WALKING = "IsWalking";
    private const string IS_SMOKING = "IsSmoking";
    private const string IS_PRESENT = "IsPresent";
    private const string IS_WORKING = "IsWorking";
    private const string IS_DRINKING = "IsDrinkWater";
    private const string IS_DRINKING_COFFEE = "IsDrinkingCoffee";
    private const string IS_MICROWAVING = "IsMicrowaving";
    private const string IS_TIRED = "IsTired";
    private const string IS_EATING = "IsEating";
    private const string IS_TALKING = "IsTalking";

    Vector2 lastDirection = Vector2.zero;
    private Color _originalColor;


    [Header("UI элементы")]
    [SerializeField] private Canvas UIelement;
    [SerializeField] private Image stressBar;
    [SerializeField] private Image starveBar;
    [SerializeField] private Image thirstBar;

    [Header("Позиции интерактивности")]
    [SerializeField] private GameObject presentPosition;
    [SerializeField] private GameObject workingPosition;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentState = GetComponent<PlayerCurrentState>();
        _rb = GetComponent<Rigidbody2D>();
        _currentState.OnStateChanged += UpdatePlayerStateVisual;
        _originalColor = _spriteRenderer.color;
    }

    private void FixedUpdate()
    {
        UpdatePlayerDirection();
        UpdateStatsScale();
    }

    private void UpdateStatsScale()
    {
        stressBar.fillAmount = PlayerStats.Instance.GetStressRatio();
        starveBar.fillAmount = PlayerStats.Instance.GetStarveRatio();
        thirstBar.fillAmount = PlayerStats.Instance.GetThirstRatio();
    }

    private void UpdatePlayerDirection()
    {
        var currentState = _currentState.GetCurrentState();
        if (currentState == PlayerStates.Present) return;

        var mousePos = GameInput.Instance.GetMousePosition();
        var playerPos = PlayerMovement.Instance.GetPlayerScreenPosition();

        // Обновление направления взгляда
        if (currentState == PlayerStates.Moving || currentState == PlayerStates.Idle)
        {
            _spriteRenderer.flipX = mousePos.x >= playerPos.x;
        }

        // Обновление вертикального направления
        if (currentState == PlayerStates.Moving ||
            currentState == PlayerStates.Idle ||
            currentState == PlayerStates.Smoking ||
            currentState == PlayerStates.Talking)
        {
            _animator.SetFloat("MoveY", mousePos.y < playerPos.y ? -1 : 1);
        }
    }

    private void UpdatePlayerStateVisual(PlayerStates newState)
    {
        _animator.Rebind();

        switch (newState)
        {
            case PlayerStates.Smoking:
                _animator.SetBool(IS_SMOKING, true);
                _spriteRenderer.flipX = true;
                break;
            case PlayerStates.Present:
                _animator.SetBool(IS_PRESENT, true);
                _rb.transform.position = presentPosition.transform.position;
                _spriteRenderer.flipX = true;
                break;
            case PlayerStates.Working:
                _animator.SetBool(IS_WORKING, true);
                _rb.transform.position = workingPosition.transform.position;
                _spriteRenderer.flipX = false;
                break;
            case PlayerStates.DrinkingWater:
                _animator.SetBool(IS_DRINKING, true);
                _spriteRenderer.flipX = false;
                break;
            case PlayerStates.DrinkingCoffee:
                _animator.SetBool(IS_DRINKING_COFFEE, true);
                break;
            case PlayerStates.Tired:
                _animator.SetBool(IS_TIRED, true);
                break;
            case PlayerStates.Eating:
                _animator.SetBool(IS_EATING, true);
                _spriteRenderer.flipX = false;
                break;
            case PlayerStates.Moving:
                _animator.SetFloat(SPEED, 1);
                break;
            case PlayerStates.Talking:
                _animator.SetFloat(IS_TALKING, 1);
                break;
            case PlayerStates.Idle:
                _animator.SetFloat(SPEED, 0);
                break;
        }
    }

    public void ResetState() => _currentState.SetState(PlayerStates.Idle);

    private void OnDestroy()
    {
        _currentState.OnStateChanged -= UpdatePlayerStateVisual;
        Destroy(gameObject);
    }

    public void ShowBarAnimation()
    {
        UIelement.enabled = true;
        _spriteRenderer.color = Color.green;
    }

    public void HideBarAnimation()
    {
        UIelement.enabled = false;
        _spriteRenderer.color = _originalColor;
    }
    public void SetRandom(int count) => _animator.SetFloat("Random", UnityEngine.Random.Range(0, count));

}
