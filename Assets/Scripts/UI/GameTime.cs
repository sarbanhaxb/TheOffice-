using UnityEngine;
using TMPro;
using System;

public class DayNightSystem : MonoBehaviour
{
    [Header("Настройки времени")]
    [SerializeField] private float _realSecondsPerGameHour = 60f; // 1 игровой час = 60 реальных секунд
    [SerializeField] private TMP_Text _timeText; // Ссылка на TextMeshPro

    //[Header("Освещение")]
    //[SerializeField] private Light _sunLight;
    //[SerializeField] private Gradient _skyColorGradient;
    //[SerializeField] private Gradient _sunColorGradient;

    private DateTime _gameTime;
    private bool _isMorningTriggered, _isDayTriggered, _isEveningTriggered;

    public static event Action OnMorning;
    public static event Action OnDay;
    public static event Action OnEvening;

    private int _lastDisplayedMinute = -1; // -1 чтобы гарантировать первое обновление

    private void Start()
    {
        _gameTime = new DateTime(2023, 1, 1, 6, 0, 0); // Начинаем в 6:00
    }

    private void Update()
    {
        UpdateGameTime();
        //UpdateSunRotation();
        //UpdateSkybox();
        CheckTimeEvents();
        UpdateUIText();
    }

    private void UpdateGameTime()
    {
        _gameTime = _gameTime.AddSeconds(Time.deltaTime * (60f / _realSecondsPerGameHour));
        Debug.Log(_gameTime.ToString("HH:mm:ss")); // Логируем время
    }

    //private void UpdateSunRotation()
    //{
    //    // 0.5f = полдень (солнце в зените)
    //    float timeNormalized = (_gameTime.Hour * 60 + _gameTime.Minute) / 1440f; // 1440 = минуты в сутках
    //    _sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(0, 360, timeNormalized) - 90f, 170f, 0);
    //    _sunLight.color = _sunColorGradient.Evaluate(timeNormalized);
    //}

    //private void UpdateSkybox()
    //{
    //    RenderSettings.ambientLight = _skyColorGradient.Evaluate((_gameTime.Hour * 60 + _gameTime.Minute) / 1440f);
    //}

    private void CheckTimeEvents()
    {
        // Утро (6:00)
        if (_gameTime.Hour == 6 && _gameTime.Minute == 0 && !_isMorningTriggered)
        {
            OnMorning?.Invoke();
            _isMorningTriggered = true;
            _isDayTriggered = false;
            _isEveningTriggered = false;
        }
        // День (9:00)
        else if (_gameTime.Hour == 9 && _gameTime.Minute == 0 && !_isDayTriggered)
        {
            OnDay?.Invoke();
            _isDayTriggered = true;
            _isMorningTriggered = false;
        }
        // Вечер (18:00)
        else if (_gameTime.Hour == 18 && _gameTime.Minute == 0 && !_isEveningTriggered)
        {
            OnEvening?.Invoke();
            _isEveningTriggered = true;
            _isDayTriggered = false;
        }
    }
    private void UpdateUIText()
    {
        int currentMinute = _gameTime.Minute;

        // Если текущая минута кратна 15 И ещё не отображали это значение
        if (currentMinute % 15 == 0 && currentMinute != _lastDisplayedMinute)
        {
            _timeText.text = _gameTime.ToString("HH:mm");
            _lastDisplayedMinute = currentMinute; // Запоминаем последнее отображённое значение
        }
    }
}