using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FireBalls : MonoBehaviour
{
    public float bounceForce = 5f;
    public float upwardFactor = 1.0f;
    public float forwardFactor = 1.0f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1f, 1f);

        StartCoroutine(LifeSpan());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflectionDirection = Vector2.Reflect(rb.velocity, normal).normalized;
            Vector2 forwardComponent = rb.velocity.normalized * forwardFactor;
            Vector2 upwardComponent = new Vector2(0f, 1f) * upwardFactor;
            Vector2 combinedDirection = reflectionDirection + forwardComponent + upwardComponent;

            combinedDirection = combinedDirection.normalized * rb.velocity.magnitude;

            rb.velocity = combinedDirection.normalized * bounceForce;
        }
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
