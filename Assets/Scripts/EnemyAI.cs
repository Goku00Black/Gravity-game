using UnityEngine;
using System.Collections;

public class EnemyAIController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 20f;

    [Header("Gravity Settings")]
    public float gravityForce = 9.8f;
    public float jumpForce = 12f;

    [Header("Detection Settings")]
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform gapCheck;

    [Header("Chase Settings")]
    public float chaseDelay = 0.5f; // Delay before enemy starts moving, reduced for faster response

    private Rigidbody2D rb;
    private Transform targetPlayer;
    private bool isGravityInverted = false;
    private bool isGrounded;
    private bool canChase = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityForce;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            targetPlayer = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        }

        StartChase();
    }

    public void StartChase()
    {
        StartCoroutine(DelayedChase());
    }

    private IEnumerator DelayedChase()
    {
        canChase = false;
        yield return new WaitForSeconds(chaseDelay); // Shortened the delay to make it faster
        canChase = true;
        Debug.Log("Chase started!");
    }

    private void Update()
    {
        // Only proceed if we can chase and if we have a player reference
        if (!canChase || targetPlayer == null) return;

        // Get the horizontal and vertical distance to the player
        Vector2 directionToPlayer = targetPlayer.position - transform.position;

        // Debugging log
        Debug.Log($"Enemy Position: {transform.position}, Player Position: {targetPlayer.position}, Direction: {directionToPlayer}");

        // Flip gravity based on the player's vertical position
        if (directionToPlayer.y > 1f && !isGravityInverted)
        {
            FlipGravity();
        }
        else if (directionToPlayer.y < -1f && isGravityInverted)
        {
            FlipGravity();
        }

        // Move towards the player in both X and Y directions
        Vector2 moveDirection = new Vector2(directionToPlayer.x, directionToPlayer.y).normalized; // Normalize direction

        // Move the enemy towards the player
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // Flip the enemy to face the correct direction based on movement
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); // Face right
        }
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); // Face left
        }

        // Optional: Catch logic, can trigger game over when in range
        float distance = Vector2.Distance(transform.position, targetPlayer.position);
        if (distance <= 1.5f)
        {
            Debug.Log("Enemy caught the player!");
            // Trigger game over or other logic here if needed
        }
    }

    private void FixedUpdate()
    {
        // If the enemy is not chasing, no need to update movement
        if (!canChase || targetPlayer == null) return;

        // Ensure enemy speed is gradually increased
        moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, speedIncreaseRate * Time.deltaTime);

        // Check if the enemy is grounded (to prevent jumping while in the air)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Check if there is a gap ahead and jump if necessary
        bool isGapAhead = !Physics2D.Raycast(gapCheck.position, Vector2.down * (isGravityInverted ? -1 : 1), 1f, groundLayer);

        if (isGapAhead && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Stop any vertical motion first
            rb.AddForce(Vector2.up * (isGravityInverted ? -jumpForce : jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FlipGravity()
    {
        isGravityInverted = !isGravityInverted;
        rb.gravityScale = isGravityInverted ? -gravityForce : gravityForce;

        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (gapCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(gapCheck.position, gapCheck.position + Vector3.down * (isGravityInverted ? -1 : 1));
        }
    }
}
