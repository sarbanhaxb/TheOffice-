using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;
using UnityEngine.Rendering.Universal;

public class DayNightLight : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private Color nightColor = new Color(0.4f, 0.4f, 0.8f);
    [SerializeField] private Color dayColor = Color.white;

    [Header("Time Settings")]
    [SerializeField] private int sunriseHour = 6;  // Рассвет в 6:00
    [SerializeField] private int sunsetHour = 18;  // Закат в 18:00
    [SerializeField] private float transitionDuration = 2f; // Часы на переход день/ночь

    private void Update()
    {
        DateTime currentTime = GameTime.Instance.GetGameTime();
        UpdateLighting(currentTime);
    }

    private void UpdateLighting(DateTime time)
    {
        float currentHour = time.Hour + time.Minute / 60f;
        float lightValue = CalculateLightValue(currentHour);

        globalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, lightValue);
        globalLight.color = Color.Lerp(nightColor, dayColor, lightValue);
    }

    private float CalculateLightValue(float currentHour)
    {
        // Нормализуем время в циклический формат (0-24)
        currentHour %= 24;

        // Рассвет начинается в sunriseHour, длится transitionDuration часов
        if (currentHour >= sunriseHour && currentHour <= sunriseHour + transitionDuration)
        {
            return Mathf.InverseLerp(sunriseHour, sunriseHour + transitionDuration, currentHour);
        }
        // Закат начинается в sunsetHour, длится transitionDuration часов
        else if (currentHour >= sunsetHour && currentHour <= sunsetHour + transitionDuration)
        {
            return 1f - Mathf.InverseLerp(sunsetHour, sunsetHour + transitionDuration, currentHour);
        }
        // День (между рассветом и закатом)
        else if (currentHour > sunriseHour + transitionDuration && currentHour < sunsetHour)
        {
            return 1f;
        }
        // Ночь (в остальное время)
        else
        {
            return 0f;
        }
    }
}