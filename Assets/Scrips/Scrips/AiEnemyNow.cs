using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemyNow : MonoBehaviour
{
    public Estados estado;
    public float speed;
    public float distanciaSeguir;
    public float distanciaAtacar;
    public float distanciaEscapar;
    private float _distancia;
    public bool vivo;
    private Rigidbody2D _rbEnemy;
    public float direction = 1;

    public float daño = 40f;


    void Start()
    {
        _rbEnemy = GetComponent<Rigidbody2D>();
        StartCoroutine(CalcularDistancia());
    }


    void Update()
    {
        switch (estado)
        {
            case Estados.patrullar:
                EstadoPatrullar();
                break;
            case Estados.seguir:
                EstadoSeguir();
                break;
            case Estados.atacar:
                EstadoAtacar();
                break;
            case Estados.muerto:
                EstadoMuerto();
                break;
            default:
                break;
        }
    }
    void EstadoSeguir()
    {
        float directionX = Mathf.Clamp(PlayerControl.singleton.transform.position.x - _rbEnemy.transform.position.x, -1, 1);
        Vector2 direction = new Vector2(directionX * Mathf.Abs(speed), _rbEnemy.velocity.y);

        _rbEnemy.velocity = direction;
        if (_distancia < distanciaAtacar)
        {
            CambiarEstado(Estados.atacar);
        }
        else if (_distancia > distanciaEscapar)
        {
            CambiarEstado(Estados.patrullar);
        }
    }
    void EstadoAtacar()
    {
        //Causar daño al jugador
        Atacar();
        if (_distancia > distanciaAtacar + 0.5f)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    void EstadoPatrullar()
    {
        _rbEnemy.velocity = new Vector2(speed * direction, _rbEnemy.velocity.y * Time.deltaTime);
        if (_distancia < distanciaSeguir)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    void EstadoMuerto()
    {

    }
    void CambiarEstado(Estados estados)
    {

        estado = estados;
        //Es como el start de cada estado
        switch (estado)
        {
            case Estados.patrullar:
                //operador ternario
                direction = (Random.Range(0f, 1f) > 0.5) ? 1 : -1;
                break;
            case Estados.seguir:
                break;
            case Estados.atacar:
                break;
            case Estados.muerto:
                break;
            default:
                break;
        }
    }
    IEnumerator CalcularDistancia()
    {
        while (vivo)
        {
            _distancia = Vector3.Distance(transform.position, PlayerControl.singleton.transform.position);
            yield return new WaitForSeconds(0.2F);
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
            //perseguirPlayer = true;
        }
    }
    void Colision()
    {

        speed *= -1;
        this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, distanciaAtacar);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, distanciaSeguir);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, distanciaEscapar);
    } 
    public void Atacar()
    {
        if (_distancia < distanciaAtacar)
        {
            PlayerControl.singleton.vida.CausarDaño(daño);
        }
    }
}

public enum Estados
{
    patrullar = 0,
    seguir = 1,
    atacar = 2,
    muerto = 3
}