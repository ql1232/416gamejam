using UnityEngine;
using UnityEngine.SceneManagement;

public class MockHUDTester : MonoBehaviour
{
    private GameUI gameUI;
    private float timer = 0f;
    private int mockScore = 0;
    private int mockHeight = 0;
    private int mockHP = 3;
    private int mockAmmo = 10;
    private bool isGameOver = false;

    private void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("MockHUDTester: GameUI not found!");
            return;
        }

        // Initialize static values
        gameUI.UpdateHP(mockHP);
        gameUI.UpdateAmmo(mockAmmo);
    }

    private void Update()
    {
        if (gameUI == null) return;

        // Only process keyboard inputs if we're not in the MainMenu scene
        if (!IsInMainMenu())
        {
            // Only update score and height in InGame scene and when game is not over
            if (SceneManager.GetActiveScene().name == "InGame" && !isGameOver)
            {
                // Update score and height every second
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    // Increment score and height
                    mockScore += 10;
                    mockHeight += 5;
                    
                    // Update HUD
                    gameUI.UpdateScore(mockScore);
                    gameUI.UpdateMaxHeight(mockHeight);

                    timer = 0f;
                }
            }

            // Press G to show game over screen
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Showing Game Over Screen!");
                isGameOver = true;
                gameUI.ShowGameOverScreen();
            }

            // Press H to decrease HP
            if (Input.GetKeyDown(KeyCode.H) && !isGameOver)
            {
                mockHP = Mathf.Max(0, mockHP - 1);
                gameUI.UpdateHP(mockHP);
                Debug.Log($"HP decreased to: {mockHP}");
            }

            // Press M to decrease ammo
            if (Input.GetKeyDown(KeyCode.M) && !isGameOver)
            {
                mockAmmo = Mathf.Max(0, mockAmmo - 1);
                gameUI.UpdateAmmo(mockAmmo);
                Debug.Log($"Ammo decreased to: {mockAmmo}");
            }
        }
    }

    private bool IsInMainMenu()
    {
        return SceneManager.GetActiveScene().name == "MainMenu";
    }
} 