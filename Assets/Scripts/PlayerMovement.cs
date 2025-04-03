using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float sprintMultiplier = 1.5f;
    private bool isGrounded;
    private Rigidbody rb;
    private MovingPlatform currentPlatform;
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

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            anim.SetBool("jump", true);
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
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            gameManager.TakeDamage(1);
        }
    }
}

