using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDeathHandler deathHandler = collision.GetComponent<PlayerDeathHandler>();
            if (deathHandler != null)
            {
                deathHandler.Die(); // Only call respawn, do NOT destroy the player
            }
        }
    }
}
