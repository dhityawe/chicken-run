using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    [Header("Audio Settings")]
    public int musicIndex;

    [Header("Win Transition Settings")]
    public GameObject winGamePanel; // The Win Panel
    public Image winGamePanelImage; // The Panel's Image component for opacity control
    public float transitionDuration = 2f; // Transition time in seconds

    private bool isTransitioning = false; // To prevent multiple triggers
    public PlayerMovement playerMovement;

    private void Start() 
    {
        AudioManager.Instance.PlayMusic(musicIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has the PlayerMovement script
        if (collision.GetComponent<PlayerMovement>() != null && !isTransitioning)
        {
            AudioManager.Instance.PlaySFX(0); // Play the win sound effect
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

        // Determine the next scene to load
        string currentSceneName = SceneManager.GetActiveScene().name;
        string nextSceneName = GetNextSceneName(currentSceneName);

        // Load the determined next scene
        SceneManager.LoadScene(nextSceneName);
    }

    private string GetNextSceneName(string currentSceneName)
    {
        // Determine the next level based on the current scene name
        switch (currentSceneName)
        {
            case "Level 1":
                return "Level 2";
            case "Level 2":
                return "Level 3";
            case "Level 3":
                return "MainMenu";
            default:
                return "MainMenu"; // Fallback if no match is found
        }
    }
}
