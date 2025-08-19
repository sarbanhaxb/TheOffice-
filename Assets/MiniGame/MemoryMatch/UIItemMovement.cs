using UnityEngine;
using UnityEngine.UI;

public class UIItemMovement : MonoBehaviour
{
    [Header("��������� ��������")]
    public float minSize = 30f;
    public int maxSplits = 3;

    private float speed;
    private RectTransform rectTransform;
    private RectTransform boundary;
    private Vector2 direction;
    private int splitCount = 0;
    private Vector3[] boundaryCorners = new Vector3[4];

    public void Initialize(float itemSpeed, RectTransform boundaryArea)
    {
        speed = itemSpeed;
        boundary = boundaryArea;
        rectTransform = GetComponent<RectTransform>();
        direction = Random.insideUnitCircle.normalized;

        // �������� ���� �������
        if (boundary != null)
        {
            boundary.GetWorldCorners(boundaryCorners);
        }
    }

    private void Update()
    {
        if (boundary == null) return;

        // ��������� ������� ���������� �������
        boundary.GetWorldCorners(boundaryCorners);

        // �������� � ������� �����������
        Vector2 currentPosition = rectTransform.position;
        rectTransform.position = currentPosition + direction * speed * Time.deltaTime;

        // �������� ������ � ������
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        Vector2 currentPos = rectTransform.position;
        float halfWidth = rectTransform.rect.width / 2;
        float halfHeight = rectTransform.rect.height / 2;

        bool bounced = false;

        // �������� ����� � ������ ������
        if (currentPos.x - halfWidth <= boundaryCorners[0].x ||
            currentPos.x + halfWidth >= boundaryCorners[2].x)
        {
            direction.x *= -1;
            bounced = true;
        }

        // �������� ������ � ������� ������
        if (currentPos.y - halfHeight <= boundaryCorners[0].y ||
            currentPos.y + halfHeight >= boundaryCorners[2].y)
        {
            direction.y *= -1;
            bounced = true;
        }

        if (bounced)
        {
            // ��������� ��������� �����������
            direction = Vector2.Lerp(direction, Random.insideUnitCircle.normalized, 0.2f).normalized;

            // ������������ �������, ����� �� �������� �� �������
            ClampToBoundaries();
        }
    }

    private void ClampToBoundaries()
    {
        Vector2 currentPos = rectTransform.position;
        float halfWidth = rectTransform.rect.width / 2;
        float halfHeight = rectTransform.rect.height / 2;

        float clampedX = Mathf.Clamp(
            currentPos.x,
            boundaryCorners[0].x + halfWidth,
            boundaryCorners[2].x - halfWidth
        );

        float clampedY = Mathf.Clamp(
            currentPos.y,
            boundaryCorners[0].y + halfHeight,
            boundaryCorners[2].y - halfHeight
        );

        rectTransform.position = new Vector2(clampedX, clampedY);
    }

    public void Split()
    {
        if (splitCount >= maxSplits || rectTransform.sizeDelta.x <= minSize) return;

        for (int i = 0; i < 2; i++)
        {
            GameObject newItem = Instantiate(gameObject, transform.parent);
            RectTransform newRt = newItem.GetComponent<RectTransform>();

            newRt.sizeDelta = rectTransform.sizeDelta * 0.7f;

            Vector2 offset = Random.insideUnitCircle * 20f;
            newRt.position = rectTransform.position + (Vector3)offset;

            UIItemMovement movement = newItem.GetComponent<UIItemMovement>();
            movement.Initialize(speed * 0.9f, boundary);
            movement.splitCount = splitCount + 1;

            Button button = newItem.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                    FindObjectOfType<OfficeCatcher>()?.OnItemClicked(newItem)
                );
            }
        }

        Destroy(gameObject);
    }
}