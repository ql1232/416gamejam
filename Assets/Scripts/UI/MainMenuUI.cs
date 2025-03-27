using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsBackButton;
    [SerializeField] private Button creditsBackButton;
    [SerializeField] private Button howToPlayBackButton;

    private UIPanelAnimator mainMenuAnimator;
    private UIPanelAnimator settingsAnimator;
    private UIPanelAnimator creditsAnimator;
    private UIPanelAnimator howToPlayAnimator;

    private void Awake()
    {
        // Get or add panel animators
        mainMenuAnimator = mainMenuPanel.GetComponent<UIPanelAnimator>();
        settingsAnimator = settingsPanel.GetComponent<UIPanelAnimator>();
        creditsAnimator = creditsPanel.GetComponent<UIPanelAnimator>();
        howToPlayAnimator = howToPlayPanel.GetComponent<UIPanelAnimator>();

        // Add CanvasGroup components if they don't exist
        if (mainMenuAnimator == null) mainMenuAnimator = mainMenuPanel.AddComponent<UIPanelAnimator>();
        if (settingsAnimator == null) settingsAnimator = settingsPanel.AddComponent<UIPanelAnimator>();
        if (creditsAnimator == null) creditsAnimator = creditsPanel.AddComponent<UIPanelAnimator>();
        if (howToPlayAnimator == null) howToPlayAnimator = howToPlayPanel.AddComponent<UIPanelAnimator>();
    }

    private void Start()
    {
        // Initialize button listeners
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonClicked);
        
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        
        if (creditsButton != null)
            creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        
        if (howToPlayButton != null)
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        
        if (settingsBackButton != null)
            settingsBackButton.onClick.AddListener(OnBackButtonClicked);
        
        if (creditsBackButton != null)
            creditsBackButton.onClick.AddListener(OnBackButtonClicked);
        
        if (howToPlayBackButton != null)
            howToPlayBackButton.onClick.AddListener(OnBackButtonClicked);

        // Show main menu panel by default
        ShowPanel(mainMenuPanel);
    }

    private void OnPlayButtonClicked()
    {
        // TODO: Implement game start logic
        Debug.Log("Play button clicked");
    }

    private void OnSettingsButtonClicked()
    {
        ShowPanel(settingsPanel);
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
        if (panel != settingsPanel && settingsPanel.activeSelf)
        {
            settingsAnimator.HidePanel();
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
        else if (panel == settingsPanel)
        {
            settingsAnimator.ShowPanel();
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