using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private float maxPlayerY, prevMaxY;

    void Start()
    {
        offset = transform.position - player.position;
        maxPlayerY = player.position.y;
    }

    void Update()
    {
	prevMaxY = maxPlayerY;
        if(player.position.y > maxPlayerY)
       {
            maxPlayerY = player.position.y;
        }

        Vector3 desiredPosition = player.position + offset;
        desiredPosition.y = maxPlayerY + offset.y;
        transform.position = desiredPosition;

	checkAndPopulate();
    }
	//normally we do this part in a game manager but this is fine
   	 void checkAndPopulate(){
		int level = (int) Mathf.Floor(maxPlayerY/10);
		if(level > (int) Mathf.Floor(prevMaxY/10)){
			pop = (int) Random.Range(3,6);
			
		}
	}	

}
