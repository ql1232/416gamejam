using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private float maxPlayerY;

    void Start()
    {
        offset = transform.position - player.position;
        maxPlayerY = player.position.y;
    }

    void Update()
    {
        if (player.position.y > maxPlayerY)
        {
            maxPlayerY = player.position.y;
        }

        Vector3 desiredPosition = player.position + offset;
        desiredPosition.y = maxPlayerY + offset.y;
        transform.position = desiredPosition;
    }
}
