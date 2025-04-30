using UnityEngine;

public class ShurikenTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit the shuriken trap!");

            PlayerDeathHandler deathHandler = collision.GetComponent<PlayerDeathHandler>();
            if (deathHandler != null)
            {
                deathHandler.Die(); // Respawns the player
            }
        }
    }
}
