using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x >= -1.4f && player.position.x <= 196f)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, -10f);
        }
        
        // x can never be less than -1.4 or more than 196
    }
}
