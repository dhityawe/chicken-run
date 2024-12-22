using UnityEngine;
using UnityEngine.SceneManagement;

public class UiButton : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public void Play()
    {
        SceneManager.LoadScene("Level 1");

    }
    
    public void Retry()
    {
        // back to level 1
        SceneManager.LoadScene("Level 1");
    }

    public void BackToMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
