using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }


    [Header("Time Settings")]
    [SerializeField] private float realSecondsPerGameHour = 60f;
    [Tooltip("Скорости ускорения времени: 1x (нормальная), 2x, 4x")]
    [SerializeField] private float[] timeSpeedMultipliers = { 1f, 2f, 4f };
    [SerializeField] private Button speedButton;
    [SerializeField] private TMP_Text speedButtonText;
    [SerializeField] private DayPart currentDayPart;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text dayOfWeekText;

    private DateTime _gameTime;
    private int _currentSpeedIndex = 0;
    private int _lastDisplayedMinute = -1;
    private int _lastDisplayedDay = -1;
    private bool _isEveningTriggered = false;
    private int _lastHour;
    private string _lastDay;

    public static event Action OnEveningTime;
    public static event Action OnNewDay;
    public static event Action OnNewHour;


    private void Awake()
    {
        Instance = this;
        currentDayPart = DayPart.morning;

        // Устанавливаем нормальную скорость по умолчанию
        Time.timeScale = 1f;
        _currentSpeedIndex = 0;

        _gameTime = new DateTime(2025, 1, 1, 7, 30, 0);
        speedButton.onClick.AddListener(ToggleTimeSpeed);
        UpdateSpeedButtonText();
        CheckEveningTime();
        ForceUpdateAllUI();

        _lastHour = _gameTime.Hour;
        _lastDay = _gameTime.ToString("dddd");
    }

    public DateTime GetGameTime() => _gameTime;
    public DayPart GetCurrentDayPart() => currentDayPart;

    private void CheckEveningTime()
    {
        int gameHour = _gameTime.Hour;
        int gameMinute = _gameTime.Minute;
        bool isPreMorningTime = (gameHour == 7 && gameMinute >= 30) || (gameHour == 8 && gameMinute == 0);

        if (isPreMorningTime)
        {
            currentDayPart = DayPart.premorning;
            _isEveningTriggered = false;
        }
        else if ((gameHour >= 8 && gameHour < 13) || (gameHour >= 14 && gameHour < 17))
        {
            currentDayPart = DayPart.morning;
            _isEveningTriggered = false;
        }
        else if (gameHour >= 13 && gameHour <= 14)
        {
            currentDayPart = DayPart.dinner;
            _isEveningTriggered = false;
        }
        else if (gameHour >= 17 && gameHour < 21)
        {
            currentDayPart = DayPart.evening;
            if (!_isEveningTriggered)
            {
                OnEveningTime?.Invoke();
                _isEveningTriggered = true;
            }
        }
        else
        {
            currentDayPart = DayPart.night;
            if (gameHour == 8 && gameMinute == 0) // Сброс флага при наступлении утра
            {
                _isEveningTriggered = false;
            }
        }
    }

    public void CheckNewHour()
    {
        int currentHour = _gameTime.Hour;
        if (currentHour != _lastHour && (currentDayPart == DayPart.morning || currentDayPart == DayPart.dinner))
        {
            OnNewHour?.Invoke();
        }
        _lastHour = currentHour;
    }

    private void CheckNewDay()
    {
        string day = _gameTime.ToString("dddd");
        if (_lastDay != day)
        {
            OnNewDay?.Invoke();
            _lastDay = day;
        }
    }

    private void Update()
    {
        _gameTime = _gameTime.AddSeconds(Time.unscaledDeltaTime * (60f / realSecondsPerGameHour) * Time.timeScale);

        UpdateUITexts();
        CheckEveningTime();
        CheckNewDay();
        CheckNewHour();
    }

    private void ToggleTimeSpeed()
    {
        _currentSpeedIndex = (_currentSpeedIndex + 1) % timeSpeedMultipliers.Length;
        Time.timeScale = timeSpeedMultipliers[_currentSpeedIndex];
        UpdateSpeedButtonText();
        AudioManager.Instance.UpdateAllAudioPitches(Time.timeScale);
        // Можно добавить визуальную/звуковую обратную связь
        // Debug.Log($"Time scale changed to: {Time.timeScale}x");
    }

    private void UpdateSpeedButtonText()
    {
        speedButtonText.text = $"×{Time.timeScale}";

        // Дополнительное визуальное выделение высокой скорости
        if (Time.timeScale >= 16f)
        {
            speedButtonText.color = Color.red;
        }
        else
        {
            speedButtonText.color = Color.black;
        }
    }

    private void UpdateUITexts()
    {
        if (_gameTime.Minute % 15 == 0 && _gameTime.Minute != _lastDisplayedMinute)
        {
            timeText.text = _gameTime.ToString("HH:mm");
            _lastDisplayedMinute = _gameTime.Minute;
        }

        if (_gameTime.Day != _lastDisplayedDay && _gameTime.Hour == 0 && _gameTime.Minute == 0)
        {
            dayOfWeekText.text = _gameTime.ToString("dddd", new System.Globalization.CultureInfo("en-US"));
            _lastDisplayedDay = _gameTime.Day;
        }
    }

    private void ForceUpdateAllUI()
    {
        timeText.text = _gameTime.ToString("HH:mm");
        dayOfWeekText.text = _gameTime.ToString("dddd", new System.Globalization.CultureInfo("en-US"));
        _lastDisplayedMinute = _gameTime.Minute;
        _lastDisplayedDay = _gameTime.Day;
    }
}