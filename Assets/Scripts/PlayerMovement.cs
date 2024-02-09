using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Header ("Horizontal Movement Settings")]
    private string HorizontalAxis = "Horizontal";
    [SerializeField] float mSpeed = 3.0f;
    private float hMovement;

    [Header ("Groundcheck Settings")]
    private float jumpForceVector = 10f;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] float groundCheckY = 0.2f;
    [SerializeField] float groundCheckX = 0.5f;
    [SerializeField] LayerMask Ground;

    [Header("Animator")]
    Animator playerAnimator;


    public static PlayerMovement Instance;

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


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Functions

        GetInputs(); //Check for Inputs

        Move(); //Horizontal Movement Function

        Grounded(); //Ground Check

        Jump(); //Jump Function

        Flip(); //Flip the character when he runs to the other direction

    }

    void GetInputs()
    {
        hMovement = Input.GetAxisRaw(HorizontalAxis);
    }

    void Flip()
    {
        if(hMovement < 0)
        {
            transform.localScale = new Vector2(-3, transform.localScale.y);
        }else if(hMovement > 0)
        {
            transform.localScale = new Vector2(3, transform.localScale.y);
        }
    }

    void Move()
    {
        _rb.velocity = new Vector2(mSpeed * hMovement, _rb.velocity.y);
        playerAnimator.SetBool("Walking", _rb.velocity.x !=0 && Grounded());
    }    
   
    //Check Ground
    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, Ground) 
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, Ground) 
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, Ground))
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
        if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);  //Salto variable
        }

        if (Input.GetButtonDown("Jump") && Grounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForceVector);
        }


        playerAnimator.SetBool("Jump", !Grounded());
    }

}
