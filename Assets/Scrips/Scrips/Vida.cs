using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vida : MonoBehaviour
{
    public float vidaInicial;
    public float vidaActual;

    public UnityEvent eventoMorir;

    void Start()
    {
        vidaActual = vidaInicial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CausarDaño(float cuanto)
    {
        vidaActual -= cuanto;
        if (vidaActual<=0)
        {
            print("Muerto" + gameObject.name);
            eventoMorir.Invoke();
        }
    }
}
