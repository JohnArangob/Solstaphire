using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D ArrowRb;
    public float force;

    [HideInInspector] public HealthManager healthManager;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        ArrowRb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = Player.transform.position - transform.position;
        ArrowRb.velocity = new Vector2 (direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, rot);

        healthManager = FindObjectOfType<HealthManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10) {
            Destroy(gameObject);        
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            healthManager.TakeDamageArrow();
            Destroy(gameObject);
        }
    }

}
