using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 20f;

    [Header("Gravity Flip Settings")]
    public float gravityForce = 9.8f;
    private bool isGravityInverted = false;
    private Rigidbody2D rb;

    [Header("Input Settings")]
    public KeyCode flipKey = KeyCode.W;

    [Header("Animation Settings")]
    public Animator animator;

    private bool gameStarted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityForce;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Start with idle animation
        animator.SetBool("isRunning", false);
    }

    private void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (!gameStarted) return;

        if (Input.GetKeyDown(flipKey))
        {
            FlipGravity();
        }

        IncreaseSpeed();
    }

    private void FixedUpdate()
    {
        if (!gameStarted) return;

        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    private void StartGame()
    {
        gameStarted = true;
        animator.SetBool("isRunning", true);
    }

    private void FlipGravity()
    {
        isGravityInverted = !isGravityInverted;
        rb.gravityScale = isGravityInverted ? -gravityForce : gravityForce;

        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        transform.localScale = newScale;
    }

    private void IncreaseSpeed()
    {
        moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, speedIncreaseRate * Time.deltaTime);
    }

    public void ResetGravity()
    {
        isGravityInverted = false;
        rb.gravityScale = gravityForce;
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
    }
}
