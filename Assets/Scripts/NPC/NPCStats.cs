using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NPCStats : MonoBehaviour
{
    NPC_CurrentState _currentState;

    [Header("Финансы")]
    [SerializeField] private float baseMoneyPerSecond = 1f;
    [SerializeField] private float coffeeMoneyBoost = 5f; // Множитель денег при эффекте кофе


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

    [Header("Параметры движения")]
    [SerializeField] private float minMoveSpeed = 10f;
    [SerializeField] private float maxMoveSpeed = 20f;
    [SerializeField] private float stressSpeedMultiplier = 0.5f;

    [Header("Текущие характеристики")]
    [SerializeField] private float _currentStressLevel;
    [SerializeField] private float _currentStarveLevel;
    [SerializeField] private float _currentThirstLevel;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void Awake()
    {
        _currentState = GetComponent<NPC_CurrentState>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        stressIncreaseRate = defaultStressIncreaseRate;
        hungerIncreaseRate = defaultHungerIncreaseRate;
        thirstIncreaseRate = defaultThirstIncreaseRate;
    }

    private void Update()
    {
        UpdateStats();
        if (_animator.GetBool("IsWorking")) UpdateFinancialStatus();
    }

    private void UpdateFinancialStatus()
    {
        float stressFactor = 1 - (_currentStressLevel / maxStressLevel);
        MoneyManager.Instance.AddMoney(baseMoneyPerSecond * stressFactor * Time.deltaTime);
    }

    private void UpdateStats()
    {
        NPCStates currentState = _currentState.GetCurrentState();
        bool isWorking = _animator.GetBool("IsWorking");
        bool isDrinkingWater = _animator.GetBool("IsDrinkWater");
        bool isSmoking = _animator.GetBool("IsSmoking");
        bool isEating = _animator.GetBool("IsEating");
        bool isDrinkingCoffee = _animator.GetBool("IsDrinkCoffee");

        // Сбрасываем все значения по умолчанию
        stressIncreaseRate = defaultStressIncreaseRate;
        thirstIncreaseRate = defaultThirstIncreaseRate;
        hungerIncreaseRate = defaultHungerIncreaseRate;

        // Обрабатываем каждое состояние
        switch (currentState)
        {
            case NPCStates.GoWorking when isWorking:
                stressIncreaseRate = 3f; // Стресс растёт при работе
                break;

            case NPCStates.GoDrinkWater when isDrinkingWater:
                thirstIncreaseRate = -20f; // Жажда снижается
                break;

            case NPCStates.GoSmoking when isSmoking:
                stressIncreaseRate = -10f; // Стресс снижается
                break;

            case NPCStates.GoEating when isEating:
                hungerIncreaseRate = -20f; // Голод снижается
                break;

            case NPCStates.GoDrinkCoffee when isDrinkingCoffee:
                stressIncreaseRate = -5f; // Стресс снижается
                thirstIncreaseRate = 3f; // Жажда растёт (кофе обезвоживает)
                break;
            case NPCStates.GoHome:
                _currentStarveLevel = 0;
                _currentStressLevel = 0;
                _currentThirstLevel = 0;
                break;
        }

        // Обновляем уровни потребностей
        _currentStressLevel = Mathf.Clamp(_currentStressLevel + stressIncreaseRate * Time.deltaTime, 0f, maxStressLevel);
        _currentStarveLevel = Mathf.Clamp(_currentStarveLevel + hungerIncreaseRate * Time.deltaTime, 0f, maxStarveLevel);
        _currentThirstLevel = Mathf.Clamp(_currentThirstLevel + thirstIncreaseRate * Time.deltaTime, 0f, maxThirstLevel);
    }

    public float GetStressRatio() => _currentStressLevel / maxStressLevel;
    public float GetStarveRatio() => _currentStarveLevel / maxStarveLevel;
    public float GetThirstRatio() => _currentThirstLevel / maxThirstLevel;
    public float GetCurrentStress() => _currentStressLevel;
    public float GetCurrentStarve() => _currentStarveLevel;
    public float GetCurrentThirst() => _currentThirstLevel;

}
