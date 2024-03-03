using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaDestroy : MonoBehaviour
{

    public GameObject player;
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.transform.parent.gameObject);
            playerScript.Jump();
        }
    }

}
