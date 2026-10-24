using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speedX;
    private float initialPosX;
    public float returnDistance;

    private float returnPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosX = transform.position.x;
        returnPosition = initialPosX + returnDistance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedX * Time.deltaTime, 0, 0);

        if(transform.position.x > returnPosition)
        {
            speedX = speedX * -1;
        }

        if(transform.position.x < initialPosX)
        {
            speedX = speedX * -1;
        }
    }
}


