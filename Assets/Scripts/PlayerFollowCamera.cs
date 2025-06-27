using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign in inspector
    [SerializeField] private float followDistance = 5f;
    [SerializeField] private float followHeight = 2f;
    [SerializeField] private float followSpeed = 10f;

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position - player.forward * followDistance + Vector3.up * followHeight;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(player.position + Vector3.up * followHeight);
    }
}
