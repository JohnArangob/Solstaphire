using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementPlayer : MonoBehaviour
{
    public GameObject character;
    public float Speed;
    private float MinSpeed=6;
    public Rigidbody2D rb;
    public float horizontalForce = 60.0f;
    [Range(1, 500)] public float potenciaSalto;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
    }

    private void Update()

    {

        float movement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * movement * Speed * Time.deltaTime);
        character.transform.position = transform.position;
       
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            print("Sprint");
            rb.AddForce(Vector2.right * MinSpeed, ForceMode2D.Impulse);
            rb.isKinematic = true;
        }else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * MinSpeed, ForceMode2D.Impulse);
            rb.isKinematic = true;
        }else if (Input.GetKeyDown(KeyCode.Space))
        {
            print("salto");
            rb.AddForce(Vector2.up * Speed, ForceMode2D.Impulse);
        }
        
        rb.isKinematic = false;
    }

}

