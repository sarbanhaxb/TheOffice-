using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfficeCatcher : MonoBehaviour
{
    public static OfficeCatcher Instance { get; private set; }

    [Header("Настройки")]
    public int targetScore = 20;
    public float startSpeed = 100f;
    public float speedIncrease = 10f;
    public float spawnDelay = 1.5f;
    public int maxObjects = 15;
    public float gameDuration = 60f;

    [Header("Префабы")]
    public List<GameObject> officeItemPrefabs;

    [Header("UI элементы")]
    public Image targetImage;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public RectTransform gameArea;

    [Header("Тексты результатов")]
    public TMP_Text winResultText;
    public TMP_Text loseResultText;

    private int score = 0;
    private float gameTimer;
    public GameObject currentTarget;
    private bool isGameActive = false;
    private List<GameObject> activeItems = new List<GameObject>();
    private Coroutine spawnCoroutine;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void StartMiniGame()
    {
        gameObject.SetActive(true);
        ResetGame();
        isGameActive = true;

        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (isGameActive)
        {
            if (activeItems.Count < maxObjects)
            {
                SpawnItem();
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        if (!isGameActive) return;

        gameTimer -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.CeilToInt(gameTimer)}с";

        if (gameTimer <= 0)
        {
            GameOver("Time is up!");
        }

        if (GameTime.Instance.GetCurrentDayPart() == DayPart.evening)
        {
            CloseGame();
        }
    }

    private void SpawnItem()
    {
        if (!isGameActive || activeItems.Count >= maxObjects) return;

        GameObject prefab = officeItemPrefabs[Random.Range(0, officeItemPrefabs.Count)];
        GameObject newItem = Instantiate(prefab, transform);

        SetupItem(newItem);
        activeItems.Add(newItem);
    }

    private void SetupItem(GameObject item)
    {
        RectTransform rt = item.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(80, 80);

        Vector2 spawnPos = GetRandomPositionInGameArea();
        rt.position = spawnPos;

        Image image = item.GetComponent<Image>();
        image.raycastTarget = true;

        Button button = item.GetComponent<Button>();
        if (button == null) button = item.AddComponent<Button>();
        button.onClick.AddListener(() => OnItemClicked(item));

        UIItemMovement movement = item.GetComponent<UIItemMovement>();
        if (movement == null) movement = item.AddComponent<UIItemMovement>();
        movement.Initialize(startSpeed, gameArea);
    }

    private Vector2 GetRandomPositionInGameArea()
    {
        if (gameArea == null)
            return new Vector2(Screen.width / 2, Screen.height / 2);

        Vector3[] corners = new Vector3[4];
        gameArea.GetWorldCorners(corners);

        float padding = 40f;
        float randomX = Random.Range(corners[0].x + padding, corners[2].x - padding);
        float randomY = Random.Range(corners[0].y + padding, corners[2].y - padding);

        return new Vector2(randomX, randomY);
    }

    public void OnItemClicked(GameObject item)
    {
        if (!isGameActive) return;

        string targetName = currentTarget.name.Replace("(Clone)", "").Trim();
        string clickedName = item.name.Replace("(Clone)", "").Trim();

        if (clickedName == targetName)
        {
            score++;
            DestroyItem(item);
            UpdateScore();

            if (score >= targetScore)
            {
                WinGame();
                return;
            }
            SetNewTarget();
        }
        else
        {
            GameOver("Wrong Element!");
        }
    }

    private void DestroyItem(GameObject item)
    {
        activeItems.Remove(item);
        Destroy(item);
    }

    private void SetNewTarget()
    {
        if (officeItemPrefabs.Count == 0) return;

        currentTarget = officeItemPrefabs[Random.Range(0, officeItemPrefabs.Count)];

        if (currentTarget.GetComponent<Image>().sprite != null)
        {
            targetImage.sprite = currentTarget.GetComponent<Image>().sprite;
        }

        targetImage.SetAllDirty();
        LayoutRebuilder.ForceRebuildLayoutImmediate(targetImage.rectTransform);
    }

    private void UpdateScore()
    {
        scoreText.text = $"Score: {score}/{targetScore}";
    }

    private void WinGame()
    {
        isGameActive = false;

        // Очищаем все игровые элементы
        Cleanup();

        // Показываем экран победы
        winPanel.SetActive(true);
        winResultText.text = $"Congrats! You earned {score}$!";

        // Начисляем деньги
        MoneyManager.Instance?.AddMoney(score);
    }

    private void GameOver(string reason = "")
    {
        isGameActive = false;

        // Очищаем все игровые элементы
        Cleanup();

        // Показываем экран проигрыша
        gameOverPanel.SetActive(true);
        loseResultText.text = $"You earned {score / 2}$!\n{reason}";

        // Начисляем деньги (половину от счета)
        MoneyManager.Instance?.AddMoney(score);
    }

    public void CloseGame()
    {
        // Дополнительная очистка на всякий случай
        Cleanup();

        // Скрываем все панели
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // Отключаем весь Canvas мини-игры
        gameObject.SetActive(false);
    }

    private void Cleanup()
    {
        // Останавливаем спавн
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        // Удаляем все активные элементы
        foreach (var item in activeItems)
        {
            if (item != null)
                Destroy(item);
        }
        activeItems.Clear();

        // Дополнительная очистка дочерних объектов
        foreach (Transform child in transform)
        {
            if (child.CompareTag("GameElement"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void ResetGame()
    {
        Cleanup();
        score = 0;
        gameTimer = gameDuration;
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        UpdateScore();
        SetNewTarget();
    }
}