using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 20f;
    public float gravityForce = 9.8f;
    public float jumpForce = 12f;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform gapCheck;

    public float catchDistance = 1.5f;
    private Rigidbody2D rb;
    private bool isGravityInverted = false;
    private Transform targetPlayer;
    private bool isGrounded;
    private bool canJump = true;
    private float jumpCooldown = 1f;

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

    private void Update()
    {
        if (targetPlayer == null) return;

        PredictGravityFlip();
        ChasePlayer();

        float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
        if (distanceToPlayer <= catchDistance)
        {
            Debug.Log("Enemy caught the player!");
            // Trigger game over or other logic here
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, speedIncreaseRate * Time.deltaTime);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void PredictGravityFlip()
    {
        float verticalDifference = targetPlayer.position.y - transform.position.y;
        if (verticalDifference > 1f && !isGravityInverted)
        {
            FlipGravity();
        }
        else if (verticalDifference < -1f && isGravityInverted)
        {
            FlipGravity();
        }
    }

    private void ChasePlayer()
    {
        bool isGapAhead = !Physics2D.Raycast(gapCheck.position, Vector2.down * (isGravityInverted ? -1 : 1), 1f, groundLayer);
        if (isGapAhead && isGrounded && canJump)
        {
            SmartJump();
        }

        float directionToPlayer = Mathf.Sign(targetPlayer.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(directionToPlayer * moveSpeed, rb.linearVelocity.y);
    }

    private void SmartJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * (isGravityInverted ? -jumpForce : jumpForce), ForceMode2D.Impulse);
        canJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void FlipGravity()
    {
        isGravityInverted = !isGravityInverted;
        rb.gravityScale = isGravityInverted ? -gravityForce : gravityForce;

        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        transform.localScale = newScale;
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
