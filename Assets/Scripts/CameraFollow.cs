

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
        desiredPosition.y = maxPlayerY + offset.y + 3;
	desiredPosition.x -= 5;
        transform.position = desiredPosition;

	checkAndPopulate();
    }
	//normally we do this part in a game manager but this is fine
   	 void checkAndPopulate(){
		int level = (int) Mathf.Floor(maxPlayerY/10);
		if(level > (int) Mathf.Floor(prevMaxY/10)){
			int pop = (int) Random.Range(3,6);
			float maxH = 30, maxV = 10;
			for(int n = 0; n<pop; n++){
				if(maxH < 2 || maxV <1){
					break;
				}
				float distH = Random.Range(2, Mathf.Min(maxH, 8));
				float distY = Random.Range(1, Mathf.Min(maxV, 4));
			}
		}
	}	

}

