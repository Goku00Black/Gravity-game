using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button quitButton;

    private void Start()
    {
        gameOverPanel.SetActive(false); // Start with the GameOver panel hidden
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true); // Show GameOver screen
    }

    private void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitGame()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
