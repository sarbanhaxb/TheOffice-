using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("���������")]
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
                // �������� ��������� � ����������
                float priority = interactable.GetPriority();
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                // ������� ��������� ���������, ����� ����������
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
        // ��������� ��������� ������ ��� ��������� �������� �������
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