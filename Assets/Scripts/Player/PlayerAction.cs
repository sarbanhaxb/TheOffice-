using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Canvas _contextMenu;
    [SerializeField] private NPC_CurrentState _currentNPC;

    [SerializeField] private Button drinkWaterButton;
    [SerializeField] private Button eatFoodButton;
    [SerializeField] private Button drinkCoffeeButton;
    [SerializeField] private Button smokingButton;
    [SerializeField] private Button speakingButton;
    private Camera _mainCamera;
    private EntityVisual _selectedObject;


    private void Awake()
    {
        _mainCamera = Camera.main;
        drinkWaterButton.onClick.AddListener(OnDrkinWater);
        eatFoodButton.onClick.AddListener(OnEatFood);
        drinkCoffeeButton.onClick.AddListener(OnDrinkCoffee);
        smokingButton.onClick.AddListener(OnSmoking);
        speakingButton.onClick.AddListener(OnSpeaking);
    }

    private void OnEnable()
    {
        GameInput.Instance.playerInputActions.Player.Action.performed += OnClickAction;
        GameInput.Instance.playerInputActions.Player.ContextMenu.performed += OnContextMenu;
    }
    private void OnDisable()
    {
        GameInput.Instance.playerInputActions.Player.Action.performed -= OnClickAction;
        GameInput.Instance.playerInputActions.Player.ContextMenu.performed -= OnContextMenu;
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
                _currentNPC = hit.TryGetComponent<NPC_CurrentState>(out var z) ? z : null;
                _selectedObject.ShowBarAnimation();
            }
            else if (_selectedObject != null)
            {
                _selectedObject.HideBarAnimation();
                _selectedObject = null;
                _currentNPC = null;
                _contextMenu.enabled = false;
            }
            else
            {
                _contextMenu.enabled = false;
                _currentNPC = null;
            }
        }
    }
    private void OnContextMenu(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, _layerMask);

        if (hit != null && (hit.CompareTag("NPC")))
        {
            _currentNPC = hit.TryGetComponent<NPC_CurrentState>(out var z) ? z : null;
            _contextMenu.transform.position = mouseWorldPos;
            _contextMenu.enabled = !_contextMenu.enabled;
        }
        else if (_selectedObject != null) { 
            _contextMenu.enabled = false;
            _currentNPC = null;
        }
        else
        {
            _contextMenu.enabled = false;
            _currentNPC = null;
        }
    }

    private void OnDrkinWater()
    {
        _currentNPC.SetState(NPCStates.GoDrinkWater);
        _contextMenu.enabled = !_contextMenu.enabled;
    }
    private void OnEatFood()
    {
        _currentNPC.SetState(NPCStates.GoEating);
        _contextMenu.enabled = !_contextMenu.enabled;
    }    
    private void OnDrinkCoffee()
    {
        _currentNPC.SetState(NPCStates.GoDrinkCoffee);
        _contextMenu.enabled = !_contextMenu.enabled;
    }    
    private void OnSmoking()
    {
        _currentNPC.SetState(NPCStates.GoSmoking);
        _contextMenu.enabled = !_contextMenu.enabled;
    }
    private void OnSpeaking()
    {
        _currentNPC.SetState(NPCStates.Speak);
        _contextMenu.enabled = !_contextMenu.enabled;
    }
}

