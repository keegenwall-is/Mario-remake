using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShroom : MonoBehaviour
{
    public float speed = 2.0f;
    private bool isActivated = false;

    public void ActivateMushroom()
    {
        isActivated = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        if (isActivated)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().Grow();
            Destroy(gameObject);
        }
    }
}
