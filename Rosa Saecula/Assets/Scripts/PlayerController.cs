using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hoirzontal Movement")]
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;

    //[SerializeField]private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckX = 0.2f;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private LayerMask isGround;

    [SerializeField] private int maxJumps = 1;
    private int jumpsLeft;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    public float fallMulti = 2.5f;
    public float lowJumpMulti = 2f;

    [Range(0f, 10f)]
    public float jumpForce;

    Animator anim;

    public static PlayerController Instance;

    PlayerStates playerStates;

    public bool unlockedDoubleJump;
    public bool dash = true;
    public bool dashed;
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
        Inputs();
        Move();
        Jump();
        Flip();
        if (playerStates.dashing) return;
        DashStart();

    }

    void Inputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
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

    void DashStart()
    {
        if(Input.GetButtonDown("Dash") && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }

        if (isGrounded())
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        dash = false;
        playerStates.dashing = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        yield return new WaitForSeconds(dashCooldown);
        dash = true;
    }
}
