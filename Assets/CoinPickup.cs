using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [Header("Coin Properties")]
    public int ammoValue = 1;
    public float rotationSpeed = 100f;
    
    [Header("Visual Effects")]
    public GameObject collectEffect;
    public AudioClip collectSound;
    
    // Bobbing animation
    public float bobHeight = 0.2f;
    public float bobSpeed = 1.5f;
    private Vector3 startPosition;
    
    private GameManager gameManager;
    
    void Start()
    {
        // Store starting position for bobbing animation
        startPosition = transform.position;
        
        // Find the game manager
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found! Coin pickup won't increase ammo.");
        }
    }
    
    void Update()
    {
        // Rotate the coin
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add ammo to player
            if (gameManager != null)
            {
                gameManager.AddAmmo(ammoValue);
            }
            
            // Play collection sound if available
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            // Spawn collection effect if available
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }
            
            // Destroy the coin
            Destroy(gameObject);
        }
    }
}