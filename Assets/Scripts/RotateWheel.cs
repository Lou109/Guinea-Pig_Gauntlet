using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    [SerializeField] private Vector3 rotationDirection = Vector3.up; // Rotation axis and speed if needed
    [SerializeField] private float rotationSpeed = 100f; // Rotation speed in degrees per second
    public bool isRotating = false; // Control whether to rotate

    private void Update()
    {
        if (isRotating)
        {
            // Rotate around the specified axis
            transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
        }
    }
}
