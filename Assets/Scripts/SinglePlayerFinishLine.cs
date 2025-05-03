using UnityEngine;

public class SinglePlayerFinishLine : MonoBehaviour
{
    public GameObject gameOverScreen; // Assign your game over UI panel here
    public GameObject player;         // Assign the player GameObject
    public TimeScoreManager scoreManager; // Reference to the time-based score system

    private bool finished = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!finished && collision.gameObject == player)
        {
            finished = true;

            // Show game over screen
            if (gameOverScreen != null)
                gameOverScreen.SetActive(true);

            // Stop timer
            if (scoreManager != null)
                scoreManager.GameOver();

            // Stop player movement
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            Debug.Log("Player reached the finish line!");
        }
    }
}
