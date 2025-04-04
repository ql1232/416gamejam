using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button creditsBackButton;
    [SerializeField] private Button howToPlayBackButton;

    [Header("Audio")]
    [SerializeField] private GameObject backgroundMusicManagerPrefab;

    private UIPanelAnimator mainMenuAnimator;
    private UIPanelAnimator creditsAnimator;
    private UIPanelAnimator howToPlayAnimator;

    private void Awake()
    {
        // Create background music manager if it doesn't exist
        if (BackgroundMusicManager.instance == null && backgroundMusicManagerPrefab != null)
        {
            Instantiate(backgroundMusicManagerPrefab);
        }

        // Get or add panel animators
        mainMenuAnimator = mainMenuPanel.GetComponent<UIPanelAnimator>();
        creditsAnimator = creditsPanel.GetComponent<UIPanelAnimator>();
        howToPlayAnimator = howToPlayPanel.GetComponent<UIPanelAnimator>();

        // Add CanvasGroup components if they don't exist
        if (mainMenuAnimator == null) mainMenuAnimator = mainMenuPanel.AddComponent<UIPanelAnimator>();
        if (creditsAnimator == null) creditsAnimator = creditsPanel.AddComponent<UIPanelAnimator>();
        if (howToPlayAnimator == null) howToPlayAnimator = howToPlayPanel.AddComponent<UIPanelAnimator>();

        // Ensure only main menu panel is visible at startup
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (howToPlayPanel != null) howToPlayPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    private void Start()
    {
        // Initialize button listeners
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonClicked);
        
        if (creditsButton != null)
            creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        
        if (howToPlayButton != null)
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        
        if (creditsBackButton != null)
            creditsBackButton.onClick.AddListener(OnBackButtonClicked);
        
        if (howToPlayBackButton != null)
            howToPlayBackButton.onClick.AddListener(OnBackButtonClicked);

        // Show main menu panel by default
        ShowPanel(mainMenuPanel);
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("InGame");
    }

    private void OnCreditsButtonClicked()
    {
        ShowPanel(creditsPanel);
    }

    private void OnHowToPlayButtonClicked()
    {
        ShowPanel(howToPlayPanel);
    }

    private void OnQuitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnBackButtonClicked()
    {
        ShowPanel(mainMenuPanel);
    }

    private void ShowPanel(GameObject panel)
    {
        // Hide other panels ONLY IF they are active
        if (panel != mainMenuPanel && mainMenuPanel.activeSelf)
        {
            mainMenuAnimator.HidePanel();
        }
        if (panel != creditsPanel && creditsPanel.activeSelf)
        {
            creditsAnimator.HidePanel();
        }
        if (panel != howToPlayPanel && howToPlayPanel.activeSelf)
        {
            howToPlayAnimator.HidePanel();
        }

        // Show the requested panel
        if (panel == mainMenuPanel)
        {
            mainMenuAnimator.ShowPanel();
        }
        else if (panel == creditsPanel)
        {
            creditsAnimator.ShowPanel();
        }
        else if (panel == howToPlayPanel)
        {
            howToPlayAnimator.ShowPanel();
        }
    }
} 