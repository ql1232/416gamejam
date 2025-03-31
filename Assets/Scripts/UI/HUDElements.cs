
using UnityEngine;
using TMPro;

public class HUDElements : MonoBehaviour
{
    [Header("Score Display")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI maxHeightText;
    
    [Header("Player Stats")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI ammoText;
    
    [Header("Animation Settings")]
    [SerializeField] private float scoreUpdateDuration = 0.5f;
    [SerializeField] private float scorePopupScale = 1.2f;
    
    private int currentScore = 0;
    private int maxHeight = 0;
    private int currentHP = 0;
    private int currentAmmo = 0;
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

        // Initialize all displays
        UpdateScore(0);
        UpdateMaxHeight(0);
        UpdateHP(0);
        UpdateAmmo(0);
    }

    public void SetTextReferences(
        TextMeshProUGUI newScoreText, 
        TextMeshProUGUI newMaxHeightText,
        TextMeshProUGUI newHPText,
        TextMeshProUGUI newAmmoText)
    {
        scoreText = newScoreText;
        maxHeightText = newMaxHeightText;
        hpText = newHPText;
        ammoText = newAmmoText;
        InitializeComponents();
    }

    // Score-related methods
    public void UpdateScore(int newScore)
    {
        if (scoreText == null || scoreTextRect == null) return;

        currentScore = newScore;
        scoreText.text = $"Score: {currentScore:0000}";
        
        // Only animate if the GameObject is active
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ScoreUpdateAnimation());
        }
    }

    public void AddPoints(int points)
    {
        UpdateScore(currentScore + points);
    }

    public void UpdateMaxHeight(int height)
    {
        if (maxHeightText == null) return;
        
        maxHeight = height;
        maxHeightText.text = $"Max Height: {maxHeight:0000}";
    }

    // Player stats methods
    public void UpdateHP(int hp)
    {
        if (hpText == null) return;
        
        currentHP = hp;
        hpText.text = $"HP: {currentHP:0000}";
    }

    public void UpdateAmmo(int ammo)
    {
        if (ammoText == null) return;
        
        currentAmmo = ammo;
        ammoText.text = $"Ammo: {currentAmmo:0000}";
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
    public int GetMaxHeight() => maxHeight;
    public int GetCurrentHP() => currentHP;
    public int GetCurrentAmmo() => currentAmmo;
} 
