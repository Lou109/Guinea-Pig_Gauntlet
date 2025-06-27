using UnityEngine;
using System.Collections;

public class PlayerFireDamage : MonoBehaviour
{
    private Coroutine damageRoutine;
    private PlayerHealth playerHealth;
    private PlayerLives playerLives;

    [SerializeField]
    private int damagePerTick = 10; // Damage per interval
    [SerializeField]
    private float damageInterval = 1f;

    [SerializeField]
    private GameObject fireParticleEffect;

    private static int fireCounter = 0; // Tracks number of active fire sources
    private bool isOnFire = false;

    private void Awake()
    {
        // Get references
        playerHealth = GetComponent<PlayerHealth>();
        playerLives = GetComponent<PlayerLives>();

        // Ensure particles are OFF initially
        if (fireParticleEffect != null)
            fireParticleEffect.SetActive(false);
    }

    // Call this when player enters a fire zone
    public void CatchFire()
    {
        fireCounter++;
        if (fireCounter == 1)
        {
            // First fire source: turn ON effects & start damage
            isOnFire = true;
            if (fireParticleEffect != null)
                fireParticleEffect.SetActive(true);
            if (damageRoutine == null)
                damageRoutine = StartCoroutine(ApplyFireDamage());
        }
        // If multiple fires, just increase count, effects stay active
    }

    // Call this when player leaves a fire zone
    public void StopFireDamage()
    {
        if (fireCounter > 0)
        {
            fireCounter--;
            if (fireCounter == 0)
            {
                // No more fire sources: turn OFF effects & stop damage
                isOnFire = false;
                if (fireParticleEffect != null)
                    fireParticleEffect.SetActive(false);
                if (damageRoutine != null)
                {
                    StopCoroutine(damageRoutine);
                    damageRoutine = null;
                }
            }
        }
    }

    public bool IsOnFire()
    {
        return isOnFire;
    }

    private IEnumerator ApplyFireDamage()
    {
        while (true)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerTick);
                if (playerHealth.GetHealth() <= 0)
                {
                    // Player died, handle life loss
                    if (playerLives != null)
                        playerLives.LoseLife();

                    // Automatically stop the fire effects
                    StopFireDamage();
                    break;
                }
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
    
    public void ExtinguishAllFires()
    {
    fireCounter = 0;
    isOnFire = false;

        if (fireParticleEffect != null)
        fireParticleEffect.SetActive(false);
        if (damageRoutine != null)
        {
        StopCoroutine(damageRoutine);
        damageRoutine = null;
        }
    }
}
