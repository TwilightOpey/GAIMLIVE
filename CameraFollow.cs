using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    private float fixedHeight;

    void Start()
    {
        fixedHeight = transform.position.y;
    }

    void FixedUpdate ()
    {
        Vector3 desiredPosition =  target.position + offset;
        desiredPosition.y = fixedHeight;
        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
