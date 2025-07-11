using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [Header("UI элемент")]
    [SerializeField] private TMP_Text _moneyText;
    public float _currentMoney;

    public void AddMoney(float money)
    {
        _currentMoney += money;
        UpdateMoneyUI();
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public float CurrentMoney => _currentMoney;
    private void UpdateMoneyUI()
    {
        _moneyText.text = $"${_currentMoney:F2}";
    }
}