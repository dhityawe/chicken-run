using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Game Over Panel")]
    public GameObject gameOverPanel;

    public bool isMoving = false; // Check if the obstacle is moving

    public UiScore uiScore; // Reference to the UiScore script component

    private Vector3 originalPosition; // Store the original position
    private float moveDuration = 1.5f; // Duration for one way of movement (up or down)

    private void Start() 
    {
        uiScore = FindObjectOfType<UiScore>();
        originalPosition = transform.position; // Save the initial position
    }

    private void Update()
    {
        if (isMoving)
        {
            MovingObstacle(); // Call the movement logic
        }
    }

    private IEnumerator SmoothMove(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate position
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);

            yield return null; // Wait for the next frame
        }

        // Ensure the final position is exact
        transform.position = endPosition;
    }

    private void MovingObstacle()
    {
        isMoving = false; // Prevent multiple coroutine starts

        // Calculate target positions
        Vector3 targetPosition = originalPosition + new Vector3(0, 2, 0);

        // Start a coroutine for smooth up and down movement
        StartCoroutine(MoveCycle(targetPosition));
    }

    private IEnumerator MoveCycle(Vector3 targetPosition)
    {
        // Move up
        yield return StartCoroutine(SmoothMove(originalPosition, targetPosition, moveDuration));

        // Move down
        yield return StartCoroutine(SmoothMove(targetPosition, originalPosition, moveDuration));

        // Reactivate isMoving for continuous movement
        yield return new WaitForSeconds(0.5f); // Small pause at the original position if needed
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the PlayerMovement component
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (uiScore.health > 1)
            {
                uiScore.DecreaseHealth(1);
                playerMovement.BackToSpawn();
                playerMovement.InvulnerableTime();
            }
            else
            {
                AudioManager.Instance.PlaySFX(1); // Play the death sound effect
                playerMovement.PlayerDead();

                // Start coroutine to delay activating the Game Over panel
                StartCoroutine(ActivateGameOverPanelAfterDelay());
            }
        }
    }

    private IEnumerator ActivateGameOverPanelAfterDelay()
    {
        yield return new WaitForSeconds(0.4f);
        gameOverPanel.SetActive(true); // Activate the Game Over panel
        // destroy current script component
        Destroy(this);
    }
}
