using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float sprintMultiplier = 1.5f;

    private bool isGrounded;
    private bool canDoubleJump = false;
    private bool hasDoubleJump = false;

    private Rigidbody rb;
    private MovingPlatform currentPlatform;
    private MovingWall currentWall;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isMoving = horizontal != 0 || vertical != 0;

        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * 200f * Time.deltaTime);

        // Sprinting
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        float speed = moveSpeed * (isRunning ? sprintMultiplier : 1f);

        // Moving
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        transform.position += moveDirection * speed * Time.deltaTime;

        // Apply platform movement if on a platform
        if (currentPlatform != null)
        {
            transform.position += currentPlatform.GetPlatformMovement();
        }

        // Apply wall movement if colliding with one
        if (currentWall != null)
        {
            transform.position += currentWall.GetWallMovement();
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                canDoubleJump = hasDoubleJump;
                anim.SetBool("jump", true);
            }
            else if (canDoubleJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = false;
                anim.SetBool("doubleJump", true);
            }
        }

        // Set Animator Booleans
        anim.SetBool("walk", isMoving && !isRunning);
        anim.SetBool("run", isRunning);

        if (!isMoving) // If not moving, disable walking and running
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }
    }

    // Detect ground or platform contact
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("jump", false);
        }
        else if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;
            currentPlatform = collision.gameObject.GetComponent<MovingPlatform>();
            anim.SetBool("jump", false);
        }
        else if (collision.gameObject.CompareTag("MovingWall"))
        {
            currentWall = collision.gameObject.GetComponent<MovingWall>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
        else if (collision.gameObject.CompareTag("MovingWall"))
        {
            currentWall = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            gameManager.TakeDamage(1);
        }
    }

    public void EnableDoubleJump()
    {
        hasDoubleJump = true;
    }

    public void DoubleJumpComplete()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        anim.SetBool("doubleJump", false);
    }

}

