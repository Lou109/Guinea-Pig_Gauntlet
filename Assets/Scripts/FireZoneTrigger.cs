using UnityEngine;

public class FireZoneTrigger : MonoBehaviour
{
    // Assign the PlayerFireDamage component here, or find in code
    public PlayerFireDamage playerFireDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerFireDamage != null)
                playerFireDamage.CatchFire();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerFireDamage != null)
                playerFireDamage.StopFireDamage();
        }
    }
}




