using UnityEngine;
using GabrielBigardi.SpriteAnimator;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public bool allowMovement = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public SpriteAnimator SpriteAnimator;
    private bool isGrounded;

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpriteAnimator = GetComponent<SpriteAnimator>();
    }

    void Update()
    {
        if (allowMovement)
        {
            Move();
            FlipSprite();
            Jump();
            CheckGround();
        }
        else
        {
            rb.velocity = Vector2.zero;
            SpriteAnimator.Play("Idle");
        }
    }

    private void Move()
    {
        // if A or D is pressed, moveInput = -1 or 1
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            SpriteAnimator.PlayIfNotPlaying("Walk");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            SpriteAnimator.PlayIfNotPlaying("Walk");
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            SpriteAnimator.Play("Idle");
        }
    }

    private void FlipSprite()
    {
        if (rb.velocity.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (rb.velocity.x < -0.1f)
            spriteRenderer.flipX = true;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}
