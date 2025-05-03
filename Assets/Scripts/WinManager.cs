using UnityEngine;
using TMPro;  // For using TMP_Text
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [Header("UI References")]
    public Canvas gameOverCanvas;         // Game Over screen canvas
    public TMP_Text winText;              // Text displaying the winner
    public GameObject restartButton;      // Restart button GameObject
    public GameObject mainMenuButton;     // Main menu button GameObject

    private void Start()
    {
        // Hide UI elements on start
        if (gameOverCanvas != null)
            gameOverCanvas.gameObject.SetActive(false);

        if (restartButton != null)
            restartButton.SetActive(false);

        if (mainMenuButton != null)
            mainMenuButton.SetActive(false);
    }

    /// <summary>
    /// Called when one player dies to declare the other as the winner.
    /// </summary>
    public void PlayerDied(string deadPlayerTag)
    {
        string winner = deadPlayerTag == "Player1" ? "Player 2 Wins!" : "Player 1 Wins!";
        ShowWinScreen(winner);
    }

    /// <summary>
    /// Called when a player reaches the finish line.
    /// </summary>
    public void PlayerWon(string winnerTag)
    {
        string winner = winnerTag == "Player1" ? "Player 1 Wins!" : "Player 2 Wins!";
        ShowWinScreen(winner);
    }

    private void ShowWinScreen(string winnerMessage)
    {
        if (winText != null)
            winText.text = winnerMessage;

        if (gameOverCanvas != null)
            gameOverCanvas.gameObject.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);

        if (mainMenuButton != null)
            mainMenuButton.SetActive(true);

        Time.timeScale = 0f; // Pause the game
    }

    // UI Button: Restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LOCAL()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LocalPlayer");
    }

    public void Main()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    // UI Button: Load Main Menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
