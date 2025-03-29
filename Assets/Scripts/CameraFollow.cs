using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Drag and drop your player GameObject here in the inspector.
    public Transform player;

    // The offset distance between the player and camera.
    private Vector3 offset;

    void Start()
    {
        // Calculate and store the offset at the start.
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update the camera's position to maintain the offset from the player's position.
        transform.position = player.position + offset;
    }
}
