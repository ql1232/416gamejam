/*

using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;
    
    void Start()
    {
        // Destroy the projectile after lifetime seconds
        Destroy(gameObject, lifetime);
    }
    
    void Update()
    {
        // Move right (x-axis) instead of forward (z-axis)
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if hit the death cube
        if (other.gameObject.name.Contains("DeathCube"))
        {
            // Destroy the death cube
            Destroy(other.gameObject);
            
            // Destroy this projectile
            Destroy(gameObject);
            
            // Add points or play sound if needed
            if (GameManager.instance != null)
            {
                GameManager.instance.AddAmmo(1); // Reward player with ammo
            }
        }
        else if (!other.CompareTag("Player")) // Don't destroy when hitting the player
        {
            // Destroy projectile when hitting anything else except player
            Destroy(gameObject);
        }
    }
}
*/


using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;

    void Start()
    {
        // Destroy the projectile after lifetime seconds
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move in the direction the projectile is facing
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if hit the death cube
        if (other.gameObject.name.Contains("DeathCube"))
        {
            Destroy(other.gameObject); // Destroy the cube
            Destroy(gameObject);       // Destroy the projectile

            if (GameManager.instance != null)
            {
                GameManager.instance.AddAmmo(1); // Reward player
            }
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy on contact with anything except player
        }
    }
}
