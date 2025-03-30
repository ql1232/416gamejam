using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfFrame : MonoBehaviour
{
    public Camera userCamera;
    public float margin = 5f;

    void Update()
    {
        Vector3 bottomCenter = userCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, userCamera.nearClipPlane));

        if (transform.position.y < bottomCenter.y - margin)
        {
            Time.timeScale = 0;
        }
    }
}
