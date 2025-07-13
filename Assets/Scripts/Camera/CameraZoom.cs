using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField, Range(10f, 15f)] private float minZoom = 10f;
    [SerializeField, Range(15f, 50f)] private float maxZoom = 45f;
    [SerializeField, Range(1f, 100f)] private float smoothSpeed = 20f;
    [SerializeField, Range(1f, 10f)] private float zoomStrength = 5f;

    private CinemachineCamera _camera;
    private float _targetZoom;
    private float _zoomVelocity;

    private void Start()
    {
        _camera = GetComponent<CinemachineCamera>();
        _targetZoom = _camera.Lens.OrthographicSize;
    }
    private void Update()
    {
        float zoomInput = Mouse.current.scroll.ReadValue().y;
        if (zoomInput != 0)
        {
            _targetZoom -= zoomInput * zoomStrength;
            _targetZoom = Mathf.Clamp(_targetZoom, minZoom, maxZoom);
        }
        _camera.Lens.OrthographicSize = Mathf.SmoothDamp(current: _camera.Lens.OrthographicSize, _targetZoom, ref _zoomVelocity, smoothTime: smoothSpeed * Time.deltaTime);
    }
}