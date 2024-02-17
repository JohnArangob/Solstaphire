using System.Collections;
using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    private Rigidbody2D _rbEnemy;
    public float speed;
    public float seguir;
    public Transform target;
    public LayerMask player;

    private void Awake()
    {
        _rbEnemy = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Comprobar si el objetivo está dentro del rango
        bool alert = Physics2D.OverlapCircle(transform.position, seguir, player);

        if (alert && target != null)
        {
            // Calcular la dirección hacia el objetivo
            Vector2 direction = (target.position - transform.position).normalized;

            // Mover el enemigo en dirección al objetivo
            _rbEnemy.velocity = direction * speed;

            // Voltear el sprite del enemigo si es necesario
            if (direction.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Si el objetivo no está en el rango, el enemigo no se mueve
            _rbEnemy.velocity = Vector2.zero;
        }
    }

    // Dibuja un gizmo para visualizar el rango en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seguir);
    }
}

