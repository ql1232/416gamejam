using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfFrame : MonoBehaviour
{
    public Camera userCamera;
    public float margin = 14.5f;
    private GameUI gameUI;
    private bool hasFallen = false;

    void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("OutOfFrame: GameUI not found!");
        }
    }

    void Update()
    {
        Vector3 bottomCenter = userCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, userCamera.nearClipPlane));

        if (transform.position.y < bottomCenter.y - margin && !hasFallen)
        {
            hasFallen = true;
            if (gameUI != null)
            {
                gameUI.UpdateHP(gameUI.GetCurrentHP() - 1);
                if (gameUI.GetCurrentHP() <= 0)
                {
                    gameUI.ShowGameOverScreen();
                }
            }
        }
        else if (transform.position.y >= bottomCenter.y - margin)
        {
            hasFallen = false;
        }
    }
}
