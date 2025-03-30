using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float raycastDistance = 1f;
    public LayerMask obstacleLayer;
    
    [Header("Properties")]
    public int health = 1;
    public int damageToPlayer = 1;
    
    private Vector3 direction = Vector3.right;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
        }
        
        // Tag as enemy
        gameObject.tag = "Enemy";
    }
    
    void Update()
    {
        // Check for obstacles or edges
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + (direction * 0.5f);
        raycastOrigin.y -= 0.5f; // Cast slightly down to detect edges
        
        bool shouldChangeDirection = false;
        
        // Cast forward to detect walls
        if (Physics.Raycast(transform.position, direction, out hit, raycastDistance, obstacleLayer))
        {
            shouldChangeDirection = true;
        }
        
        // Cast down to detect edges
        if (!Physics.Raycast(raycastOrigin, Vector3.down, 1f, obstacleLayer))
        {
            shouldChangeDirection = true;
        }
        
        // Change direction if needed
        if (shouldChangeDirection)
        {
            direction = -direction;
        }
        
        // Move in current direction
        Vector3 movement = direction * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if hit by projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
        
        // Check if collided with player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage player
            GameObject player = collision.gameObject;
            GameManager gameManager = FindObjectOfType<GameManager>();
            
            if (gameManager != null)
            {
                gameManager.TakeDamage(damageToPlayer);
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            // Optional: Spawn effect or play sound
            Destroy(gameObject);
        }
    }
    
    // Visualize the patrol path in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        // Draw the raycast for wall detection
        Gizmos.DrawRay(transform.position, direction * raycastDistance);
        
        // Draw the raycast for edge detection
        Vector3 edgeRayOrigin = transform.position + (direction * 0.5f);
        edgeRayOrigin.y -= 0.5f;
        Gizmos.DrawRay(edgeRayOrigin, Vector3.down);
    }
}