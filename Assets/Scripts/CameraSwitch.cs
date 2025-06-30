using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject cinemachineCamera;
    [SerializeField] private bool revertOnExit = true; // Default to false

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cinemachineCamera.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Do nothing to keep the camera active afterward
        if (other.CompareTag("Player") && revertOnExit)
        {
            cinemachineCamera.SetActive(true);
        }
    }
} 

    