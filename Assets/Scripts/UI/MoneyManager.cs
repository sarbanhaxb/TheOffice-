using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [Header("UI элемент")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text salaryDebtText;
    [SerializeField] private TMP_Text officeRent;

    [Header("Настройки")]
    [SerializeField] private float initialMoney = 0f;


    private float _currentMoney;
    private float _salaryDebt = 0f; // Накопленный долг по зарплатам
    private float _officeRent = 50f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        _currentMoney = initialMoney;

        officeRent.text = _officeRent.ToString();

        UpdateMoneyUI();
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
        moneyText.text = $"Money: ${_currentMoney:F2}";
        salaryDebtText.text = $"Salary Debt: ${_salaryDebt:F2}";
        officeRent.text = $"Office rent: ${_officeRent:F2}";
        if (_salaryDebt < 0f)
        {
            salaryDebtText.color = Color.red;
        }
        else
        {
            salaryDebtText.color = Color.white;
        }
    }


    private void OnEnable()
    {
        GameTime.OnNewDay += PayDebt;
    }
    private void OnDisable()
    {
        GameTime.OnNewDay -= PayDebt;
    }

    private void PayDebt()
    {
        _currentMoney -= _salaryDebt;
        _currentMoney -= _officeRent;
        _salaryDebt = 0f;
        UpdateMoneyUI();
    }
}