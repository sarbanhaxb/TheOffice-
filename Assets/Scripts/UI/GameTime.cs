using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }


    [Header("Time Settings")]
    [SerializeField] private float _realSecondsPerGameHour = 60f;
    [Tooltip("Скорости ускорения времени: 1x (нормальная), 2x, 4x, 16x")]
    [SerializeField] private float[] _timeSpeedMultipliers = { 1f, 2f, 4f, 16f }; [SerializeField] private Button _speedButton;
    [SerializeField] private TMP_Text _speedButtonText;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _dayOfWeekText;

    private DateTime _gameTime;
    private int _currentSpeedIndex = 0;
    private int _lastDisplayedMinute = -1;
    private int _lastDisplayedDay = -1;
    private bool _isEveningTriggered = false;

    public static event Action OnEveningTime;

    private void Start()
    {
        Instance = this;

        // Устанавливаем нормальную скорость по умолчанию
        Time.timeScale = 1f;
        _currentSpeedIndex = 0;

        _gameTime = new DateTime(2025, 1, 1, 8, 0, 0);
        _speedButton.onClick.AddListener(ToggleTimeSpeed);
        UpdateSpeedButtonText();
        CheckEveningTime();
        ForceUpdateAllUI();
    }

    public DateTime GetGameTime() => _gameTime;

    private void CheckEveningTime()
    {
        if (_gameTime.Hour == 17 && _gameTime.Minute == 0 && !_isEveningTriggered)
        {
            OnEveningTime?.Invoke();
            _isEveningTriggered = true;
        }
        else if (_gameTime.Hour == 0 && _gameTime.Minute == 0)
        {
            _isEveningTriggered = false;
        }
    }

    private void Update()
    {
        _gameTime = _gameTime.AddSeconds(Time.unscaledDeltaTime * (60f / _realSecondsPerGameHour) * Time.timeScale);
        UpdateUITexts();
        CheckEveningTime();
    }

    private void ToggleTimeSpeed()
    {
        _currentSpeedIndex = (_currentSpeedIndex + 1) % _timeSpeedMultipliers.Length;
        Time.timeScale = _timeSpeedMultipliers[_currentSpeedIndex];
        UpdateSpeedButtonText();
        // Можно добавить визуальную/звуковую обратную связь
        // Debug.Log($"Time scale changed to: {Time.timeScale}x");
    }

    private void UpdateSpeedButtonText()
    {
        _speedButtonText.text = $"×{Time.timeScale}";

        // Дополнительное визуальное выделение высокой скорости
        if (Time.timeScale >= 16f)
        {
            _speedButtonText.color = Color.red;
        }
        else
        {
            _speedButtonText.color = Color.white;
        }
    }

    private void UpdateUITexts()
    {
        if (_gameTime.Minute % 15 == 0 && _gameTime.Minute != _lastDisplayedMinute)
        {
            _timeText.text = _gameTime.ToString("HH:mm");
            _lastDisplayedMinute = _gameTime.Minute;
        }

        if (_gameTime.Day != _lastDisplayedDay && _gameTime.Hour == 0 && _gameTime.Minute == 0)
        {
            _dayOfWeekText.text = _gameTime.ToString("dddd");
            _lastDisplayedDay = _gameTime.Day;
        }
    }

    private void ForceUpdateAllUI()
    {
        _timeText.text = _gameTime.ToString("HH:mm");
        _dayOfWeekText.text = _gameTime.ToString("dddd");
        _lastDisplayedMinute = _gameTime.Minute;
        _lastDisplayedDay = _gameTime.Day;
    }
}