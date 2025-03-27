using UnityEngine;
using TMPro;

public class ScoreDisplayUI : MonoBehaviour
{
    [Header("Score Display")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    [Header("Animation Settings")]
    [SerializeField] private float scoreUpdateDuration = 0.5f;
    [SerializeField] private float scorePopupScale = 1.2f;
    
    private int currentScore = 0;
    private int highScore = 0;
    private RectTransform scoreTextRect;
    private Vector3 originalScale;

    private void Awake()
    {
        InitializeComponents();
    }

    public void InitializeComponents()
    {
        if (scoreText != null)
        {
            scoreTextRect = scoreText.GetComponent<RectTransform>();
            if (scoreTextRect != null)
            {
                originalScale = scoreTextRect.localScale;
            }
        }
        
        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreDisplay();
    }

    public void SetTextReferences(TextMeshProUGUI newScoreText, TextMeshProUGUI newHighScoreText)
    {
        scoreText = newScoreText;
        highScoreText = newHighScoreText;
        InitializeComponents();
    }

    public void UpdateScore(int newScore)
    {
        if (scoreText == null || scoreTextRect == null) return;

        currentScore = newScore;
        scoreText.text = $"Score: {currentScore:0000}";
        
        // Animate score update
        StartCoroutine(ScoreUpdateAnimation());
        
        // Update high score if necessary
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreDisplay();
        }
    }

    public void AddPoints(int points)
    {
        UpdateScore(currentScore + points);
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {highScore:0000}";
        }
    }

    private System.Collections.IEnumerator ScoreUpdateAnimation()
    {
        // Scale up
        float elapsedTime = 0f;
        while (elapsedTime < scoreUpdateDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(1f, scorePopupScale, elapsedTime / (scoreUpdateDuration / 2));
            scoreTextRect.localScale = originalScale * scale;
            yield return null;
        }

        // Scale down
        elapsedTime = 0f;
        while (elapsedTime < scoreUpdateDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(scorePopupScale, 1f, elapsedTime / (scoreUpdateDuration / 2));
            scoreTextRect.localScale = originalScale * scale;
            yield return null;
        }

        // Ensure we end at the original scale
        scoreTextRect.localScale = originalScale;
    }

    // Public methods for external access
    public int GetCurrentScore() => currentScore;
    public int GetHighScore() => highScore;
} 