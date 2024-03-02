using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 100.0f;

    public GameObject UiManager;
    private Rigidbody2D rb;
    public bool isGrounded;
    private Vector2 movement;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Transform groundedCheck;
    private ContactFilter2D filter;
    private Collider2D[] colliders = new Collider2D[1];

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundedCheck = GameObject.FindWithTag("GroundedCheck").transform;
    }

    private void Update()
    {
        Flip();
        IsGrounded();
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector2(moveHorizontal, 0.0f);
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + movement * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Debug.Log("jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void ReloadCurrentScene()
    {
        // 获取当前场景的索引，并重新加载该场景
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }*/

    private void IsGrounded()
    {
        if (Physics2D.OverlapBox(groundedCheck.position, groundedCheck.localScale, 0, filter, colliders) > 0)
        {
            isGrounded = true;
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
    void OnCollisionEnter2D(Collision2D collider)
        {
            if (collider.gameObject.CompareTag("Box"))
                {
                    UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                    uiManager.boxToCoin(collider.gameObject);
                }
        }

    }


