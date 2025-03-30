/*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Player stats
    [Header("Player Stats")]
    public int playerHealth = 3;
    public int maxPlayerHealth = 3;
    public int ammunition = 5;
    public int maxAmmunition = 10;
    
    // Game state
    [Header("Game State")]
    public bool gameOver = false;
    public bool gamePaused = false;
    
    // UI elements (assign these in the Inspector)
    [Header("UI References")]
    public Text healthText;
    public Text ammoText;
    public Image[] healthIcons;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    
    // Audio for pickups and damage
    [Header("Audio")]
    public AudioClip ammoPickupSound;
    public AudioClip healthPickupSound;
    public AudioClip playerHitSound;
    
    private AudioSource audioSource;
    
    // Singleton pattern
    public static GameManager instance;
    
    void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Initialize UI displays
        UpdateUI();
        
        // Make sure game is running
        Time.timeScale = 1f;
        
        // Hide game over panel if it exists
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        // Hide pause panel if it exists
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }
    
    void Update()
    {
        // Check for pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    void UpdateUI()
    {
        // Update text displays if they exist
        if (healthText != null)
            healthText.text = "Lives: " + playerHealth;
            
        if (ammoText != null)
            ammoText.text = "Ammo: " + ammunition;
            
        // Update health icons if they exist
        if (healthIcons != null && healthIcons.Length > 0)
        {
            for (int i = 0; i < healthIcons.Length; i++)
            {
                if (healthIcons[i] != null)
                    healthIcons[i].enabled = (i < playerHealth);
            }
        }
    }
    
    // Call this when player takes damage
    public void TakeDamage(int damageAmount = 1)
    {
        if (gameOver)
            return;
            
        playerHealth -= damageAmount;
        
        // Make sure health doesn't go below 0
        if (playerHealth < 0)
            playerHealth = 0;
            
        // Play hit sound
        if (playerHitSound != null && audioSource != null)
            audioSource.PlayOneShot(playerHitSound);
            
        UpdateUI();
        
        // Check if player is defeated
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }
    
    // Call this when player gets extra life
    public void AddHealth(int healthAmount = 1)
    {
        if (gameOver)
            return;
            
        playerHealth += healthAmount;
        
        // Make sure health doesn't exceed maximum
        if (playerHealth > maxPlayerHealth)
            playerHealth = maxPlayerHealth;
            
        // Play health pickup sound
        if (healthPickupSound != null && audioSource != null)
            audioSource.PlayOneShot(healthPickupSound);
            
        UpdateUI();
    }
    
    // Call this when player collects ammo
    public void AddAmmo(int ammoAmount = 3)
    {
        if (gameOver)
            return;
            
        ammunition += ammoAmount;
        
        // Cap ammunition at max value
        if (ammunition > maxAmmunition)
            ammunition = maxAmmunition;
            
        // Play ammo pickup sound
        if (ammoPickupSound != null && audioSource != null)
            audioSource.PlayOneShot(ammoPickupSound);
            
        UpdateUI();
    }
    
    // Call this when player uses ammo for attacking obstacles
    public bool UseAmmo(int ammoAmount = 1)
    {
        if (gameOver)
            return false;
            
        // Check if player has enough ammo
        if (ammunition >= ammoAmount)
        {
            ammunition -= ammoAmount;
            UpdateUI();
            return true;
        }
        return false;
    }
    
    void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
        
        // Show game over panel if it exists
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        Debug.Log("Game Over!");
    }
    
    void TogglePause()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0f : 1f;
        
        // Show/hide pause panel if it exists
        if (pausePanel != null)
        {
            pausePanel.SetActive(gamePaused);
        }
    }
    
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}


*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerHealth = 3;
    public int maxPlayerHealth = 3;
    public int ammunition = 5;
    public int maxAmmunition = 10;
    
    [Header("UI Elements")]
    public Text healthText;
    public Text ammoText;
    public GameObject gameOverPanel;
    
    [Header("Audio")]
    public AudioClip coinPickupSound;
    public AudioClip hurtSound;
    
    // Game state
    private bool gameOver = false;
    
    // Singleton instance
    public static GameManager instance;
    
    void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Initialize UI
        UpdateUI();
        
        // Hide game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    
    void UpdateUI()
    {
        // Update health text
        if (healthText != null)
        {
            healthText.text = "Lives: " + playerHealth;
        }
        
        // Update ammo text
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + ammunition;
        }
    }
    
    public void TakeDamage(int damage = 1)
    {
        if (gameOver) return;
        
        // Reduce health
        playerHealth -= damage;
        
        // Play hurt sound
        if (hurtSound != null)
        {
            AudioSource.PlayClipAtPoint(hurtSound, Camera.main.transform.position);
        }
        
        // Check for game over
        if (playerHealth <= 0)
        {
            GameOver();
        }
        
        UpdateUI();
    }
    
    public void AddHealth(int amount = 1)
    {
        if (gameOver) return;
        
        // Add health but don't exceed max
        playerHealth = Mathf.Min(playerHealth + amount, maxPlayerHealth);
        UpdateUI();
    }
    
    public void AddAmmo(int amount = 1)
    {
        if (gameOver) return;
        
        // Add ammo but don't exceed max
        ammunition = Mathf.Min(ammunition + amount, maxAmmunition);
        
        // Play coin pickup sound
        if (coinPickupSound != null)
        {
            AudioSource.PlayClipAtPoint(coinPickupSound, Camera.main.transform.position);
        }
        
        UpdateUI();
    }
    
    public bool UseAmmo(int amount = 1)
    {
        // Check if we have enough ammo
        if (ammunition >= amount)
        {
            ammunition -= amount;
            UpdateUI();
            return true;
        }
        
        return false;
    }
    
    void GameOver()
    {
        gameOver = true;
        
        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        Debug.Log("Game Over!");
    }
    
    public void RestartGame()
    {
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}