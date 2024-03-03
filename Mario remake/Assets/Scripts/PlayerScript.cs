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

    public bool isBig = false;
    private bool hasFire = false;
    private Vector3 originalSize; 

    public Camera camera;
    public CameraMovement CameraMovement;

    public float hp;


    public bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameObject underworldSpawn;

    public GameObject overworldSpawn;

    public bool onPipe = false;
    public bool nextToPipe = false;
    public bool movingLeft = false;
    public bool ending = false;

    public bool marioImmune = false;

    public GameObject FireBall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        CameraMovement = camera.GetComponent<CameraMovement>();
        hp = 1;
    }

    void Update()
    {
        Flip();
        if (rb.velocity.x > 0)
        {
            movingLeft = true;
        }
        else if (rb.velocity.x < 0)
        {
            movingLeft = false;
        }

        if (rb.velocity.x != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            if (movingLeft)
            {
                spriteRenderer.flipX = true;
            }
        }

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

        if (ending && isGrounded)
        {
            animator.StopPlayback();
        }
        if (hasFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootFire();
            }
        }

        //Debug.Log(hp);
        if (hp == 0)
        {
            MarioDie();
        }

        if (hasFire)
        {
            animator.SetBool("fireFlower", true);
        }
        else if (!hasFire)
        {
            animator.SetBool("fireFlower", false);
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Map"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Box"))
        {
            UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            uiManager.boxToCoin(other.gameObject);
        }

        if (other.gameObject.CompareTag("OWPipe"))
        {
            onPipe = false;
            nextToPipe = true;
        }
        if (other.gameObject.CompareTag("UWPipe"))
        {
            onPipe = true;
            nextToPipe = false;
        }
        else if (other.gameObject.CompareTag("Goomba"))
        {
            bool hitFromAbove = false;
            foreach (ContactPoint2D hit in other.contacts)
            {
                if (hit.normal.y > 0.5)
                {
                    hitFromAbove = true;
                    break;
                }
            }

            if (!hitFromAbove && isBig)
            {
                Shrink();
            }
            else if (hitFromAbove)
            {
                Destroy(other.gameObject); 
            }
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
            animator.SetBool("isBig", true);
            transform.localScale = Vector3.Scale(transform.localScale, growthFactor);
            isBig = true;
            
            hp = 2;
            Debug.Log(hp);
        }
    }


   

    void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void Flip()
    {
        if (rb.velocity.x > 0)
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

    private void Shrink()
    {
        if (isBig)
        {
            transform.localScale = originalSize;
            isBig = false;
            animator.SetBool("isBig", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flagpole"))
        {
            ending = true;
            animator.SetBool("flagpoleAnim", true);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.gravityScale = 3f;
        }
    }

    public void FirePower()
    {
        if (!hasFire)
        {
            isBig = true;
            hasFire = true;
            
            hp = 3;
            Debug.Log(hp);
        }
    }

    void ShootFire()
    {
        Instantiate(FireBall, new Vector2 (this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
    }

    public void TakeDamage()
    {
        if (!marioImmune)
        {
            if (hasFire && isBig)
            {
                hp = 2;
            } else if (!hasFire && isBig)
            {
                hp = 1;
            } else if (!hasFire && !isBig)
            {
                hp = 0;
            }
            StartCoroutine(MarioImmune());
            Debug.Log(hp);
        }

        if (hp < 2)
        {
            Shrink();
        }
        if (hp < 3)
        {
            hasFire = false;
            
        }
        
    }

    IEnumerator MarioImmune()
    {
        marioImmune = true;
        yield return new WaitForSeconds(3f);
        marioImmune = false;
    }

    void MarioDie()
    {
        speed = 0;
        jumpForce = 0;
    }
}
