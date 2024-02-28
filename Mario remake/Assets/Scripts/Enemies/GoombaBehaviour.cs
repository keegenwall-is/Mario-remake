using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaBehaviour : MonoBehaviour
{

    float moveX = -1;

    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    public bool canMove;

    public string mapTag = "Map";
    public string wallTag = "Wall";
    public string goombaTag = "Goomba";

    //bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDirection();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    void MoveDirection()
    {
        moveDirection = new Vector2(moveX, 0).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == mapTag)
        {
            canMove = true;
        }

        if (collision.tag == wallTag || collision.tag == goombaTag)
        {
            moveX = -moveX;
        }

        if (collision.tag == goombaTag)
        {
            moveX = -moveX;
            Debug.Log("YO");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == mapTag)
        {
            canMove = false;
        }
    }
}
