using UnityEngine;

public class SlideTrigger : MonoBehaviour
{
    public enum TriggerType { Start, End, Boost }
    public TriggerType triggerType;

    // For Boost triggers
    public float boostFactor = 1.05f; // tweak for desired boost strength

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var mover = other.GetComponent<Mover>();
        if (mover != null)
        {
            switch (triggerType)
            {
                case TriggerType.Start:
                    mover.SetSliding(true);
                    Debug.Log("Sliding started");
                    break;
                case TriggerType.End:
                    mover.SetSliding(false);
                    Debug.Log("Sliding ended");
                    break;
                case TriggerType.Boost:
                    mover.TriggerBoost(boostFactor);
                    Debug.Log("Boost triggered");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var mover = other.GetComponent<Mover>();
        if (mover != null)
        {
            switch (triggerType)
            {
                case TriggerType.Start:
                    // Optional: stop sliding if you want.
                    // Uncomment if you want sliding to stop on exit:
                    // mover.SetSliding(false);
                    Debug.Log("Exited start zone");
                    break;
                case TriggerType.End:
                    // Optional: do something when leaving end zone.
                    Debug.Log("Exited end zone");
                    break;
                case TriggerType.Boost:
                    // Optional: reset boost or do nothing
                    // mover.ResetBoost(); // if you implement such method
                    Debug.Log("Left boost zone");
                    break;
            }
        }
    }
}


