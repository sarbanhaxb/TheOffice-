using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private Transform hourHand;   // Часовая стрелка
    [SerializeField] private Transform minuteHand; // Минутная стрелка

    private void Start()
    {
        System.DateTime currentTime = GameTime.Instance.GetGameTime();
        // Вычисляем углы поворота
        float hourAngle = (currentTime.Hour % 12) * 30f + currentTime.Minute * 0.5f;  // 30° в час + 0.5° в минуту
        float minuteAngle = currentTime.Minute * 6f;  // 6° в минуту

        // Применяем поворот
        hourHand.localRotation = Quaternion.Euler(0, 0, -hourAngle);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteAngle);

    }

    void Update()
    {
        // Получаем текущее время
        System.DateTime currentTime = GameTime.Instance.GetGameTime();

        // Вычисляем углы поворота
        float hourAngle = (currentTime.Hour % 12) * 30f + currentTime.Minute * 0.5f;  // 30° в час + 0.5° в минуту
        float minuteAngle = currentTime.Minute * 6f;  // 6° в минуту

        // Применяем поворот
        hourHand.localRotation = Quaternion.Euler(0, 0, -hourAngle);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
    }
}