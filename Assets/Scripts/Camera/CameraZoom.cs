using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)] private float minZoom = 1f;
    [SerializeField, Range(1f, 100f)] private float maxZoom = 5f;
    [SerializeField, Range(1f, 100f)] private float smoothSpeed = 20f;
    [SerializeField, Range(1f, 10f)] private float zoomStrength = 5f;

    private Camera _camera;
    private float _targetZoom;
    private float _zoomVelocity;

    private void Start()
    {
        _camera = Camera.main;
        _targetZoom = _camera.orthographicSize;
    }
    private void Update()
    {
        float zoomInput = Mouse.current.scroll.ReadValue().y;
        if (zoomInput != 0)
        {
            _targetZoom -= zoomInput * zoomStrength;
            _targetZoom = Mathf.Clamp(_targetZoom, minZoom, maxZoom);
        }
        _camera.orthographicSize = Mathf.SmoothDamp(current: _camera.orthographicSize, _targetZoom, ref _zoomVelocity, smoothTime: smoothSpeed * Time.deltaTime);
    }
}