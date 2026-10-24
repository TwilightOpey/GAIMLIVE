using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    private float fixedHeight;

    private Bounds camBounds;
    private Vector3 targetPos;
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
    }



    void Start()
    {
        fixedHeight = transform.position.y;
        var height = _camera.orthographicSize;
        var width = height * _camera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        camBounds = new Bounds();
        camBounds.SetMinMax(new Vector3(minX, minY, 0f), new Vector3 (maxX, maxY, 0));
    }

    void FixedUpdate ()
    {
        Vector3 desiredPosition =  target.position + offset;
        desiredPosition.y = fixedHeight;

        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);

        targetPos = smoothedPosition;
        targetPos = GetCameraBounds();


        

        transform.position = targetPos;
        
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3( 
            Mathf.Clamp(targetPos.x, camBounds.min.x, camBounds.max.x),
            Mathf.Clamp(targetPos.y, camBounds.min.y, camBounds.max.y),
            transform.position.z
        );
    }
}
