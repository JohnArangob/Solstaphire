using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Sign;

    public CoinManager cm;

    bool InTheDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        cm = FindObjectOfType<CoinManager>();
        Sign.SetActive(false);
        Door.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cm.coinCount == 4 && InTheDoor)
        {
            Door.SetActive(false);
            Sign.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        { 
            InTheDoor = true;
            Sign.SetActive(true);
            Debug.Log(cm.coinCount);
            
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InTheDoor = false;
            Sign.SetActive(false);
        }
    }
}
