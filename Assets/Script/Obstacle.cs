using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Game Over Panel")]
    public GameObject gameOverPanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the PlayerMovement component
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            // Activate the Game Over Panel
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }
}
