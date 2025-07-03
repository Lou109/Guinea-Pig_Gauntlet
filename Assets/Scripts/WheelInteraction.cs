using UnityEngine;

public class WheelInteraction : MonoBehaviour
{
    [SerializeField] private RotateObjects rotateObjects; // Reference to your rotation script

    private void Start()
    {
        // Ensure the rotation is disabled initially
        if (rotateObjects != null)
        {
            rotateObjects.enabled = false;
        }
        else
        {
            Debug.LogWarning("RotateObjects reference not assigned in WheelInteraction.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rotateObjects != null)
            {
                rotateObjects.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rotateObjects != null)
            {
                rotateObjects.enabled = false;
            }
        }
    }
}
