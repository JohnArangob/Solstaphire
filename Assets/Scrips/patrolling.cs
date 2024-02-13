using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    private Rigidbody2D rbEnemy;
    public float speed;
    public float attack;
    public float seguir;

    // Start is called before the first frame update
    private void Awake()
    {
        rbEnemy = GetComponent<Rigidbody2D>();

      
    }

    // Update is called once per frame
    void Update()
    {
        rbEnemy.velocity = new Vector2(speed, rbEnemy.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            speed *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1,this.transform.localScale.y);
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attack);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position,seguir);
        
    }
}
