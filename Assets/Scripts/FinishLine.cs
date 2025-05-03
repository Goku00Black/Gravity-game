using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool hasWinner = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWinner) return; // Prevent multiple winners

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            hasWinner = true;
            FindObjectOfType<WinManager>().PlayerWon(other.tag);
        }
    }
}
