using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0f, 10f)] private Vector3 offset;
    [SerializeField] private float smoothFactor;

    private float zPosition = -10;

    private void FixedUpdate()
    {
        Follow();
        zPosition = transform.position.z;
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        smoothPosition.z = zPosition;
        transform.position = smoothPosition;
        
    }

}