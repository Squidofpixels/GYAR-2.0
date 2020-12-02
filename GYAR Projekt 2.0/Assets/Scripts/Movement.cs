using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;


    private bool facingRight = true;


    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private int extraJumps;
    public int extraJumpsValue;


    public bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    private int input;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    private void Start()
    {
        extraJumps = extraJumpsValue; 
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        // för att röra sig höger och vänster
       /* moveInput = Input.GetAxis("Horizontal"); 
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            flip();
        }
       */
    }

    private void Update()
    {
        
        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        // för att hoppa och dubbelhoppa
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // för att vägghoppa
        if(isTouchingFront == true && isGrounded == false && input != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if(wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if(wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }

        if(wallJumping != true)
        {
            moveInput = Input.GetAxis("Horizontal");
            Debug.Log(moveInput);
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    //void flip()
    //{
        // för att flippa player objectet höger och vänster
        //facingRight = !facingRight;
        //Vector3 Scaler = transform.localScale;
        //Scaler.x *= -1;
        //transform.localScale = Scaler;
    //}

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
}
