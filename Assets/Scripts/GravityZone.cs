using UnityEngine;

public class GravityZone : MonoBehaviour
{
    public float gravityForce = 9.8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Flip gravity for the player
                float newGravity = rb.gravityScale > 0 ? -gravityForce : gravityForce;
                rb.gravityScale = newGravity;

                // Flip the player's visual scale to match
                Transform playerTransform = collision.transform;
                Vector3 newScale = playerTransform.localScale;
                newScale.y = Mathf.Abs(newScale.y) * Mathf.Sign(-rb.gravityScale);
                playerTransform.localScale = newScale;
            }
        }
    }
}
