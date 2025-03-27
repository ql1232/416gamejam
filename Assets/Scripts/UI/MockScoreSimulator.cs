using UnityEngine;
using UnityEngine.SceneManagement;

public class MockScoreSimulator : MonoBehaviour
{
    private GameUI gameUI;
    private float timer;
    private int scoreIncrement = 1;

    private void Start()
    {
        gameUI = FindAnyObjectByType<GameUI>();
        timer = 0f;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "SampleScene") return;
        if (gameUI == null)
        {
            Debug.LogWarning("MockScoreSimulator: GameUI not found!");
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            Debug.Log("Adding point!");
            gameUI.AddPoints(scoreIncrement);
            timer = 0f;
        }
    }

}
