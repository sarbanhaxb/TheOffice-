using UnityEngine;
using UnityEngine.EventSystems;

public class CollisionHandler : MonoBehaviour, IPointerEnterHandler
{
    private UIItemMovement movement;

    private void Awake()
    {
        movement = GetComponent<UIItemMovement>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���������, �������� �� ������ ������ UI ���������
        if (eventData.pointerEnter != null && eventData.pointerEnter != gameObject)
        {
            CollisionHandler other = eventData.pointerEnter.GetComponent<CollisionHandler>();
            if (other != null)
            {
                HandleCollision();
                other.HandleCollision();
            }
        }
    }

    private void HandleCollision()
    {
        movement?.Split();
    }
}