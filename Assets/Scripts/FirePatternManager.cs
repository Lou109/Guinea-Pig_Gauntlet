using UnityEngine;
using System.Collections;

public class FirePatternManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] flames; // Array of flames

    [SerializeField]
    private float baseOnTime = 1.5f;
    [SerializeField]
    private float baseOffTime = 1f;
    [SerializeField]
    private float patternVariation = 0.3f;

    private float[] onDurations;
    private float[] offDurations;

    void Start()
    {
        onDurations = new float[flames.Length];
        offDurations = new float[flames.Length];

        for (int i=0; i<flames.Length; i++)
        {
            onDurations[i] = baseOnTime + Random.Range(-patternVariation, patternVariation);
            offDurations[i] = baseOffTime + Random.Range(-patternVariation, patternVariation);
        }

        StartCoroutine(FireSequence());
    }

    private IEnumerator FireSequence()
    {
        while (true)
        {
            for (int i=0; i<flames.Length; i++)
            {
                flames[i].SetActive(true);
                yield return new WaitForSeconds(onDurations[i]);
                flames[i].SetActive(false);
                yield return new WaitForSeconds(offDurations[i]);
            }

            // Slight variation for next cycle
            for (int i=0; i<flames.Length; i++)
            {
                onDurations[i] = baseOnTime + Random.Range(-patternVariation, patternVariation);
                offDurations[i] = baseOffTime + Random.Range(-patternVariation, patternVariation);
            }
        }
    }
}
