using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController playerController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    public void Die()
    {
        // Stop movement
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Optional: Reset gravity if needed
        if (playerController != null)
            playerController.ResetGravity();

        // Disable the player GameObject
        gameObject.SetActive(false);

        // Optional: Trigger game over UI or scene reload here
        Debug.Log("Player has died.");
    }
}
