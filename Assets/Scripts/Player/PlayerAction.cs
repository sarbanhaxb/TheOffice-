using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private Camera _mainCamera;
    private EntityVisual _selectedObject;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, _layerMask);

            Debug.DrawRay(mouseWorldPos, Vector2.right * 0.1f, Color.green, 1f);

            if (hit != null && (hit.CompareTag("Player") || hit.CompareTag("NPC")))
            {
                _selectedObject = hit.TryGetComponent<EntityVisual>(out var t) ? t : null;
                _selectedObject.ShowBarAnimation();
            }
            else if (_selectedObject != null)
            {
                _selectedObject.HideBarAnimation();
                Debug.Log("Клик не попал в NPC. Pos: " + mouseWorldPos);
            }
        }
    }

    public void OnContextMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Контекстное меню открыто");
    }
}
