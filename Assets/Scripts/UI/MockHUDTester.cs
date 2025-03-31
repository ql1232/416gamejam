using UnityEngine;
using UnityEngine.SceneManagement;

public class MockHUDTester : MonoBehaviour
{
    private GameUI gameUI;
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