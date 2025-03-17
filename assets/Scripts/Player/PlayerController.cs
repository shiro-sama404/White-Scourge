using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public int maxJumps = 2;
    public float climbSpeed = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isDashing = false;
    private bool canDash = true;
    private int jumpsRemaining;
    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        HandleMovement();
        HandleDash();
        HandleJump();
        HandleClimb();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (!isDashing && !isClimbing)
        {
            Vector2 movement = new Vector2(moveX, 0).normalized * moveSpeed;
            rb.velocity = new Vector2(movement.x, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(movement.x));
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            jumpsRemaining--;
        }
    }

    void HandleClimb()
    {
        if (isClimbing)
        {
            float moveY = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, moveY * climbSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            isClimbing = true;
            rb.gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            isClimbing = false;
            rb.gravityScale = 3;
        }
    }
}