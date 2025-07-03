using UnityEngine;

public class WheelInteraction : MonoBehaviour
{
    [SerializeField] private RotateWheel rotateWheel; // Reference to your RotateWheel script
    [SerializeField]
    [Range(0f, 1f)]
    private float facingThreshold = 0.866f; // Default 30° threshold

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
                    rotateWheel.isRotating = true;
                }
            }
            else
            {
                if (rotateWheel != null)
                {
                    rotateWheel.isRotating = false;
                }
                Debug.Log("Player not facing wheel enough to rotate");
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
        }
    }
}
