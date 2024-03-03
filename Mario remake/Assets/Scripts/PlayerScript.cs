using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    private float moveHorizontal;

    public GameObject UiManager;
    public float jumpForce = 15.0f;
    public Vector3 growthFactor = new Vector3(1.5f, 1.5f, 1);
    private Rigidbody2D rb;

    private bool isBig = false;
    private Vector3 originalSize; 

    public Camera camera;
    public CameraMovement CameraMovement;


    public bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameObject underworldSpawn;

    public GameObject overworldSpawn;

    public bool onPipe = false;
    public bool nextToPipe = false;

    public int hp = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        CameraMovement = camera.GetComponent<CameraMovement>();
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
            Jump();
        }
         if (Input.GetKeyDown("s"))
        {
            if (onPipe)
            {
                CameraMovement.isUnderground = true;
                this.transform.position = underworldSpawn.transform.position;
                //Debug.Log("Pipe");
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (nextToPipe)
            {
                CameraMovement.isUnderground = false;
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
        } else if (other.gameObject.CompareTag("OWPipe")) {
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
            hp += hp;
            //StartCoroutine(ShrinkBack());
        }
    }

    public void ShrinkBack()
    {
        //yield return new WaitForSeconds(5); 
        //yield return new WaitForSeconds(5); 
        transform.localScale = originalSize; 
        isBig = false;
        hp -= hp;
    }

    void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }
}
