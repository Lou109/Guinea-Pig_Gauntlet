
using UnityEngine;

public class SlideTrigger : MonoBehaviour
{
    public enum TriggerType { Start, End }
    public TriggerType triggerType;
    public GameObject player; // assign via inspector or find dynamically

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name} (tag: {other.tag})");
        if (!other.CompareTag("Player"))
        {
            Debug.Log("Object not tagged as Player, ignoring.");
            return;
        }

        var mover = other.GetComponent<Mover>();
        Debug.Log($"Detected Mover component: {mover}");

        if (mover != null)
        {
            if (triggerType == TriggerType.Start)
            {
                mover.SetSliding(true);
                Debug.Log("Sliding Started");
            }
            else if (triggerType == TriggerType.End)
            {
                mover.SetSliding(false);
                Debug.Log("Sliding Ended");
            }
        }
    }
}
