using UnityEngine;

public class UiScore : MonoBehaviour
{
    public int health = 0; // Initial health
    public int score = 0; // Initial score

    public GameObject[] HealthIcons; // Array of health icons
    public TMPro.TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    // Start is called before the first frame update
    void Start()
    {
        health = 1; // Ensure the health starts at 1
        score = 0; // Ensure the score starts at 0
        UpdateScoreUI(); // Update the score text on game start
        UpdateHealthUI(); // Update the health icons on game start
    }

    // Method to add score
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd; // Add to the current score
        UpdateScoreUI(); // Update the score text
    }

    public void AddHealth(int healthToAdd)
    {
        if (health <= 3)
        {
            health += healthToAdd; // Add to the current health
            UpdateHealthUI(); // Update the health icons
        }
        else 
        {
            health = 3;
        }
    }

    public void DecreaseHealth(int healthToDecrease)
    {
        health -= healthToDecrease; // Decrease the current health
        UpdateHealthUI(); // Update the health icons
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

    private void UpdateHealthUI()
    {
        for (int i = 0; i < HealthIcons.Length; i++)
        {
            if (i < health)
            {
                HealthIcons[i].SetActive(true); // Activate the health icon
            }
            else
            {
                HealthIcons[i].SetActive(false); // Deactivate the health icon
            }
        }
    }
}
