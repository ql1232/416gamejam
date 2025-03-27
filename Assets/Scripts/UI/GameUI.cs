using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Score Display")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    private ScoreDisplayUI scoreDisplayUI;

    private void Awake()
    {
        // Get or add ScoreDisplayUI component
        scoreDisplayUI = scorePanel.GetComponent<ScoreDisplayUI>();
        if (scoreDisplayUI == null)
        {
            scoreDisplayUI = scorePanel.AddComponent<ScoreDisplayUI>();
        }

        // Set up the references in ScoreDisplayUI
        if (scoreDisplayUI != null && scoreText != null && highScoreText != null)
        {
            scoreDisplayUI.SetTextReferences(scoreText, highScoreText);
        }
    }

    private void Start()
    {
        // Show score panel
        if (scorePanel != null)
        {
            scorePanel.SetActive(true);
        }
    }

    // Public methods to update score
    public void UpdateScore(int newScore)
    {
        if (scoreDisplayUI != null)
        {
            scoreDisplayUI.UpdateScore(newScore);
        }
    }

    public void AddPoints(int points)
    {
        if (scoreDisplayUI != null)
        {
            scoreDisplayUI.AddPoints(points);
        }
    }

    public int GetCurrentScore()
    {
        return scoreDisplayUI != null ? scoreDisplayUI.GetCurrentScore() : 0;
    }

    public int GetHighScore()
    {
        return scoreDisplayUI != null ? scoreDisplayUI.GetHighScore() : 0;
    }
} 