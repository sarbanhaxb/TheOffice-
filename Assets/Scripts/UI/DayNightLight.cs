using System;
using UnityEngine;

using UnityEngine.Rendering.Universal;

public class DayNightLight : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 1f;

    [Header("Daylight Colors")]
    [SerializeField] private Color midnightColor = new Color(0.05f, 0.05f, 0.15f);
    [SerializeField] private Color dawnColor = new Color(0.3f, 0.3f, 0.5f);
    [SerializeField] private Color sunriseColor = new Color(1f, 0.7f, 0.4f);
    [SerializeField] private Color dayColor = new Color(1f, 0.95f, 0.9f);
    [SerializeField] private Color sunsetColor = new Color(1f, 0.5f, 0.3f);
    [SerializeField] private Color duskColor = new Color(0.2f, 0.2f, 0.4f);

    [Header("Time Settings")]
    [SerializeField, Range(0, 24)] private float dawnStartTime = 4f;    // Начало рассвета
    [SerializeField, Range(0, 24)] private float sunriseTime = 6f;     // Восход солнца
    [SerializeField, Range(0, 24)] private float fullDayTime = 8f;     // Полный день
    [SerializeField, Range(0, 24)] private float sunsetStartTime = 18f; // Начало заката
    [SerializeField, Range(0, 24)] private float duskEndTime = 21f;     // Конец сумерек

    private void Update()
    {
        DateTime currentTime = GameTime.Instance.GetGameTime();
        UpdateLighting(currentTime);
    }

    private void UpdateLighting(DateTime time)
    {
        float currentHour = time.Hour + time.Minute / 60f;
        (Color color, float intensity) = CalculateLightParameters(currentHour);

        globalLight.color = color;
        globalLight.intensity = intensity;
    }

    private (Color color, float intensity) CalculateLightParameters(float currentHour)
    {
        currentHour %= 24;

        // Ночь (полночь → рассвет)
        if (currentHour < dawnStartTime)
        {
            float t = NormalizedTime(0, dawnStartTime, currentHour);
            return (Color.Lerp(midnightColor, dawnColor, t),
                    Mathf.Lerp(minIntensity, minIntensity * 1.2f, t));
        }
        // Рассвет (начало → восход)
        else if (currentHour < sunriseTime)
        {
            float t = NormalizedTime(dawnStartTime, sunriseTime, currentHour);
            return (Color.Lerp(dawnColor, sunriseColor, t),
                    Mathf.Lerp(minIntensity * 1.2f, maxIntensity * 0.6f, t));
        }
        // Утро (восход → полный день)
        else if (currentHour < fullDayTime)
        {
            float t = NormalizedTime(sunriseTime, fullDayTime, currentHour);
            return (Color.Lerp(sunriseColor, dayColor, t),
                    Mathf.Lerp(maxIntensity * 0.6f, maxIntensity, t));
        }
        // День (полный день → начало заката)
        else if (currentHour < sunsetStartTime)
        {
            return (dayColor, maxIntensity);
        }
        // Закат (начало → конец)
        else if (currentHour < duskEndTime)
        {
            float t = NormalizedTime(sunsetStartTime, duskEndTime, currentHour);
            return (Color.Lerp(dayColor, duskColor, t),
                    Mathf.Lerp(maxIntensity, minIntensity * 1.2f, t));
        }
        // Вечер (конец заката → полночь)
        else
        {
            float t = NormalizedTime(duskEndTime, 24, currentHour);
            return (Color.Lerp(duskColor, midnightColor, t),
                    Mathf.Lerp(minIntensity * 1.2f, minIntensity, t));
        }
    }

    // Плавная нормализация времени с использованием кривой Безье
    private float NormalizedTime(float start, float end, float current)
    {
        float t = Mathf.Clamp01((current - start) / (end - start));
        // Кривая Безье для более плавных переходов
        return t * t * (3f - 2f * t);
    }
}