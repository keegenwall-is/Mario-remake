using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShroom : MonoBehaviour
{
    public float speed = 2.0f;
    private bool isActivated = false;

    public GameObject player;

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
            PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
            player.Recoverlevelup();
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.transform.localScale = new Vector3(2,2);
            collision.gameObject.GetComponent<PlayerScript>().Grow();
            Destroy(gameObject);
        }
    }
}
