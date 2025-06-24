using UnityEngine;

public class Mover : MonoBehaviour
{
    // Movement states
    private enum MovementState { Walking, Running }

    [SerializeField] private float rotationSpeed = 120f; // degrees/sec
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float autoSlideSpeed = 5f;             // Moving forward speed (can tweak in inspector)
    [SerializeField] private float maxSwayRotationSpeed = 30f;     // Max degrees per second
    [SerializeField] private float swaySmoothness = 10f;           // How quickly sway adapts
    [SerializeField] private Transform groundCheckPoint; // assign in inspector
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private float swayInputSmoothed = 0f;

    private float moveSpeed;

    [SerializeField] private float jumpForce = 8f; // Jump force
    private Rigidbody myRigidbody;
    private bool isGrounded = true;
    private bool canJump = true;
    private bool hasBeenSquashed = false;
    private bool isSliding = false;

    private PlayerSquash playerSquash;
    [SerializeField] private Animator animator;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        playerSquash = GetComponent<PlayerSquash>();

        // Initialize movement speed
        moveSpeed = walkSpeed;

        // Initialize animator parameters
        if (animator != null)
        {
            animator.SetBool("isGrounded", true);
        }
    }

    private void Update()
    {
        if (isSliding)
        {
            HandleSliding();
            HandleJump();
        }
        else
        {
            HandleControls();
            HandleJump();
            HandleRun();
        }
    }
private void FixedUpdate()
{
    CheckGround();
}

private void CheckGround()
{
    // Visualize the raycast in the scene for debugging
    Debug.DrawRay(groundCheckPoint.position, Vector3.down * groundCheckDistance, Color.red);

    // Raycast down only to detect ground
    if (Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer))
    {
        isGrounded = true;
    }
    // Do NOT set to false here; only set false when jumping or leaving ground
}

    private void HandleControls()
    {
        // Handle rotation
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Handle movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

        // Set movement animation parameter
        if (animator != null)
        {
            animator.SetFloat("Speed", moveSpeed);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            if (animator != null)
            {
                animator.SetTrigger("PlayJump");
                animator.SetBool("isGrounded", false);
            }
        }
    }
    private void HandleSliding()
{
    // Move forward automatically
    transform.position += transform.forward * autoSlideSpeed * Time.deltaTime;

    // Determine target sway based on input
    float targetSway = 0f;
    if (Input.GetKey(KeyCode.A))
        targetSway = -1f;
    else if (Input.GetKey(KeyCode.D))
        targetSway = 1f;

    // Smooth the sway input for gradual change
    swayInputSmoothed = Mathf.Lerp(swayInputSmoothed, targetSway, swaySmoothness * Time.deltaTime);

    // Calculate sway rotation with max speed limit
    float swayRotation = swayInputSmoothed * maxSwayRotationSpeed;

    // Apply rotation
    transform.Rotate(0, swayRotation * Time.deltaTime, 0);
}
    private void HandleRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {

            moveSpeed = runSpeed;
        }
        else
        {

            moveSpeed = walkSpeed;
        }
    }

    public void EnableJumping()
    {
        canJump = true;
    }

    public void DisableJumping()
    {
        canJump = false;
    }

    public void SetSliding(bool slide)
    {
        isSliding = slide;
        Debug.Log("SetSliding called with: " + slide);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tag = collision.gameObject.tag;

        // Detect landing
        if (tag == "Ground" || tag == "Platform")
        {
            isGrounded = true;
        }

        // Detect being squashed
        if (tag == "Boulder" && !hasBeenSquashed)
        {
            hasBeenSquashed = true;
            playerSquash?.Squash();
        }
    }
}
