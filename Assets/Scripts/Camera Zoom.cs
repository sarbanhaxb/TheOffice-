using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private Camera _camera;
    private float _zoomTarget;
    private float _velocity;

    [SerializeField] private float multiplier = 100f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 35f;
    [SerializeField] private float smoothTime = 0.1f;

    private void Start()
    {
        _camera = Camera.main;
        _zoomTarget = _camera.orthographicSize;
    }

    private void Update()
    {
        float scrollValue = Mouse.current.scroll.ReadValue().y * 0.01f;

        _zoomTarget -= scrollValue * multiplier;
        _zoomTarget = Mathf.Clamp(_zoomTarget, minZoom, maxZoom);

        _camera.orthographicSize = Mathf.SmoothDamp(
            _camera.orthographicSize,
            _zoomTarget,
            ref _velocity,
            smoothTime
        );
    }
}