

using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private float maxPlayerY, prevMaxY;

	public GameObject smallP;
	public GameObject medP;
	public GameObject largeP; public GameObject barrel; public GameObject death;
	public GameObject freeze, reduce;
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
	desiredPosition.z -=3;
        transform.position = desiredPosition;

	checkAndPopulate();
    }
	//normally we do this part in a game manager but this is fine
   	 void checkAndPopulate(){
		int level = (int) Mathf.Floor((maxPlayerY+10)/10);
		if(level > (int) Mathf.Floor((prevMaxY+10)/10)){
			int pop = 20;
			float maxH = 30, maxV = 7;
			float dirMod = 1;
			if(player.position.x < 0){dirMod = -1;}
			for(int n = 0; n<pop; n++){
				if(maxH < 2 && maxV <1){
					break;
				}
				float distH = Random.Range(2, Mathf.Min(maxH, 5));
				float distY = Random.Range(1, Mathf.Min(maxV, 3));
				int m = (int) Mathf.Floor(Random.Range(0,3));
				GameObject temp = smallP;
				if(m==0) {temp = medP;if(Mathf.Abs(30-maxH) < 5){temp=smallP;}}
				else if(m==1){temp=largeP;
					if(Mathf.Abs(30-maxH) < 5){temp=smallP;}
				}
				maxH-=distH; maxV-=distY;
				Instantiate(temp, new Vector3(dirMod*(15 - maxH),3-maxV + level*10,0), Quaternion.identity);
				m = (int) Mathf.Floor(Random.Range(0,8));
				temp = barrel;
				if(m<2) {temp = death;} else if (m<4){temp = freeze;} else if(m<6){temp=reduce;}
				if(Random.Range(1,10) < 3){Instantiate(temp, new Vector3(dirMod*(20 - maxH),13-maxV + level*10 + n*10/pop,-0.5F), Quaternion.Euler(91.099F, 0, 0));
}

				}
		}
	}	

}

