using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    [Header("Score Display")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Game Over Display")]

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalHighScoreText;

    private ScoreDisplayUI scoreDisplayUI;

    private bool isGameOver = false;

    [Header("Game Over Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;


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
        if (scorePanel != null)
            scorePanel.SetActive(true);

        if (backgroundPanel != null)
            backgroundPanel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(RetryGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
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

    public void ShowGameOverScreen()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (scoreDisplayUI != null)
        {
            int current = scoreDisplayUI.GetCurrentScore();
            int high = scoreDisplayUI.GetHighScore();

            if (finalScoreText != null)
                finalScoreText.text = $"Final Score: {current:0000}";

            if (finalHighScoreText != null)
                finalHighScoreText.text = $"High Score: {high:0000}";
        }

        // Optional: Hide live score panel
        if (scorePanel != null)
            scorePanel.SetActive(false);

        // Show the game over panel
        if (backgroundPanel != null)
            backgroundPanel.SetActive(true);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    private void RetryGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


} 