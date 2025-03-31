/*

using UnityEngine;

public class FreezeTimePowerUp : MonoBehaviour
{
    public float duration = 5f;            // How long the freeze lasts
    public float rotateSpeed = 50f;        // How fast the powerup rotates
    public float hoverHeight = 0.3f;       // How high it hovers
    public float hoverSpeed = 1f;          // Speed of hovering
    
    private Vector3 startPosition;
    private float timeOffset;
    
    void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.value * 2f * Mathf.PI; // Random offset for hovering
    }
    
    void Update()
    {
        // Rotate the power-up
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        // Make it hover up and down
        float newY = startPosition.y + Mathf.Sin((Time.time + timeOffset) * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if player collected the power-up
        if (other.CompareTag("Player"))
        {
            Debug.Log("Freeze power-up collected!");
            
            // Find all DeathCubes in the scene
            DeathCubeController[] deathCubes = FindObjectsOfType<DeathCubeController>();
            
            // Apply freeze to each death cube (0 speed modifier = full stop)
            foreach (DeathCubeController cube in deathCubes)
            {
                cube.ApplySpeedModifier(0f, duration);
            }
            
            // Show visual feedback
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            
            // Create freeze effect (optional)
            CreateFreezeEffect();
            
            // Destroy the power-up after the effect duration
            Destroy(gameObject, duration);
        }
    }
    
    void CreateFreezeEffect()
    {
        // This is a placeholder for a visual effect
        // You could instantiate a particle effect here
        Debug.Log("Time freeze effect activated!");
    }
}

*/

using UnityEngine;

public class FreezeTimePowerUp : MonoBehaviour
{
    public float duration = 5f;            // How long the freeze lasts
    public float rotateSpeed = 50f;        // How fast the powerup rotates
    public float hoverHeight = 0.3f;       // How high it hovers
    public float hoverSpeed = 1f;          // Speed of hovering
    
    [Header("Sound Effects")]
    public AudioClip collectSound;         // Sound played when collected
    [Range(0, 1)]
    public float soundVolume = 0.7f;       // Volume for sound effects
    
    private Vector3 startPosition;
    private float timeOffset;
    
    void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.value * 2f * Mathf.PI; // Random offset for hovering
    }
    
    void Update()
    {
        // Rotate the power-up
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        // Make it hover up and down
        float newY = startPosition.y + Mathf.Sin((Time.time + timeOffset) * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if player collected the power-up
        if (other.CompareTag("Player"))
        {
            Debug.Log("Freeze power-up collected!");
            
            // Play collection sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, soundVolume);
            }
            
            // Find all DeathCubes in the scene
            DeathCubeController[] deathCubes = FindObjectsOfType<DeathCubeController>();
            
            // Apply freeze to each death cube (0 speed modifier = full stop)
            foreach (DeathCubeController cube in deathCubes)
            {
                cube.ApplySpeedModifier(0f, duration);
            }
            
            // Show visual feedback
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            
            // Create freeze effect (optional)
            CreateFreezeEffect();
            
            // Destroy the power-up after the effect duration
            Destroy(gameObject, duration);
        }
    }
    
    void CreateFreezeEffect()
    {
        // This is a placeholder for a visual effect
        // You could instantiate a particle effect here
        Debug.Log("Time freeze effect activated!");
    }
}