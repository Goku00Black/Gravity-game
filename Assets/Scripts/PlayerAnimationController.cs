using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController; // Reference to the movement script

    private bool gameStarted = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Freeze movement at start
        playerController.enabled = false;
        animator.SetBool("isRunning", false);
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        playerController.enabled = true;
        animator.SetBool("isRunning", true);
    }
}
