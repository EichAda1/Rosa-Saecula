using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [Header("Horizontal Movement")]
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;
    private float horizontal;
    private float speed;

    public bool _interact = false;
    private Vector3 playerPosition;
    private Transform playerTransform;

    public int _currentAge = 0;
    public float yAxis;

    //[SerializeField]private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckX = 0.2f;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private LayerMask isGround;
    [SerializeField] private Cooldown cooldown;

    [SerializeField] private int maxJumps = 1;
    private int jumpsLeft;

    [SerializeField] private float dashingPower = 30f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    public float fallMulti = 2.5f;
    public float lowJumpMulti = 2f;

    [Range(0f, 10f)]
    public float jumpForce;
    private bool Doublejump = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 48f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    Animator anim;

    public static PlayerController Instance { get; private set; }


    PlayerStates playerStates;

    public bool Doublejump;
    public bool canDash = true;
    public bool isDashing;
    public float gravity;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Only one Player is allowed. Newest instance destroyed.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        jumpsLeft = maxJumps;

        gravity = rb.gravityScale;

        if (DataPersistenceManager.instance.gameData.playerPosition != Vector3.zero)
        {
            Instance.transform.position = DataPersistenceManager.instance.gameData.playerPosition;
        }
        else
        {
            Instance.transform.position = DataPersistenceManager.instance.gameData.playerPosition + new Vector3(0,2.255f, 0);
        }
        CameraFollow.Instance.transform.position = Vector3.Lerp(transform.position, Instance.transform.position + CameraFollow.Instance.offset, CameraFollow.Instance.followSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
<<<<<<< Updated upstream
=======
        Crouch();
>>>>>>> Stashed changes

        if (isDashing)
        {
            return;
        }

<<<<<<< Updated upstream
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
=======
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
=======
        if (Input.GetKeyDown(KeyCode.R) && canDash)
>>>>>>> Stashed changes
        {
            StartCoroutine(Dash());
        }

<<<<<<< Updated upstream
        
=======
>>>>>>> Stashed changes
        Flip();
        

        Interact();
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
        yAxis = Input.GetAxis("Vertical"); //used for interact and dropping through platforms
    }

    public bool Interact()
    {       

        if (IsGrounded() && !cooldown.isCoolingDown())
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                _interact = true;
                cooldown.StartCooldown();
            }
            else
            {
                _interact = false;
            }
        }
        else
        {
            _interact = false;
        }

        return _interact;
    } 

    private void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool IsGrounded()
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
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = Vector2.up * jumpForce;
            maxJumps -= 1;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += (fallMulti - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += (lowJumpMulti - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SaveTile") && Interact())
        {
            DataPersistenceManager.instance.SaveGame();
            Debug.Log("Game Saved");
        }
        if (collision.CompareTag("AgeTile") && Interact())
        {
            if (_currentAge >= 0 && _currentAge < 2)
            {
                _currentAge++;
                Debug.Log("Age changed.");
            }
            else if (_currentAge >= 2)
            {
                _currentAge = 0;
                Debug.Log("Age changed.");
            }
            else
            {
                Debug.Log("Error");
            }
            
        }

        if (collision.CompareTag("Platform") && IsGrounded() && yAxis > 0)
        {
            //TODO - drop through platform
        }

        //if (collision.CompareTag("NPC") && isGrounded && yAxis > 0)
        //{

        //} 
    }

<<<<<<< Updated upstream
=======
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(xAxis * walkSpeed, rb.velocity.y);
    }

    private IEnumerator Dash()
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

>>>>>>> Stashed changes
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;        
    }
    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerTransform = this.transform;
        data.sceneID = SceneManager.GetActiveScene().buildIndex;
    }
}
