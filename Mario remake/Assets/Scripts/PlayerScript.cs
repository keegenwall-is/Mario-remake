using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    private float moveHorizontal;

    public GameObject UiManager;
    public float jumpForce = 15.0f;

    private Rigidbody2D rb;
    public bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameObject underworldSpawn;

    public GameObject overworldSpawn;

    public bool onPipe = false;
    public bool nextToPipe = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Flip();
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
        }
        else if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
        }
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

    public void Flip()
    {
        if (rb.velocity.x >= 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
