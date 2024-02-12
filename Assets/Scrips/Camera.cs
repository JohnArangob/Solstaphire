using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 posicionrelativa;
    // Start is called before the first frame update
    private void Awake()
    {
        posicionrelativa = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + posicionrelativa;
    }
}
