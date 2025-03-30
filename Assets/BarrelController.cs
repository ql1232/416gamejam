using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public float rollSpeed = 5f;
    public float lifetime = 10f;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Initial push to start rolling
        rb.AddForce(Vector3.right * rollSpeed, ForceMode.Impulse);
        
        // Destroy barrel after lifetime to prevent too many objects
        Destroy(gameObject, lifetime);
    }
    
    void Update()
    {
        // Apply constant force for consistent rolling (optional)
        rb.AddTorque(Vector3.forward * rollSpeed * 0.5f);
        
        // If barrel falls below a certain y position, destroy it
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if barrel hit the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call player damage function if it exists
            if (GameManager.instance != null)
            {
                GameManager.instance.TakeDamage(1);
            }
        }
    }
}