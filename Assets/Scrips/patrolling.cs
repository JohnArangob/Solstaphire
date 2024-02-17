using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    private Rigidbody2D _rbEnemy;
    public float speed;
    public float attack;
    public float seguir;
    public Transform target;
    public bool autoSeleccionarTarget = true;
    public float distancia;
    public bool perseguirPlayer;

    public LayerMask player;
    bool alert;
    public Animator Enemy;
    public bool ataquess = true;
    public float damage;
    public bool canAttack;

    private void Awake()
    {

        _rbEnemy = GetComponent<Rigidbody2D>();

        StartCoroutine(CalcularDistancia());

    }
    
    private void LateUpdate()
    {
        alert = Physics.CheckSphere(transform.position, seguir, player);

        if (perseguirPlayer)
        {
            float directionX = Mathf.Clamp(target.position.x - _rbEnemy.transform.position.x, -1, 1);
            Vector2 direction = new Vector2(directionX * Mathf.Abs(speed), _rbEnemy.velocity.y);

            _rbEnemy.velocity = direction;
        }
        else
        {
            _rbEnemy.velocity = new Vector2(speed, _rbEnemy.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            Colision();
        }
        if (other.gameObject.tag == "PJ")
        {
            perseguirPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "PJ")
        {
            if (Math.Abs(target.transform.position.x - _rbEnemy.transform.position.x) < (attack) && canAttack)
            {
                StartCoroutine(ataques());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D Exit)
    {
        print("Entro");
        if (Exit.gameObject.tag == "PJ")
        {

            perseguirPlayer = false;
        }
    }
    


    void Colision()
    {
       
        speed *= -1;
        this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
        
    }
    IEnumerator ataques()
    {

        if (canAttack)
        {
            canAttack = false;
            print("Ataques");
            ataquess = false;
            _rbEnemy.velocity = Vector2.zero;
            Enemy.SetBool("attack", true);
            yield return new WaitForSeconds(2f);
            damage = 40;
            ataquess = true;
            canAttack = true;
        }


    }
    IEnumerator CalcularDistancia()
    {
        while (true)
        {
            if (target != null)
            {
                distancia = Vector2.Distance(transform.position, target.position);
            }
            yield return new WaitForSeconds(0.4f);
        }
    }
    
    
}
