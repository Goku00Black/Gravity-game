using UnityEngine;

public class ShurikenTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDeathHandler deathHandler = collision.GetComponent<PlayerDeathHandler>();
            if (deathHandler != null)
            {
                deathHandler.Die();
            }
        }
    }
}
