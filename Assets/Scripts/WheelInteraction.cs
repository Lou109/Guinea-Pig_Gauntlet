using UnityEngine;

public class WheelInteraction : MonoBehaviour
{
    [SerializeField] private Transform guineaPig;        // The guinea pig object
    [SerializeField] private Transform standPoint;       // The stand point on the wheel
    [SerializeField] private RotateObjects wheelRotator;  // Reference to your rotation script

    private bool onWheel = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GuineaPig"))
        {
            // Parent guinea pig to the wheel
            guineaPig.parent = transform;
            guineaPig.position = standPoint.position;
            guineaPig.rotation = standPoint.rotation;

            // Activate rotation
            if (wheelRotator != null)
                wheelRotator.enabled = true;

            onWheel = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GuineaPig"))
        {
            guineaPig.parent = null;

            // Stop rotation
            if (wheelRotator != null)
                wheelRotator.enabled = false;

            onWheel = false;
        }
    }
}
