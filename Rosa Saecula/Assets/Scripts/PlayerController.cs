using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hoirzontal Movement")]
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;
    private float horizontal;
    private float speed;

    //[SerializeField]private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckX = 0.2f;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private LayerMask isGround;

    [SerializeField] private int maxJumps = 1;
    private int jumpsLeft;

    [SerializeField] private float dashingPower = 30f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    public float fallMulti = 2.5f;
    public float lowJumpMulti = 2f;

    [Range(0f, 10f)]
    public float jumpForce;

    Animator anim;

    public static PlayerController Instance;

    PlayerStates playerStates;

    public bool Doublejump;
    public bool canDash = true;
    public bool isDashing;
    public float gravity;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        jumpsLeft = maxJumps;

        gravity = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                Doublejump = true;
            }

            else if (Doublejump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                Doublejump = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        
        Flip();
        

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool isGrounded()
    {
        if(Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckY, isGround) 
            || Physics2D.Raycast(groundCheck.position + new Vector3(groundCheckX, 0, .5f), Vector2.down, groundCheckY, isGround) 
            || Physics2D.Raycast(groundCheck.position + new Vector3(-groundCheckX, 0, .5f), Vector2.down, groundCheckY, isGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {
        //if(Input.GetButton("Jump") && rb.velocity.y > 0)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 0);
        //}

        if(isGrounded() && rb.velocity.y <= 0)
        {
            jumpsLeft = maxJumps;
        }

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            maxJumps -= 1;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
        }
    }

    void Flip()
    {
        if(xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if(xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
