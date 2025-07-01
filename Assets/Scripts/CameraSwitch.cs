using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras; // Assign all your cameras here in the inspector
    [SerializeField] private int cameraIndex; // This determines which camera this trigger zone activates
    [SerializeField] private bool revertOnExit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchCamera(cameraIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && revertOnExit)
        {
            SwitchCamera(cameraIndex - 1); // Or however, you determine the previous camera
        }
    }

    private void SwitchCamera(int index)
    {
        if (index < 0 || index >= cameras.Length) return;

        // Deactivate all cameras
        foreach (var cam in cameras)
        {
            cam.SetActive(false);
        }

        // Activate the desired camera
        cameras[index].SetActive(true);
    }
}

    