using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform arrowPos;
    [SerializeField] SpriteRenderer _sr;

    private float timer;
    private GameObject player;

    private Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
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
            timer += Time.deltaTime;
            

            if(timer >= 2)
            {
                Anim.SetBool("Attack", true);
                Anim.SetBool("Idle", false);
                Anim.SetBool("Rest", false);
            }
            
            if (timer > 3)
            {
                timer = 0;
                Shoot();
                Anim.SetBool("Attack", false);
                Anim.SetBool("Rest", true);
                Anim.SetBool("Idle", true);
            }

        }else {
            Anim.SetBool("Idle", true);
            Anim.SetBool("Attack", false);
            Anim.SetBool("Rest", false);
        }

        
    }

    void Shoot()
    {
        
        Instantiate(Arrow, arrowPos.position, Quaternion.identity);
    }


}
