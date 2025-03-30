using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private GameObject hudElementsPanel;
    private HUDElements hudElements;

    [Header("Game Over Display")]
    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private TextMeshProUGUI finalHeightText;

    [Header("Game Over Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button gameOverQuitButton;

    private bool isGameOver = false;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button pauseQuitButton;
    
    private void Awake()
    {
        // Get or add HUDElements component
        hudElements = hudElementsPanel.GetComponent<HUDElements>();
        if (hudElements == null)
        {
            hudElements = hudElementsPanel.AddComponent<HUDElements>();
        }
    }

    private void Start()
    {
        // Show HUD panel
        if (hudElementsPanel != null)
            hudElementsPanel.SetActive(true);

        if (backgroundPanel != null)
            backgroundPanel.SetActive(false);

        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (restartButton != null)
            restartButton.onClick.AddListener(RetryGame);

        if (pauseQuitButton != null)
            pauseQuitButton.onClick.AddListener(QuitGame);

        if (retryButton != null)
            retryButton.onClick.AddListener(RetryGame);

        if (gameOverQuitButton != null)
            gameOverQuitButton.onClick.AddListener(QuitGame);
    }

    // HUD update methods (delegated to HUDElements)
    public void UpdateHP(int hp)
    {
        if (hudElements != null)
        {
            hudElements.UpdateHP(hp);
        }
    }

    public void UpdateAmmo(int ammo)
    {
        if (hudElements != null)
        {
            hudElements.UpdateAmmo(ammo);
        }
    }

    public void UpdateScore(int newScore)
    {
        if (hudElements != null)
        {
            hudElements.UpdateScore(newScore);
        }
    }

    public void UpdateMaxHeight(int height)
    {
        if (hudElements != null)
        {
            hudElements.UpdateMaxHeight(height);
        }
    }

    private void Update()
    {
        // Only process ESC key if we're not in the MainMenu scene
        if (!IsInMainMenu())
        {
            // ESC to toggle Pause Menu
            if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
            {
                if (pauseMenuPanel != null)
                {
                    bool isPaused = pauseMenuPanel.activeSelf;
                    pauseMenuPanel.SetActive(!isPaused);
                    Time.timeScale = isPaused ? 1f : 0f;
                }
            }
        }
    }

    private bool IsInMainMenu()
    {
        return SceneManager.GetActiveScene().name == "MainMenu";
    }

    public void ShowGameOverScreen()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (hudElements != null && finalHeightText != null)
        {
            finalHeightText.text = $"Max Height: {hudElements.GetMaxHeight():0000}";
        }

        // Hide HUD panel
        if (hudElementsPanel != null)
            hudElementsPanel.SetActive(false);

        // Show the game over panel
        if (backgroundPanel != null)
            backgroundPanel.SetActive(true);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    private void ResumeGame()
    {
        if (pauseMenuPanel != null){
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f; // Resume time
        }
    }

    private void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InGame");
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