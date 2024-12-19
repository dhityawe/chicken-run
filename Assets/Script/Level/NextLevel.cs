using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    [Header("Win Transition Settings")]
    public GameObject winGamePanel; // The Win Panel
    public Image winGamePanelImage; // The Panel's Image component for opacity control
    public float transitionDuration = 2f; // Transition time in seconds

    private bool isTransitioning = false; // To prevent multiple triggers
    public PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has the PlayerMovement script
        if (collision.GetComponent<PlayerMovement>() != null && !isTransitioning)
        {
            NextLevelTrigger();
        }
    }

    private void NextLevelTrigger()
    {
        isTransitioning = true; // Prevent multiple triggers
        StartCoroutine(WinTransition());
    }

    private IEnumerator WinTransition()
    {
        playerMovement.allowMovement = false; // Disable player movement
        // Activate the win panel and set its initial opacity to 0
        winGamePanel.SetActive(true);
        Color color = winGamePanelImage.color;
        color.a = 0;
        winGamePanelImage.color = color;

        // Gradually increase opacity over the transition duration
        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / transitionDuration); // Calculate alpha
            winGamePanelImage.color = color;
            yield return null;
        }

        // Wait an additional 2 seconds before loading the next level
        yield return new WaitForSeconds(2f);

        // Load the next scene (e.g., "Level 2")
        SceneManager.LoadScene("Level 2");
    }
}
