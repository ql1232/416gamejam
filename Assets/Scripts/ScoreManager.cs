using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private GameUI gameUI;
    private float highestHeight = 0f;
    private int currentScore = 0;

    void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("ScoreManager: GameUI not found!");
        }
    }

    void Update()
    {
        if (gameUI == null) return;

        // Check if HP is 0 (game over)
        if (gameUI.GetCurrentHP() <= 0)
        {
            gameUI.ShowGameOverScreen();
            return;
        }

        // Get current height (minimum 0)
        float currentHeight = Mathf.Max(0f, transform.position.y - 3f);

        // Update highest height if we've reached a new maximum
        if (currentHeight > highestHeight)
        {
		int temp = (int) highestHeight;
            highestHeight = (int)currentHeight;
            currentScore = Mathf.RoundToInt(highestHeight);
            
            // Update UI with new values
            gameUI.UpdateMaxHeight((int)currentScore);
            gameUI.IncrementScore((int)(currentScore-temp));
        }
    }
} 