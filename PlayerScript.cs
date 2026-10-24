using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Declare variables
    public float playerSpeed;
    Rigidbody2D rb;
    public float jumpForce;

    //how long is player in the air
    private float jumpTime;

    
    private int jumping;
    private int onGround;

    //max time player can hold the button for
    public float buttonTime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialize variables
        rb = GetComponent<Rigidbody2D>();
        jumping = 1;
        onGround = 1;
       
    }

    // Update is called once per frame
    void Update()
    {
        //check for inputs
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed, 0, 0);

        //when player presses space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //and character is not already jumping
            if(onGround == 1) 
            {
                //make jumping 0
                onGround--;
                jumping--;

                //start counting jumpTime
                jumpTime = 0;

                Debug.Log("you cannot jump");
            }
            
        }   

        if (jumping == 0) 
        {   
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpTime+= Time.deltaTime;  
        }

        if(Input.GetKeyUp(KeyCode.Space) || jumpTime > buttonTime)
        {
            jumping = 1;
            Debug.Log("jumping is false");
        }
    }

    void OnCollisionEnter2D (Collision2D otherObject) 
    {
        if (otherObject.gameObject.CompareTag("floor"))
        {
            onGround = 1;
            Debug.Log("Im on the floor");
        }
    }
}
