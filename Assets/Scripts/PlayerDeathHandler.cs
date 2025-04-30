using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public Transform respawnPoint;

    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private PlayerController playerController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();

        initialPosition = respawnPoint != null ? respawnPoint.position : transform.position;
    }

    public void Die()
    {
        Debug.Log("Player Died! Respawning...");

        // Reset position and velocity
        transform.position = initialPosition;
        rb.linearVelocity = Vector2.zero;

        // Reset gravity and orientation
        if (playerController != null)
        {
            playerController.ResetGravity();
        }
    }
}
