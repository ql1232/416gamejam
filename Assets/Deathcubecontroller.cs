/*
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

*/

/*

using UnityEngine;
using System.Collections;

public class DeathCubeController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;
    
    private int direction = 1; // 1 for right, -1 for left
    private float originalSpeed;
    private float currentSpeedModifier = 1f;
    private Coroutine speedModifierCoroutine;
    
    void Start()
    {
        originalSpeed = moveSpeed;
    }
    
    void Update()
    {
        // Move the cube with the current speed and modifier
        transform.Translate(Vector3.right * direction * moveSpeed * currentSpeedModifier * Time.deltaTime);
        
        // Change direction when hitting boundaries
        if (transform.position.x >= rightBoundary)
        {
            direction = -1; // Change direction to left
        }
        else if (transform.position.x <= leftBoundary)
        {
            direction = -1; // Change direction to right
        }
    }
    
    // Called by the power-up
    public void ApplySpeedModifier(float modifier, float duration)
    {
        // Stop any existing modifier coroutine
        if (speedModifierCoroutine != null)
        {
            StopCoroutine(speedModifierCoroutine);
        }
        
        // Start a new modifier coroutine
        speedModifierCoroutine = StartCoroutine(SpeedModifierCoroutine(modifier, duration));
    }
    
    IEnumerator SpeedModifierCoroutine(float modifier, float duration)
    {
        // Apply the modifier
        currentSpeedModifier = modifier;
        
        // Change the color to indicate slowed status
        Renderer cubeRenderer = GetComponent<Renderer>();
        Color originalColor = cubeRenderer.material.color;
        cubeRenderer.material.color = Color.blue;
        
        Debug.Log("Death cube slowed down for " + duration + " seconds!");
        
        // Wait for the duration
        yield return new WaitForSeconds(duration);
        
        // Restore original speed and color
        currentSpeedModifier = 1f;
        cubeRenderer.material.color = originalColor;
        
        Debug.Log("Death cube speed restored!");
        
        speedModifierCoroutine = null;
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



{
    public float moveSpeed = 3f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;
    
    private int direction = 1; // 1 for right, -1 for left
    private float originalSpeed;
    private float currentSpeedModifier = 1f;
    private Coroutine speedModifierCoroutine;
    
    void Start()
    {
        originalSpeed = moveSpeed;
    }
    
    void Update()
    {
        // Move the cube with the current speed and modifier
        transform.Translate(Vector3.right * direction * moveSpeed * currentSpeedModifier * Time.deltaTime);
        
        // Change direction when hitting boundaries
        if (transform.position.x >= rightBoundary)
        {
            direction = -1; // Change direction to left
        }
        else if (transform.position.x <= leftBoundary)
        {
            direction = 1; // Change direction to right
        }
    }
    
    // Called by the power-up
    public void ApplySpeedModifier(float modifier, float duration)
    {
        // Stop any existing modifier coroutine
        if (speedModifierCoroutine != null)
        {
            StopCoroutine(speedModifierCoroutine);
        }
        
        // Start a new modifier coroutine
        speedModifierCoroutine = StartCoroutine(SpeedModifierCoroutine(modifier, duration));
    }
    
    IEnumerator SpeedModifierCoroutine(float modifier, float duration)
    {
        // Apply the modifier
        currentSpeedModifier = modifier;
        
        // Change the color to indicate status
        Renderer cubeRenderer = GetComponent<Renderer>();
        Color originalColor = cubeRenderer.material.color;
        
        // Different color based on effect type
        if (modifier == 0f)
        {
            // Frozen - ice blue color
            cubeRenderer.material.color = new Color(0.5f, 0.8f, 1f);
            Debug.Log("Death cube FROZEN for " + duration + " seconds!");
        }
        else
        {
            // Slowed - normal blue
            cubeRenderer.material.color = Color.blue;
            Debug.Log("Death cube slowed down for " + duration + " seconds!");
        }
        
        // Wait for the duration
        yield return new WaitForSeconds(duration);
        
        // Restore original speed and color
        currentSpeedModifier = 1f;
        cubeRenderer.material.color = originalColor;
        
        Debug.Log("Death cube speed restored!");
        
        speedModifierCoroutine = null;
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

*/

using UnityEngine;
using System.Collections;

public class DeathCubeController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;
    
    private int direction = 1; // 1 for right, -1 for left
    private float originalSpeed;
    private float currentSpeedModifier = 1f;
    private Coroutine speedModifierCoroutine;

    void Start()
    {
        originalSpeed = moveSpeed;
    }

    void Update()
    {
        // Move the cube with the current speed and modifier
        transform.Translate(Vector3.right * direction * moveSpeed * currentSpeedModifier * Time.deltaTime);
        
        // Change direction when hitting boundaries
        if (transform.position.x >= rightBoundary)
        {
            direction = -1; // Change direction to left
        }
        else if (transform.position.x <= leftBoundary)
        {
            direction = 1; // Change direction to right (FIXED typo here)
        }
    }

    // Called by the power-up
    public void ApplySpeedModifier(float modifier, float duration)
    {
        // Stop any existing modifier coroutine
        if (speedModifierCoroutine != null)
        {
            StopCoroutine(speedModifierCoroutine);
        }

        // Start a new modifier coroutine
        speedModifierCoroutine = StartCoroutine(SpeedModifierCoroutine(modifier, duration));
    }

    IEnumerator SpeedModifierCoroutine(float modifier, float duration)
    {
        // Apply the modifier
        currentSpeedModifier = modifier;

        // Change the color to indicate status
        Renderer cubeRenderer = GetComponent<Renderer>();
        Color originalColor = cubeRenderer.material.color;

        if (modifier == 0f)
        {
            // Frozen - ice blue color
            cubeRenderer.material.color = new Color(0.5f, 0.8f, 1f);
            Debug.Log("Death cube FROZEN for " + duration + " seconds!");
        }
        else
        {
            // Slowed - normal blue
            cubeRenderer.material.color = Color.blue;
            Debug.Log("Death cube slowed down for " + duration + " seconds!");
        }

        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore original speed and color
        currentSpeedModifier = 1f;
        cubeRenderer.material.color = originalColor;

        Debug.Log("Death cube speed restored!");

        speedModifierCoroutine = null;
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
