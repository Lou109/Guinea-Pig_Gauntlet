using UnityEngine;

public class WheelInteraction : MonoBehaviour
{
    [SerializeField] private RotateWheel rotateWheel; // Reference to your RotateWheel script
    [SerializeField] private DoorRaiseAndLower doorController; // Assign your door script here
    [SerializeField]
    [Range(0f, 1f)]
    private float facingThreshold = 0.866f; // Default 30°

    private bool cycleStarted = false;

    private void Start()
    {
        if (rotateWheel != null)
        {
            rotateWheel.isRotating = false;
        }
        else
        {
            Debug.LogWarning("RotateWheel reference is missing.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerForward = other.transform.forward;
            Vector3 wheelForward = transform.forward;
            float dotProduct = Vector3.Dot(playerForward.normalized, wheelForward.normalized);

            if (dotProduct > facingThreshold)
            {
                if (rotateWheel != null)
                {
                    // Start rotating
                    if (!rotateWheel.isRotating)
                    {
                        rotateWheel.isRotating = true;

                        // Trigger door to raise once when rotation starts
                        if (!cycleStarted)
                        {
                            cycleStarted = true;
                            if (doorController != null)
                            {
                                Debug.Log("Starting door raise");
                                doorController.StartCycle();
                            }
                        }
                    }
                }
            }
            else
            {
                if (rotateWheel != null)
                {
                    rotateWheel.isRotating = false;
                }
                cycleStarted = false; // Reset for next cycle
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rotateWheel != null)
            {
                rotateWheel.isRotating = false;
            }
            cycleStarted = false;
        }
    }
}
