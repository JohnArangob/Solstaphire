using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("PJ"))
        {
            print("damage");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
