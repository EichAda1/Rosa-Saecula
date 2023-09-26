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

    public float fallMulti = 2.5f;
    public float lowJumpMulti = 2f;

    [Range(0f, 10f)]
    public float jumpForce;

    Animator anim;

    public static PlayerController instance { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Only one Player is allowed. Newest instance destroyed.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (DataPersistenceManager.instance.gameData.playerPosition != Vector3.zero)
        {
            instance.transform.position = DataPersistenceManager.instance.gameData.playerPosition;
        }
        else
        {
            instance.transform.position = DataPersistenceManager.instance.gameData.playerPosition + new Vector3(0,2.255f, 0);
        }
        CameraFollow.Instance.transform.position = Vector3.Lerp(transform.position, instance.transform.position + CameraFollow.Instance.offset, CameraFollow.Instance.followSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        Inputs();
        Move();
        Jump();
        Flip();
        Interact();
    }

    void Inputs()
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
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
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

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = Vector2.up * jumpForce;
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
