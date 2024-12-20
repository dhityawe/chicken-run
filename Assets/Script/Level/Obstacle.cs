using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    [Header("Game Over Panel")]
    public GameObject gameOverPanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the PlayerMovement component
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.PlayerDead();

            // Start coroutine to delay activating the Game Over panel
            StartCoroutine(ActivateGameOverPanelAfterDelay());
        }
    }

    private IEnumerator ActivateGameOverPanelAfterDelay()
    {
        yield return new WaitForSeconds(0.4f);
        gameOverPanel.SetActive(true);      // Activate the Game Over panel
        // destroy current script component
        Destroy(this);
    }
}
