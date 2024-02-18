using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl singleton;
    public Vida vida;
    private void Awake()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
