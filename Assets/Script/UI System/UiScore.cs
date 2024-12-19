using UnityEngine;

public class UiScore : MonoBehaviour
{
    public int score = 0; // Initial score
    public TMPro.TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the score starts at 0
        score = 0;
        UpdateScoreUI(); // Update the score text on game start
    }

    // Method to add score
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd; // Add to the current score
        UpdateScoreUI(); // Update the score text
    }

    // Method to update the score UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Update the score text
        }
        else
        {
            Debug.LogWarning("Score TextMeshProUGUI is not assigned!");
        }
    }
}
