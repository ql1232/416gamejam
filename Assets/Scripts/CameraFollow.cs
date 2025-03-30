using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 displacement;
    private float playerMaxY;

    void Start()
    {
        displacement = transform.position - player.position;
        playerMaxY = player.position.y;
    }

    void Update()
    {
        if (player.position.y  > playerMaxY)
        {
            playerMaxY = player.position.y;
        }

        Vector3 desiredPosition = player.position + displacement;
        desiredPosition.y = playerMaxY + displacement.y;
        transform.position = desiredPosition;
    }
}
