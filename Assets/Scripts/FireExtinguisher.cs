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
                fireComp.ExtinguishAllFires(); // Call the new method here
                Debug.Log("Fire fully extinguished");
                Destroy(gameObject); // Remove the extinguisher after use (optional)
            }
        }
    }
}




