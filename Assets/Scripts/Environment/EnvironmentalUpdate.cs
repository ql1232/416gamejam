using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	public Transform player;
	private Vector3 offset;
	private float maxPlayerY;
   	    void Start()
    {
        maxPlayerY = player.position.y;
    }

    void Update()
    {
        if (player.position.y > maxPlayerY)
        {
            maxPlayerY = player.position.y;
        }
        transform.position += new Vector3(0,maxPlayerY - transform.position.y,0);
    }
}
