using UnityEngine;
using UnityEngine.SceneManagement;

public class UiButton : MonoBehaviour
{
    public void Play()
    {
        // Load the first level
        SceneManager.LoadScene("Level 1");
    }

    public void Retry()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void BackToMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
