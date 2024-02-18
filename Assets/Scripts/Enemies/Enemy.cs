using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float recoilLength;
    [SerializeField] float recoilFactor;
    [SerializeField] bool isRecoiling = false;

    float timer;

    float recoilTimer;
    Rigidbody2D rb;

    [HideInInspector] public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {          
                Destroy(gameObject);
            
        }
        if (isRecoiling)
        {
            if(recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
    }

    public void EnemyHit(float damageDone, Vector2 hitDirection, float hitForce)
    {
        health -= damageDone;
        if (!isRecoiling)
        {
            rb.AddForce(-hitForce * recoilFactor * hitDirection);
        }
    }

    internal void EnemyHit(object damageDone)
    {
        throw new NotImplementedException();
    }
}
