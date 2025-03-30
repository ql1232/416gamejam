using UnityEngine;

public class DeathCubeController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;
    private int direction = 1; // 1 for right, -1 for left
    
    void Update()
    {
        // Move the cube
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
        
        // Check if we need to change direction
        if (transform.position.x >= rightBoundary)
        {
            direction = -1; // Change direction to left
        }
        else if (transform.position.x <= leftBoundary)
        {
            direction = 1; // Change direction to right
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the cube collided with the player
        if (other.CompareTag("Player"))
        {
            // Reset the game
            if (GameManager.instance != null)
            {
                GameManager.instance.RestartGame();
            }
            else
            {
                // Fallback if no GameManager is found
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}