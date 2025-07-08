using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    private PlayerCurrentState _playerCurrentState;

    [Header("Состояние персонажа")]
    [SerializeField] private float maxStressLevel = 100f;
    [SerializeField] private float stressIncreaseRate;
    [SerializeField] private float defaultStressIncreaseRate = 0.2f;
    [SerializeField] private float maxStarveLevel = 100f;
    [SerializeField] private float hungerIncreaseRate;
    [SerializeField] private float defaultHungerIncreaseRate = 0.3f;
    [SerializeField] private float maxThirstLevel = 100f;
    [SerializeField] private float thirstIncreaseRate;
    [SerializeField] private float defaultThirstIncreaseRate = 0.4f;

    [Header("Настройки скорости")]
    [SerializeField] private float minMoveSpeed = 10f;
    [SerializeField] private float stressSpeedMultiplier = 0.5f;

    [Header("Финансы")]
    [SerializeField] private float baseMoneyPerSecond = 1f;
    [SerializeField] private float maxMoneyMultiplier = 2f;

    [Header("Дополнительные эффекты")]
    [SerializeField] private float coffeeSpeedBoost = 3f; // Множитель скорости
    [SerializeField] private float coffeeEffectDuration = 15f; // Длительность в секундах
    [SerializeField] private float coffeeEffect = -5;
    [SerializeField] private float smokeEffect = -10f;
    [SerializeField] private float workingEffect = 2f;
    [SerializeField] private float drinkWaterEffect = -10f;
    [SerializeField] private float eatingEffect = -10;
    [SerializeField] private float socialEffect = -3;

    private float _coffeeEffectTimer = 0f;
    private bool _hasCoffeeEffect = false;

    private float _currentStressLevel;
    private float _currentStarveLevel;
    private float _currentThirstLevel;
    private float _currentMoney;

    private void Awake()
    {
        Instance = this;
        _currentStarveLevel = 0;
        _currentThirstLevel = 0;
        _currentStressLevel = 0;
        _currentMoney = 0;

        stressIncreaseRate = defaultStressIncreaseRate;
        hungerIncreaseRate = defaultThirstIncreaseRate;
        thirstIncreaseRate = defaultThirstIncreaseRate;

        _playerCurrentState = GetComponent<PlayerCurrentState>();
        _playerCurrentState.OnStateChanged += UpdateCurrentState;
    }

    private void Update()
    {
        UpdateStats();
        if (_playerCurrentState.GetCurrentState().Equals(PlayerStates.Working)) UpdateFinancialStatus();
    }

    private void UpdateFinancialStatus()
    {
        float stressFactor = 1 - (_currentStressLevel / maxStressLevel);
        float moneyEarned = baseMoneyPerSecond * stressFactor * Time.deltaTime;
        _currentMoney += moneyEarned;
    }

    private void UpdateCurrentState(PlayerStates newState)
    {
        switch (newState)
        {
            case PlayerStates.Smoking:
                stressIncreaseRate = smokeEffect;
                break;
            case PlayerStates.Working:
                stressIncreaseRate = workingEffect;
                break;
            case PlayerStates.DrinkingWater:
                thirstIncreaseRate = drinkWaterEffect;
                break;
            case PlayerStates.Eating:
                hungerIncreaseRate = eatingEffect;
                break;
            case PlayerStates.DrinkingCoffee:
                stressIncreaseRate = coffeeEffect;
                thirstIncreaseRate = drinkWaterEffect;
                ApplyCoffeeEffect();
                break;
            case PlayerStates.Present:
                stressIncreaseRate = workingEffect;
                break;
            case PlayerStates.Talking:
                stressIncreaseRate = socialEffect;
                break;
            default:
                stressIncreaseRate = defaultStressIncreaseRate;
                hungerIncreaseRate = defaultHungerIncreaseRate;
                thirstIncreaseRate = defaultThirstIncreaseRate;
                break;
        }
    }

    private void UpdateStats()
    {
        _currentStressLevel = Mathf.Clamp(_currentStressLevel + stressIncreaseRate * Time.deltaTime, 0f, maxStressLevel);
        _currentStarveLevel = Mathf.Clamp(_currentStarveLevel + hungerIncreaseRate * Time.deltaTime, 0f, maxStarveLevel);
        _currentThirstLevel = Mathf.Clamp(_currentThirstLevel + thirstIncreaseRate * Time.deltaTime, 0f, maxThirstLevel);

        float stressImpact = _currentStressLevel / maxStressLevel;
        float targetSpeed = Mathf.Lerp(PlayerMovement.Instance.GetBaseMoveSpeed(), minMoveSpeed, stressImpact * stressSpeedMultiplier);

        if (_hasCoffeeEffect)
        {
            float boostProgress = _coffeeEffectTimer / coffeeEffectDuration;
            float currentBoost = 1 + (coffeeSpeedBoost - 1) * boostProgress;
            PlayerMovement.Instance.SetBaseMoveSpeed(targetSpeed * currentBoost);
            baseMoneyPerSecond = 5f;
        }
        else
        {
            PlayerMovement.Instance.SetBaseMoveSpeed(targetSpeed);
            baseMoneyPerSecond = 1f;
        }
    }
    private void UpdateCoffeeEffect()
    {
        if (_hasCoffeeEffect)
        {
            _coffeeEffectTimer -= Time.deltaTime;
            //coffeeTimerUI.fillAmount = _coffeeEffectTimer / coffeeEffectDuration;
            if (_coffeeEffectTimer <= 0f)
            {
                _hasCoffeeEffect = false;
                //coffeeTimerUI.gameObject.SetActive(false);
            }
        }
    }
    public void ApplyCoffeeEffect()
    {
        _hasCoffeeEffect = true;
        _coffeeEffectTimer = coffeeEffectDuration;
        //coffeeTimerUI.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _playerCurrentState.OnStateChanged -= UpdateCurrentState;
        Destroy(gameObject);
    }

    public float GetCurrentMoney() => _currentMoney;
    public float GetStressRatio() => _currentStressLevel / maxStressLevel;
    public float GetStarveRatio() => _currentStarveLevel / maxStarveLevel;
    public float GetThirstRatio() => _currentThirstLevel / maxThirstLevel;
}
