using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;  // Drag the respawn point here in the inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug message to confirm the player hit the spike trap
            Debug.Log("Player hit the spike trap!");

            // Trigger player death (destroy player object or disable it)
            PlayerDeath(collision.gameObject);
        }
    }

    private void PlayerDeath(GameObject player)
    {
        // Optionally, disable the player or show a death animation
        player.SetActive(false);

        // Trigger respawn after a short delay (e.g., 1 second)
        Invoke("RespawnPlayer", 1f);
    }

    private void RespawnPlayer()
    {
        // Make sure to enable the player again
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && respawnPoint != null)
        {
            // Respawn player at the respawn point's position
            player.transform.position = respawnPoint.position;
            player.SetActive(true);
        }
    }
}
