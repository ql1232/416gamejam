using UnityEngine;

public class Despawner : MonoBehaviour
{
	float time = 500000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time-- < 0){Destroy(gameObject);}
    }
}
