using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var fireComp = other.GetComponent<PlayerFireDamage>();
            if (fireComp != null && fireComp.IsOnFire())
            {
                fireComp.StopFireDamage();
                Debug.Log("Fire extinguished");
                Destroy(gameObject); // Remove the extinguisher after use
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Extinguisher trigger exited by: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            var fireComp = other.GetComponent<PlayerFireDamage>();
            if (fireComp != null)
            {
                fireComp.StopFireDamage();
                Debug.Log("Fire re-ignited or still active if multiple fires");
            }
        }
    }
}



