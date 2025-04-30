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
    public KeyCode flipKey = KeyCode.W; // Assign different key for Player 2

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityForce;
    }

    private void Update()
    {
        if (Input.GetKeyDown(flipKey))
        {
            FlipGravity();
        }

        IncreaseSpeed();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
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
}