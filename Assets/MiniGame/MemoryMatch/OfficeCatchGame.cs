using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class OfficeCatchGame : MonoBehaviour
{
    [Header("Настройки")]
    public int targetScore = 20;
    public float startSpeed = 1f;
    public float speedIncrease = 0.1f;
    public float swapnDelay = 1.5f;

    [Header("Ссылки")]
    public List<GameObject> officeItems;
    public Image targetImage;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

    private int score = 0;
    private GameObject currentTarget;
    private bool isGameActive = true;

    private void Start()
    {
        SetNewTarget();
        InvokeRepeating(nameof(SpawnItem), 0f, swapnDelay);
        scoreText.text = $"Score: {score}";

    }

    private void SpawnItem()
    {
        if (!isGameActive) return;

        GameObject randomItem = officeItems[Random.Range(0, officeItems.Count)];
        GameObject item = Instantiate(randomItem, GetRandomSpawnPos(), Quaternion.identity, transform);

        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = item.AddComponent<PolygonCollider2D>();
        }

        OfficeItem itemScript = item.AddComponent<OfficeItem>();
        itemScript.Initialize(startSpeed, speedIncrease);

        item.GetComponent<Button>().onClick.AddListener(() => OnItemClicked(item));
    }

    private Vector2 GetRandomSpawnPos()
    {
        float x = Random.Range(0, Screen.width);
        float y = Random.Range(0, Screen.height);
        return Camera.main.ScreenToWorldPoint(new Vector2(x, y));
    }

    private void OnItemClicked(GameObject item)
    {
        if (!isGameActive) return;

        OfficeItem itemComponent = item.GetComponent<OfficeItem>();
        if (itemComponent == null)
        {
            Debug.LogError("У элемента нет компонента OfficeItem!");
            return;
        }

        // Сравниваем имя цели (без клонирования "(Clone)")
        string currentTargetName = currentTarget.name.Replace("(Clone)", "");
        string clickedItemName = item.name.Replace("(Clone)", "");

        if (clickedItemName == currentTargetName)
        {
            score++;
            scoreText.text = $"Score: {score}";
            Destroy(item);

            if (score >= targetScore)
            {
                WinGame();
                return;
            }
            SetNewTarget();
        }
        else
        {
            GameOver();
        }
    }
    private void SetNewTarget()
    {
        currentTarget = officeItems[Random.Range(0, officeItems.Count)];
        targetImage.sprite = currentTarget.GetComponent<SpriteRenderer>().sprite;
    }

    private void WinGame()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        Debug.Log("You are win! Earned: " + (score) + "$");
        MoneyManager.Instance.AddMoney(score);
    }

    private void GameOver()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        Debug.Log("You are lose! Earned: " + (score/2) + "$");
        MoneyManager.Instance.AddMoney(score/2);

    }
}

