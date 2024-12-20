using UnityEngine;
using GabrielBigardi.SpriteAnimator;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float spawnPointX = -13f;
    public float spawnPointY = -6.73f;

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
            // disable the x velocity
            rb.velocity = new Vector2(0, rb.velocity.y);
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

    public void PlayerDead()
    {
         SpriteAnimator.Play("Dead");
        allowMovement = false;
        transform.position = new Vector2(transform.position.x, transform.position.y + 2);
    }

    public void BackToSpawn()
    {
        allowMovement = true;
        transform.position = new Vector2(spawnPointX, spawnPointY);
    }

    public void InvulnerableTime()
    {
        // start coroutine to make the player invulnerable for 1 second
        StartCoroutine(Invulnerable());
    }

    private IEnumerator Invulnerable()
    {
        allowMovement = false; // Disable player movement
        float duration = 2f; // Total duration of invulnerability
        float changeInterval = 0.3f; // Time to transition between opacity levels
        float elapsedTime = 0f;

        // While invulnerability duration has not elapsed
        while (elapsedTime < duration)
        {
            // Fade out to 100 opacity
            yield return FadeOpacity(250f / 255f, 100f / 255f, changeInterval);

            // Fade in to 250 opacity
            yield return FadeOpacity(100f / 255f, 250f / 255f, changeInterval);

            elapsedTime += 2 * changeInterval; // Account for both fade-out and fade-in durations
        }

        // Reset sprite opacity to full (250)
        spriteRenderer.color = new Color(
            spriteRenderer.color.r, 
            spriteRenderer.color.g, 
            spriteRenderer.color.b, 
            250f / 255f
        );

        allowMovement = true; // Re-enable player movement
    }

    private IEnumerator FadeOpacity(float from, float to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate the opacity
            float currentOpacity = Mathf.Lerp(from, to, elapsedTime / duration);
            spriteRenderer.color = new Color(
                spriteRenderer.color.r, 
                spriteRenderer.color.g, 
                spriteRenderer.color.b, 
                currentOpacity
            );

            yield return null; // Wait for the next frame
        }
    }




}
