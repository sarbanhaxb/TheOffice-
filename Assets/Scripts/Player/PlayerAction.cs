using System;
using UnityEngine;
using UnityEngine.EventSystems;
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

    private void OnEnable()
    {
        GameInput.Instance.playerInputActions.Player.Action.performed += OnClickAction;
        GameInput.Instance.playerInputActions.Player.Action.performed += OnContextMenu;
    }
    private void OnDisable()
    {
        GameInput.Instance.playerInputActions.Player.Action.performed -= OnClickAction;
        GameInput.Instance.playerInputActions.Player.Action.performed -= OnContextMenu;
    }

    private void OnClickAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

            if (EventSystem.current.IsPointerOverGameObject()) { return; }

            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, _layerMask);

            Debug.DrawRay(mouseWorldPos, Vector2.right * 0.1f, Color.green, 1f);

            if (hit != null && (hit.CompareTag("Player") || hit.CompareTag("NPC")))
            {
                if (_selectedObject != null)
                {
                    _selectedObject.HideBarAnimation();
                }
                _selectedObject = hit.TryGetComponent<EntityVisual>(out var t) ? t : null;
                _selectedObject.ShowBarAnimation();
            }
            else if (_selectedObject != null) 
            {
                _selectedObject.HideBarAnimation();
                //Debug.Log(" лик не попал в NPC. Pos: " + mouseWorldPos);
                _selectedObject = null;
            }
        }
    }
    private void OnContextMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && _selectedObject != null)
        {
            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, _layerMask);

            //if (hit != null && (hit.CompareTag("Player") || hit.CompareTag("NPC")))
            //{
            //    Debug.Log(hit.tag);
            //}
        }
    }
}
