
using UnityEngine;
using System.Collections;

public class PlayerSquash : MonoBehaviour
{
    [SerializeField] float squashDuration = 0.2f; // How long the squash animation lasts
    [SerializeField] float squashScaleY = 0.2f;   // The Y scale when squashed
    private Vector3 originalScale;      // Player's original scale

    private Mover moverScript;          // Reference to the movement script
    private Animator animator;          // Reference to the animator

    public bool IsSquashed { get; private set; } = false; // New property

    void Start()
    {
        originalScale = transform.localScale;
        moverScript = GetComponent<Mover>();
        animator = GetComponent<Animator>();
    }

    public void Squash()
{
    IsSquashed = true;
    var rb = GetComponent<Rigidbody>();
    if (rb != null)
        rb.isKinematic = true;
    StartCoroutine(SquashRoutine());
}

    private IEnumerator SquashRoutine()
    {
        // Disable jumping and movement
        if (moverScript != null)
        {
            moverScript.DisableJumping();
            moverScript.enabled = false; // Stop all movement controls
        }

        // Stop legs animation
        if (animator != null)
            animator.enabled = false;

        // Animate squash
        float elapsed = 0f;
        Vector3 squashScale = new Vector3(originalScale.x, squashScaleY, originalScale.z);
        while (elapsed < squashDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squashScale, elapsed / squashDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = squashScale;  // Stay squashed
    }

    // Call this method to reset the player (if needed, e.g., respawn)
    public void ResetPlayer()
    {
        transform.localScale = originalScale;
        IsSquashed = false; // Reset squash state

        if (animator != null)
            animator.SetBool("isMoving", true); // Reactivate leg movement

        if (moverScript != null)
        {
            moverScript.enabled = true;  // Re-enable movement
            moverScript.EnableJumping(); // Re-enable jumping
        }
    }
}
