using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Настройки")]
    public float interactionRange = 1.5f;
    public LayerMask interactableLayer;

    private IInteractable _currentInteractable;
    private IInteractable _previousInteractable;
    private Vector2 _facingDirection = Vector2.right;

    private void Update()
    {
        FindInteractableWithPriority();
        UpdateInteractionHint();
    }

    private void FindInteractableWithPriority()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        _currentInteractable = GetHighestPriorityInteractable(hits);
    }

    private IInteractable GetHighestPriorityInteractable(Collider2D[] hits)
    {
        IInteractable highestPriorityInteractable = null;
        float highestPriority = float.MinValue;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IInteractable interactable))
            {
                // Получаем приоритет и расстояние
                float priority = interactable.GetPriority();
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                // Сначала проверяем приоритет, затем расстояние
                if (priority > highestPriority ||
                   (Mathf.Approximately(priority, highestPriority) && distance < closestDistance))
                {
                    highestPriority = priority;
                    closestDistance = distance;
                    highestPriorityInteractable = interactable;
                }
            }
        }

        return highestPriorityInteractable;
    }

    private void UpdateInteractionHint()
    {
        // Обновляем подсказки только при изменении текущего объекта
        if (_currentInteractable != _previousInteractable)
        {
            _previousInteractable?.HideHint();
            _currentInteractable?.ShowHint();
            _previousInteractable = _currentInteractable;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && _currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 3f, interactionRange);
    }
}