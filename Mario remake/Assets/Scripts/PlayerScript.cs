using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 15.0f;
    public Vector3 growthFactor = new Vector3(1.5f, 1.5f, 1);
    private Rigidbody2D rb;
    private bool isBig = false;
    private Vector3 originalSize; 
    public GameObject UiManager;
    public bool isGrounded;

    public GameObject underworldSpawn;

    public GameObject overworldSpawn;

    public bool onPipe = false;
    public bool nextToPipe = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
         if (Input.GetKeyDown("s"))
        {
            if (onPipe)
            {
                this.transform.position = underworldSpawn.transform.position;
                //Debug.Log("Pipe");
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (nextToPipe)
            {
                this.transform.position = overworldSpawn.transform.position;
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

   
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Goomba") && isBig)
        {
            Destroy(other.gameObject); 
        }
        if (other.gameObject.CompareTag("Box")) {
            UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiManager.boxToCoin(other.gameObject);
        }
        if (other.gameObject.CompareTag("UWPipe"))
        {
            onPipe = true;
        }
        else if (other.gameObject.CompareTag("OWPipe")) {
             nextToPipe = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("UWPipe") || collision.gameObject.CompareTag("OWPipe"))
        {
            onPipe = false;
            nextToPipe = false;
        }
    }

    public void Grow()
    {
        if (!isBig)
        {
            transform.localScale = Vector3.Scale(transform.localScale, growthFactor); 
            isBig = true;
            StartCoroutine(ShrinkBack());
        }
    }

    IEnumerator ShrinkBack()
    {
        yield return new WaitForSeconds(5); 
        transform.localScale = originalSize; 
        isBig = false;
    }

    void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
