using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public Transform respawnPoint;
    private Rigidbody2D rb;
    private PlayerController playerController;

    public EnemyAIController enemy; // drag and drop the enemy reference in inspector

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    public void Die()
    {
        transform.position = respawnPoint != null ? respawnPoint.position : transform.position;
        rb.velocity = Vector2.zero;

        if (playerController != null)
            playerController.ResetGravity();

        if (enemy != null)
            enemy.StartChase(); // Tell the enemy to start
    }
}
