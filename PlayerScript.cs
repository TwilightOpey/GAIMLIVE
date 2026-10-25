using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Declare variables
    public float playerSpeed;
    Rigidbody2D rb;
    CapsuleCollider2D boxCollider2d;
    Animator animator;
    public float jumpForce;

    [SerializeField] GameObject deathScr;

    [SerializeField] private LayerMask platformLayerMask;
    

    //how long is player in the air
    private float jumpTime;

    private Vector3 initialPos;
    
    private int jumping;
    private int onGround;
    public bool isFacingLeft;
    private Vector2 facingLeft;
    
    Vector3 movement;

    public string nextLevel;

    //max time player can hold the button for
    public float buttonTime;

    private float elapsedTime = 0.2f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialize variables
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        jumping = 1;
        onGround = 1;

        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        initialPos = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        if(moveHorizontal != 0) 
        {
            animator.SetFloat("speed", 0.5f);
        }

        if(moveHorizontal == 0)
        {
            animator.SetFloat("speed", 0);
        }

        //movement = new Vector3(moveHorizontal,  , 0);
        //check for inputs
        rb.linearVelocity = new Vector2 (moveHorizontal * playerSpeed, rb.velocity.y);

        if(moveHorizontal > 0 && isFacingLeft)
        {
            isFacingLeft = false;
            Flip();
        }
        if(moveHorizontal < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            Flip();
        }
        

        //when player presses space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //and character is not already jumping
            if(IsGrounded()) 
            {
                //make jumping 0
                onGround--;
                jumping--;
                animator.SetBool("jumping", true);

                //start counting jumpTime
                jumpTime = 0;

                //Debug.Log("you cannot jump");
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
            //Debug.Log("jumping is false");
        }
    }

    private bool IsGrounded() 
    {
        float extraHeightText = 0.5f;
        
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText));
        Debug.Log(raycastHit.collider);
        
        return raycastHit.collider != null;
    }

    void OnCollisionEnter2D (Collision2D otherObject) 
    {
        if (otherObject.gameObject.CompareTag("floor"))
        {
            onGround = 1;
            animator.SetBool("jumping", false);
            Debug.Log("Im on the floor");
        }
    }

    void OnTriggerEnter2D (Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("death")) 
        {
            transform.position = initialPos;
            deathScr.SetActive(true);   

        }

        if (otherObject.gameObject.CompareTag("Exit"))
        {
           SceneManager.LoadScene(nextLevel);
        }
    }

    protected void Flip()
    {
        if (isFacingLeft)
        {
        transform.localScale = facingLeft;
        }
        if (!isFacingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

}
