using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;

    public GameObject UiManager;
    public float jumpForce = 15.0f;

    private Rigidbody2D rb;
    public bool isGrounded;

    public GameObject underworldSpawn;

    public GameObject overworldSpawn;

    public bool onPipe = false;
    public bool nextToPipe = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    void ReloadCurrentScene()
    {
        // 获取当前场景的索引，并重新加载该场景
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //Debug.Log("Grounded");
        }

        if (other.gameObject.CompareTag("Box"))
        {
            UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiManager.boxToCoin(other.gameObject);
        }

        if (other.gameObject.CompareTag("UWPipe"))
        {
            onPipe = true;
        }
        else if (other.gameObject.CompareTag("OWPipe"))
        {
            nextToPipe = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UWPipe") || collision.gameObject.CompareTag("OWPipe"))
        {
            onPipe = false;
            nextToPipe = false;
        }
    }
}
