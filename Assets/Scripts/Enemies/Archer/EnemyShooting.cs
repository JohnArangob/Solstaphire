using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform arrowPos;

    private float timer;
    private GameObject player;

    private Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        float distance = Vector2.Distance(transform.position, player.transform.position);



        if (distance < 10)
        {
            timer += Time.deltaTime;

            
            if (timer > 4)
            {
                timer = 0;
                Shoot();
                               
            }

        }

        
    }

    void Shoot()
    {

        Instantiate(Arrow, arrowPos.position, Quaternion.identity);
    }


}
