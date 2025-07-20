using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NPC_Stats : MonoBehaviour
{
    NPC_CurrentState _currentState;

    [Header("�������")]
    [SerializeField] private float baseMoneyPerSecond = 1f;
    [SerializeField] private float currentSalary = 1f;
    [SerializeField] private float minSalary = 0.5f;
    [SerializeField] private float maxSalary = 3f;
    [SerializeField] private float salaryStressModifier = 0.5f;
    [SerializeField] private float coffeeMoneyBoost = 5f; // ��������� ����� ��� ������� ����


    [Header("��������� ���������")]
    [SerializeField] private float maxStressLevel = 100f;
    [SerializeField] private float stressIncreaseRate;
    [SerializeField] private float defaultStressIncreaseRate = 0.2f;
    [SerializeField] private float maxStarveLevel = 100f;
    [SerializeField] private float hungerIncreaseRate;
    [SerializeField] private float defaultHungerIncreaseRate = 0.3f;
    [SerializeField] private float maxThirstLevel = 100f;
    [SerializeField] private float thirstIncreaseRate;
    [SerializeField] private float defaultThirstIncreaseRate = 0.4f;

    [Header("��������� ��������")]
    [SerializeField] private float minMoveSpeed = 10f;
    [SerializeField] private float maxMoveSpeed = 20f;
    [SerializeField] private float stressSpeedMultiplier = 0.5f;

    [Header("������� ��������������")]
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
        MoneyManager.Instance.AddMoney(baseMoneyPerSecond * currentSalary * stressFactor * Time.deltaTime);
    }

    private void UpdateStats()
    {
        NPCStates currentState = _currentState.GetCurrentState();
        bool isWorking = _animator.GetBool("IsWorking");
        bool isDrinkingWater = _animator.GetBool("IsDrinkWater");
        bool isSmoking = _animator.GetBool("IsSmoking");
        bool isEating = _animator.GetBool("IsEating");
        bool isDrinkingCoffee = _animator.GetBool("IsDrinkCoffee");
        bool isMeeting = _animator.GetBool("IsMeeting");

        // ���������� ��� �������� �� ���������
        stressIncreaseRate = defaultStressIncreaseRate;
        thirstIncreaseRate = defaultThirstIncreaseRate;
        hungerIncreaseRate = defaultHungerIncreaseRate;

        // ��������� ����������� �������� � ������� (��� ���� ��������, ��� ��������� ����� ������)
        float salaryStressFactor = 1 + (maxSalary - currentSalary) * salaryStressModifier;
        stressIncreaseRate *= salaryStressFactor;

        // ������������ ������ ���������
        switch (currentState)
        {
            case NPCStates.GoWorking when isWorking:
                stressIncreaseRate = 3f * salaryStressFactor; // ������ ����� ��� ������
                break;

            case NPCStates.GoDrinkWater when isDrinkingWater:
                thirstIncreaseRate = -20f; // ����� ���������
                break;

            case NPCStates.GoSmoking when isSmoking:
                stressIncreaseRate = -10f; // ������ ���������
                break;

            case NPCStates.GoEating when isEating:
                hungerIncreaseRate = -20f; // ����� ���������
                break;

            case NPCStates.GoDrinkCoffee when isDrinkingCoffee:
                stressIncreaseRate = -5f; // ������ ���������
                thirstIncreaseRate = 3f; // ����� ����� (���� ������������)
                break;
            case NPCStates.GoHome:
                _currentStarveLevel = 0;
                _currentStressLevel = 0;
                _currentThirstLevel = 0;
                break;
            case NPCStates.GoMeeting when isMeeting:
                stressIncreaseRate = -5f; //������ ���������
                break;
        }

        // ��������� ������ ������������
        _currentStressLevel = Mathf.Clamp(_currentStressLevel + stressIncreaseRate * Time.deltaTime, 0f, maxStressLevel);
        _currentStarveLevel = Mathf.Clamp(_currentStarveLevel + hungerIncreaseRate * Time.deltaTime, 0f, maxStarveLevel);
        _currentThirstLevel = Mathf.Clamp(_currentThirstLevel + thirstIncreaseRate * Time.deltaTime, 0f, maxThirstLevel);
    }

    // ������ ��� ��������� ��������
    public void DecreaseSalary()
    {
        if (currentSalary > minSalary)
        {
            currentSalary -= 0.5f;
        } 
    }
    public void IncreaseSalary()
    {
        if (currentSalary < maxSalary)
        {
            currentSalary += 0.5f;
        }
    }


    private void OnEnable()
    {
        GameTime.OnNewHour += AddCurrentSalaryDebt;
    }
    private void OnDisable()
    {
        GameTime.OnNewHour -= AddCurrentSalaryDebt;
    }

    private void AddCurrentSalaryDebt()
    {
        MoneyManager.Instance.AddSalaryDebt(currentSalary);
    }

    public float GetCurrentSalary() => currentSalary;
    public float GetStressRatio() => _currentStressLevel / maxStressLevel;
    public float GetStarveRatio() => _currentStarveLevel / maxStarveLevel;
    public float GetThirstRatio() => _currentThirstLevel / maxThirstLevel;
    public float GetCurrentStress() => _currentStressLevel;
    public float GetCurrentStarve() => _currentStarveLevel;
    public float GetCurrentThirst() => _currentThirstLevel;
}
