using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MovementPlayer : MonoBehaviour
{
    public Collider2D playes;
    private Collider2D others = FindObjectOfType<Collider2D>();

    public GameObject character;

    public float Speed;
    private float MinSpeed = 11f;
    public Rigidbody2D rb;
    public float horizontalForce = 60.0f;
    [Range(1, 500)] public float potenciaSalto;
    bool saltos = false;
    private float gravedad;

    private float dashTime = 0.2f;
    private float dashForce = 5f;
    private float timeDash = 1.5f;
    private float timeJump = 1f;
    private float jumpForce = 7f;
    private bool dashing;
    private bool canDash = true;
    private bool Jump = true;
    private float movement;
    private bool ignore = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravedad = rb.gravityScale;



    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * movement * Speed * Time.deltaTime);
        character.transform.position = transform.position;

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            StartCoroutine(invulnerableRight());
            StartCoroutine(invulnerable());
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            StartCoroutine(invulnerableLeft());
            StartCoroutine(invulnerable());

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Salto());
            
        }

    }

    IEnumerator invulnerableRight()
    {
        if (canDash)

        {
            dashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            print("Sprint");
            rb.velocity = new Vector2(movement * dashForce, 0f);
            yield return new WaitForSeconds(dashTime);
            dashing = false;
            rb.gravityScale = gravedad;
            yield return new WaitForSeconds(timeDash);
            canDash = true;
        }
    }

    IEnumerator invulnerableLeft()
    {
        if (canDash)
        {

            dashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            print("Sprint");
            rb.velocity = new Vector2(movement * dashForce, 0f);
            yield return new WaitForSeconds(dashTime);
            dashing = false;
            rb.gravityScale = gravedad;
            yield return new WaitForSeconds(timeDash);
            canDash = true;

        }

    }
    IEnumerator Salto()
    {
        if (Jump)
        {
            dashing = true;
            Jump = false;
            print("salto");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dashTime);
            dashing = false;
            yield return new WaitForSeconds(timeJump);
            Jump = true;
        }

    }
    IEnumerator invulnerable()
    {
        if (ignore = false)
        {
            ignore = true;
            Physics2D.IgnoreCollision(playes, others, ignore);
            ignore = false;
            yield return new WaitForSeconds(timeDash);
        }
    }
}
