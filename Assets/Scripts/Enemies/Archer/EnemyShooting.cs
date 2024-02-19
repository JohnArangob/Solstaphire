using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform arrowPos;
    private SpriteRenderer _sr;

    private float timerShoot;
    private float timerIdle;
    private GameObject player;

    private Animator Anim;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
    

    float distance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.x < transform.position.x)
        {
            _sr.flipX = false;
        }
        else
        {
            _sr.flipX= true;
        }


        if (distance < 10) 
        {
            timerShoot += Time.deltaTime;

            if(timerShoot >= 1f)
            {
                Anim.SetBool("Attack", true);
                Anim.SetBool("Idle", false);
                Anim.SetBool("Rest", false);
            }
            
            if (timerShoot > 1.2f)
            {
                timerShoot = 0;
                Shoot();
                Anim.SetBool("Attack", false);
                Anim.SetBool("Rest", true);
                Anim.SetBool("Idle", true);
            }

        }


        
    }

    void Shoot()
    {
        
        Instantiate(Arrow, arrowPos.position, Quaternion.identity);
    }

    
}
