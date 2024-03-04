using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    public bool isUnderground = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUnderground)
        {
            if (player.position.x >= 5f && player.position.x <= 196f)
            {
                transform.position = new Vector3(player.position.x, 3f, -10f);
            }
        }
        
        if (isUnderground)
        {
            transform.position = new Vector3(150f, -13f, -10f);
        }
        
        // x can never be less than -1.4 or more than 196
    }
}
