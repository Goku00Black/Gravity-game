using UnityEngine;
using TMPro; // Make sure TextMeshPro is used

public class TimeScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Reference to TextMeshPro component
    private float survivalTime = 0f;  // Variable to track survival time
    private bool isGameOver = false;  // Flag to check if the game is over
    private bool gameStarted = false;  // Flag to check if the game has started

    private void Update()
    {
        // Only start the timer after Spacebar is pressed
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;  // Set gameStarted to true when Spacebar is pressed
            Debug.Log("Game Started");
        }

        if (isGameOver || !gameStarted) return;

        // Increase survival time only if the game has started
        survivalTime += Time.deltaTime;

        // Update score display with numbers only (no "s")
        scoreText.text = "Score: " + Mathf.FloorToInt(survivalTime).ToString();
    }

    public void GameOver()
    {
        isGameOver = true;  // Set game over flag to true
        Debug.Log("Final Score: " + Mathf.FloorToInt(survivalTime).ToString());
    }

    public float GetFinalScore()
    {
        return survivalTime;  // Return the final score (time survived)
    }
}
