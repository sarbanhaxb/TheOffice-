using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [Header("UI элемент")]
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _salaryDebtText;

    [Header("Настройки")]
    [SerializeField] private float initialMoney = 100f;


    private float _currentMoney;
    private float _salaryDebt = 0f; // Накопленный долг по зарплатам

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _currentMoney = initialMoney;
        UpdateMoneyUI();
        GameTime.OnNewDay += PaySalaryDebt;

    }

    public void AddMoney(float money)
    {
        _currentMoney += money;
        UpdateMoneyUI();
    }

    public void AddSalaryDebt(float salary)
    {
        _salaryDebt += salary;
        UpdateMoneyUI();
    }

    public float CurrentMoney => _currentMoney;
    private void UpdateMoneyUI()
    {
        _moneyText.text = $"Money: ${_currentMoney:F2}";
        _salaryDebtText.text = $"Salary Debt: ${_salaryDebt:F2}";
        if (_salaryDebt < 0f)
        {
            _salaryDebtText.color = Color.red;
        }
        else
        {
            _salaryDebtText.color = Color.white;
        }
    }


    private void OnEnable()
    {
        GameTime.OnNewDay += PaySalaryDebt;
    }
    private void OnDisable()
    {
        GameTime.OnNewDay -= PaySalaryDebt;
    }

    private void PaySalaryDebt()
    {
        _currentMoney -= _salaryDebt;
        _salaryDebt = 0f;
        UpdateMoneyUI();
    }
}