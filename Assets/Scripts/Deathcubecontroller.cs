using UnityEngine;
using System.Collections;

public class DeathCubeController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;

    [Header("Sound Effects")]
    public AudioClip deathSound;           // Sound played when player dies
    public AudioClip freezeSound;          // Sound played when cube is frozen
    public AudioClip slowdownSound;        // Sound played when cube is slowed
    public AudioClip returnToNormalSound;  // Sound played when effect ends
    [Range(0, 1)]
    public float soundVolume = 0.7f;       // Volume for sound effects

    private int direction = 1; // 1 for right, -1 for left
    private float originalSpeed;
    private float currentSpeedModifier = 1f;
    private Coroutine speedModifierCoroutine;
    private AudioSource audioSource;
    private GameUI gameUI;

    void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("OutOfFrame: GameUI not found!");
        }
        originalSpeed = moveSpeed;

        // Add AudioSource component if it doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f; // 3D sound
            audioSource.volume = soundVolume;
        }
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

        if (modifier == 0f)
        {
            // Frozen - ice blue color
            cubeRenderer.material.color = new Color(0.5f, 0.8f, 1f);
            Debug.Log("Death cube FROZEN for " + duration + " seconds!");

            // Play freeze sound
            PlaySound(freezeSound);
        }
        else
        {
            // Slowed - normal blue
            cubeRenderer.material.color = Color.blue;
            Debug.Log("Death cube slowed down for " + duration + " seconds!");

            // Play slowdown sound
            PlaySound(slowdownSound);
        }

        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore original speed and color
        currentSpeedModifier = 1f;
        cubeRenderer.material.color = originalColor;

        // Play return to normal sound
        PlaySound(returnToNormalSound);

        Debug.Log("Death cube speed restored!");

        speedModifierCoroutine = null;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the cube collided with the player
        if (other.CompareTag("Player"))
        {
            // Play death sound
            if (deathSound != null)
            {
                // Use PlayClipAtPoint to ensure sound plays even if object is destroyed
                AudioSource.PlayClipAtPoint(deathSound, transform.position, soundVolume);
            }

            // Kill player
            if (GameManager.instance != null)
            {
                gameUI.UpdateHP(gameUI.GetCurrentHP() - 50);
                if (gameUI.GetCurrentHP() <= 0)
                {
                    gameUI.ShowGameOverScreen();
                }
            }
            else
            {
                // Fallback if no GameManager is found
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
        }

        // Check if the cube collided with a Bullet (layer 8)
        if (other.gameObject.layer == 8)  // Layer 8 is Bullet
        {
		gameUI.IncrementScore(20);
            Destroy(other.gameObject);  // Destroy the bullet
            Destroy(gameObject);        // Destroy the deathcube
        }
    }

    // Helper method to play sounds
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
