using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 movement;
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Flip();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0.0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }


        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
    }

    public void Flip()
    {
        if (movement.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
