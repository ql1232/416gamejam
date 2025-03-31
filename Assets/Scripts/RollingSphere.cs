/*

using UnityEngine;

public class RollingSphere : MonoBehaviour
{
    public float initialPush = 0f;  // Optional initial force
    
    private Rigidbody rb;
    
    void Start()
    {
        // Get or add required components
        rb = GetComponent<Rigidbody>();
        
        // If there's no Rigidbody, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // If there's no collider, add a SphereCollider
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<SphereCollider>();
        }
        
        // Configure the Rigidbody for reliable physics
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = 5f;
        rb.linearDamping = 0.2f;
        rb.angularDamping = 0.1f;
        
        // Apply initial push if set
        if (initialPush != 0)
        {
            rb.AddForce(Vector3.right * initialPush, ForceMode.Impulse);
        }
    }
    
    // Use this method to drop the sphere at a specific position
    public void DropAt(Vector3 position)
    {
        transform.position = position;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}

*/

/*
using UnityEngine;

public class RollingSphere : MonoBehaviour
{
    public string playerTag = "Player";  // Tag of the player object
    public float rollForce = 5f;         // Force applied to roll toward player
    public float maxSpeed = 10f;         // Maximum rolling speed
    public bool rollTowardPlayer = true; // Whether to automatically roll toward player
    public bool dropOnStart = true;      // Whether to automatically drop on start
    
    private Rigidbody rb;
    private Transform playerTransform;
    private bool isGrounded = false;
    
    void Start()
    {
        // Get or add required components
        rb = GetComponent<Rigidbody>();
        
        // If there's no Rigidbody, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // If there's no collider, add a SphereCollider
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<SphereCollider>();
        }
        
        // Configure the Rigidbody for reliable physics
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = 5f;
        rb.linearDamping = 0.2f;
        rb.angularDamping = 0.1f;
        
        // Find the player in the scene
        GameObject playerObject = GameObject.FindWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure your player has the tag: " + playerTag);
        }
    }
    
    void FixedUpdate()
    {
        CheckGrounded();
        
        // Only roll if we're on the ground and have a player reference
        if (isGrounded && rollTowardPlayer && playerTransform != null)
        {
            RollTowardPlayer();
        }
    }
    
    // Check if the sphere is on the ground
    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<SphereCollider>().radius + 0.1f);
    }
    
    // Roll toward the player's position
    void RollTowardPlayer()
    {
        // Get direction to player (only on x-axis for 2D platformer)
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; // Don't roll up/down
        directionToPlayer.z = 0; // Don't roll forward/backward in 3D space
        
        // Normalize and apply force
        if (directionToPlayer.magnitude > 0.1f)
        {
            Vector3 rollDirection = directionToPlayer.normalized;
            
            // Only apply force if we're below max speed
            if (rb.linearVelocity.magnitude < maxSpeed)
            {
                rb.AddForce(rollDirection * rollForce);
            }
        }
    }
    
    // Use this method to drop the sphere at a specific position
    public void DropAt(Vector3 position)
    {
        transform.position = position;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    
    // For debugging: show the direction to player
    void OnDrawGizmos()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }
}

*/

using UnityEngine;

public class DeathRollingSphere : MonoBehaviour
{
    public string playerTag = "Player";      // Tag of the player object
    public float rollForce = 5f;             // Force applied to roll toward player
    public float maxSpeed = 10f;             // Maximum rolling speed
    public bool rollTowardPlayer = true;     // Whether to automatically roll toward player
    public bool dropOnStart = true;          // Whether to automatically drop on start
    public Color sphereColor = Color.red;    // Color of the death sphere
    
    private Rigidbody rb;
    private Transform playerTransform;
    private bool isGrounded = false;
    
    void Start()
    {
        // Get or add required components
        rb = GetComponent<Rigidbody>();
        
        // If there's no Rigidbody, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // If there's no collider, add a SphereCollider
        if (GetComponent<Collider>() == null)
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;  // Make it a trigger to detect player collision
        }
        else
        {
            // Make sure the existing collider is a trigger
            Collider collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }
        
        // Configure the Rigidbody for reliable physics
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = 5f;
        rb.linearDamping = 0.2f;
        rb.angularDamping = 0.1f;
        
        // Set the color of the sphere to indicate danger
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = sphereColor;
        }
        
        // Find the player in the scene
        GameObject playerObject = GameObject.FindWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure your player has the tag: " + playerTag);
        }
        
        // Add a second non-trigger collider for physics interactions with the environment
        GameObject physicsCollider = new GameObject("PhysicsCollider");
        physicsCollider.transform.parent = transform;
        physicsCollider.transform.localPosition = Vector3.zero;
        physicsCollider.transform.localRotation = Quaternion.identity;
        physicsCollider.transform.localScale = Vector3.one * 0.9f; // Slightly smaller
        
        SphereCollider physicsSphereColl = physicsCollider.AddComponent<SphereCollider>();
        physicsSphereColl.isTrigger = false;
    }
    
    void FixedUpdate()
    {
        CheckGrounded();
        
        // Only roll if we're on the ground and have a player reference
        if (isGrounded && rollTowardPlayer && playerTransform != null)
        {
            RollTowardPlayer();
        }
    }
    
    // Check if the sphere is on the ground
    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 
                                     GetComponent<SphereCollider>().radius + 0.1f);
    }
    
    // Roll toward the player's position
    void RollTowardPlayer()
    {
        // Get direction to player (only on x-axis for 2D platformer)
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; // Don't roll up/down
        directionToPlayer.z = 0; // Don't roll forward/backward in 3D space
        
        // Normalize and apply force
        if (directionToPlayer.magnitude > 0.1f)
        {
            Vector3 rollDirection = directionToPlayer.normalized;
            
            // Only apply force if we're below max speed
            if (rb.linearVelocity.magnitude < maxSpeed)
            {
                rb.AddForce(rollDirection * rollForce);
            }
        }
    }
    
    // Use this method to drop the sphere at a specific position
    public void DropAt(Vector3 position)
    {
        transform.position = position;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    
    // Handle collision with the player
    void OnTriggerEnter(Collider other)
    {
        // Check if the sphere collided with the player
        if (other.CompareTag(playerTag))
        {
            // Kill the player
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
    
    // For debugging: show the direction to player
    void OnDrawGizmos()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }
}