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
    public float chaseDelay = 1f; // Delay before enemy starts moving

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
    }

    public void StartChase()
    {
        StartCoroutine(DelayedChase());
    }

    private IEnumerator DelayedChase()
    {
        canChase = false;
        yield return new WaitForSeconds(chaseDelay);
        canChase = true;
    }

    private void Update()
    {
        if (!canChase || targetPlayer == null) return;

        float verticalDifference = targetPlayer.position.y - transform.position.y;

        if (verticalDifference > 1f && !isGravityInverted)
        {
            FlipGravity();
        }
        else if (verticalDifference < -1f && isGravityInverted)
        {
            FlipGravity();
        }

        // Optional: Catch logic
        float distance = Vector2.Distance(transform.position, targetPlayer.position);
        if (distance <= 1.5f)
        {
            Debug.Log("Enemy caught the player!");
            // Trigger game over here if needed
        }
    }

    private void FixedUpdate()
    {
        if (!canChase || targetPlayer == null) return;

        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, speedIncreaseRate * Time.deltaTime);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        bool isGapAhead = !Physics2D.Raycast(gapCheck.position, Vector2.down * (isGravityInverted ? -1 : 1), 1f, groundLayer);

        if (isGapAhead && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
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
