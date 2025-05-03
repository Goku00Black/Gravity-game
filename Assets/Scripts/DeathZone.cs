using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public WinManager winManager; // Drag the WinManager GameObject here in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Call PlayerDied and pass in the tag of the player that died
            winManager.PlayerDied(other.tag);

            // Optionally, deactivate the player
            other.gameObject.SetActive(false);
        }
    }
}
