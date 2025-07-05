using UnityEngine;

public class JumpLimitArea : MonoBehaviour
{
    [SerializeField] private float reducedJumpForce = 2f;
    [SerializeField] private float normalJumpForce; // assign in inspector if needed

    private Mover playerMover;

    private void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerMover = playerObj.GetComponent<Mover>();
        }
        // Optionally, assign normalJumpForce here or in inspector
        if (playerMover != null)
        {
            normalJumpForce = playerMover.jumpForce;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerMover != null)
        {
            playerMover.SetJumpForce(reducedJumpForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerMover != null)
        {
            playerMover.SetJumpForce(normalJumpForce);
        }
    }
}

