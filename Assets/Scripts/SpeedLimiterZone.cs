using UnityEngine;

public class SpeedLimiterZone : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 4f;      // Your walk speed
    [SerializeField] private float originalRunSpeed = 12f; // Your normal run speed
    [SerializeField] private Mover playerMover;          // Assign your Player's 'Mover' script in inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerMover != null)
        {
            // Set run speed to walk speed to prevent pushing
            playerMover.SetRunSpeed(walkSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerMover != null)
        {
            // Reset to original run speed
            playerMover.SetRunSpeed(originalRunSpeed);
        }
    }
}
