using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 2f;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private float smoothTime = 0.1f;

    private Camera _camera;
    private Vector3 _dragOriginWorldPos;
    private Vector3 _cameraOriginPos;
    private bool _isDragging;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        // ѕримен€ем ограничени€ области движени€ камеры
        Vector3 clampedPosition = _camera.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
        _camera.transform.position = clampedPosition;
    }

    private void OnEnable()
    {
        GameInput.Instance.playerInputActions.Player.Drag.performed += OnMouseDragPerformed;
        GameInput.Instance.playerInputActions.Player.Drag.started += OnMouseDragStarted;
        GameInput.Instance.playerInputActions.Player.Drag.canceled += OnMouseDragCanceled;
        GameInput.Instance.playerInputActions.Player.CameraResetPosition.canceled += OnMouse;
    }

    private void OnDisable()
    {
        GameInput.Instance.playerInputActions.Player.Drag.performed -= OnMouseDragPerformed;
        GameInput.Instance.playerInputActions.Player.Drag.started -= OnMouseDragStarted;
        GameInput.Instance.playerInputActions.Player.Drag.canceled -= OnMouseDragCanceled;
        GameInput.Instance.playerInputActions.Player.CameraResetPosition.canceled -= OnMouse;
    }

    private void OnMouse(InputAction.CallbackContext callbackContext)
    {
        _camera.transform.localPosition = new Vector3(0, 0, -10);
    }

    private void OnMouseDragStarted(InputAction.CallbackContext callbackContext)
    {
        _isDragging = true;
        _dragOriginWorldPos = GameInput.Instance.GetMousePosition();
        _cameraOriginPos = _camera.transform.position;
    }

    private void OnMouseDragPerformed(InputAction.CallbackContext callbackContext)
    {
        if (!_isDragging) return;

        Vector3 currentMouseWorldPos = GameInput.Instance.GetMousePosition();
        Vector3 difference = currentMouseWorldPos - _dragOriginWorldPos;

        // ¬ычисл€ем целевую позицию на основе исходной позиции камеры
        Vector3 targetPosition = _cameraOriginPos - difference * dragSpeed;

        // ѕлавное перемещение к целевой позиции
        _camera.transform.position = Vector3.SmoothDamp(
            _camera.transform.position,
            targetPosition,
            ref _velocity,
            smoothTime);
    }

    private void OnMouseDragCanceled(InputAction.CallbackContext callbackContext)
    {
        _isDragging = false;
    }
}