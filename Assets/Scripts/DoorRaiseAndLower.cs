using UnityEngine;
using System.Collections;

public class DoorRaiseAndLower : MonoBehaviour
{
    [SerializeField] private Transform doorTransform;        // Assign your door's child transform
    [SerializeField] private float raiseDuration = 15f;      // Time to raise the door
    [SerializeField] private float closeDuration = 30f;      // Time to close the door (longer for obstacles)
    [SerializeField] private Vector3 raiseOffset = new Vector3(0, 3, 0); // How high door raises
    [SerializeField] private float stayOpenTime = 10f;       // How long door remains open before closing
    [SerializeField] private bool autoCloseAfterOpen = true; // Automatically close after open

    private Vector3 initialLocalPosition;
    private Vector3 targetLocalPosition;
    private Coroutine currentRoutine;

    void Start()
    {
        if (doorTransform == null)
        {
            Debug.LogError("Assign the door transform in inspector");
            return;
        }
        initialLocalPosition = doorTransform.localPosition;
        targetLocalPosition = initialLocalPosition + raiseOffset;
    }

    public void StartCycle()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(CycleRoutine());
    }

    private IEnumerator CycleRoutine()
    {
        yield return StartCoroutine(RaiseRoutine());

        // Door fully open; stay open for specified time
        float timer = 0f;
        while (timer < stayOpenTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // After staying open, start closing if autoClose is enabled
        if (autoCloseAfterOpen)
        {
            yield return StartCoroutine(CloseRoutine());
        }
    }

    public void TriggerClose()
    {
        // Call this to close the door at any time
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(CloseRoutine());
    }

    private IEnumerator RaiseRoutine()
    {
        float elapsed = 0f;
        while (elapsed < raiseDuration)
        {
            float t = Mathf.Clamp01(elapsed / raiseDuration);
            doorTransform.localPosition = Vector3.Lerp(initialLocalPosition, targetLocalPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        doorTransform.localPosition = targetLocalPosition;
    }

    private IEnumerator CloseRoutine()
    {
        float elapsed = 0f;
        while (elapsed < closeDuration)
        {
            float t = Mathf.Clamp01(elapsed / closeDuration);
            doorTransform.localPosition = Vector3.Lerp(targetLocalPosition, initialLocalPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        doorTransform.localPosition = initialLocalPosition;
    }
}
