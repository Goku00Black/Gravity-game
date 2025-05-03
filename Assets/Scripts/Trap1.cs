using UnityEngine;

public class Trap1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController1 playerController = collision.GetComponent<PlayerController1>();
        if (playerController != null)
        {
            playerController.Die(); // Call the Die() method from PlayerController
        }
    }
}
