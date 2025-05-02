using UnityEngine;

public class CameraFollowTwoPlayers2D : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 0.1f;
    public Vector3 offset = new Vector3(0, 0, -10); // Z = -10 for 2D

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
            return;

        // Find midpoint between players
        Vector3 centerPoint = (player1.position + player2.position) / 2f;
        Vector3 targetPosition = centerPoint + offset;

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
