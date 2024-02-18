using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Header ("Horizontal Movement Settings")]
    private string HorizontalAxis = "Horizontal";
    [SerializeField] float mSpeed = 3.0f;
    private float hMovement;
    [Space]

    [Header ("Groundcheck Settings")]
    private float jumpForceVector = 10f;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] float groundCheckY = 0.2f;
    [SerializeField] float groundCheckX = 0.5f;
    [SerializeField] LayerMask Ground;
    [Space]

    [Header("Animator")]
    [HideInInspector] public static Animator playerAnimator;
    [Space]
    
    [Header("Attacking")]
    bool attack = false;
    float timeBetweenAttack, timeSinceAttack;
    public bool weapon;
    [SerializeField] Transform AttackTransform;
    [SerializeField] Vector2 AttackArea;
    [SerializeField] LayerMask attackableLayer;
    [SerializeField] float damage;
    [Space]

    [Header("Coin")]
    public CoinManager cm;
    [Space]
    public HealthManager healthManager;

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
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(AttackTransform.position, AttackArea);
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

        Weapon();

        Sheat();

        Attack();
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if(weapon == true)
            {
                playerAnimator.SetTrigger("HitWeapon");
            }
            else
            {
                playerAnimator.SetTrigger("HitA");
            }
        }
    }

    void GetInputs()
    {
        hMovement = Input.GetAxisRaw(HorizontalAxis);
        attack = Input.GetKeyDown(KeyCode.Z);
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

    void Weapon()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            weapon = true;
            playerAnimator.SetBool("Weapon", true);

        }
        
    }

    void Sheat()
    {
        weapon = false;
        if (Input.GetKeyDown(KeyCode.G))
        {
            playerAnimator.SetBool("Weapon", false);
        }
    }

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        
        
        if(attack && timeSinceAttack >= timeBetweenAttack)
        {
            timeSinceAttack = 0;
            playerAnimator.SetTrigger("AttackA");
            Hit(AttackTransform, AttackArea);
        }
    }

    void Hit(Transform AttackTransform, Vector2 AttackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(AttackTransform.position, AttackArea, 0, attackableLayer);

        if(objectsToHit.Length > 0)
        {
            Debug.Log("Hit");
        } 
        for(int i = 0; i < objectsToHit.Length; i++)
        {
            if (objectsToHit[i].GetComponent<Enemy>() != null)
            {
                objectsToHit[i].GetComponent<Enemy>().EnemyHit(damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
            
        }
    }
}
