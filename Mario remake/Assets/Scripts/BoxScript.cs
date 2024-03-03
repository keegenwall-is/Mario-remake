using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private PlayerScript PlayerScript;
    public GameObject shroom;
    public GameObject fireFlower;
    void Start()
    {
        PlayerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (!PlayerScript.isBig)
            {
            StartCoroutine(Rise(shroom));
        }
            else
            {
                StartCoroutine(Rise(fireFlower));
            }
        
    }

    public IEnumerator Rise(GameObject obj)
    {
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            obj.transform.localPosition = new Vector2(0, Mathf.Lerp(0, 1, (elapsedTime / 1)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
