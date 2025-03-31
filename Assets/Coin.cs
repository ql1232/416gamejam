using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 90f;        // How fast the coin rotates
    public int value = 1;                  // Value of this coin
    
    void Update()
    {
        // Rotate the coin to make it more interesting
        transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if player collected the coin
        if (other.CompareTag("Player"))
        {
            // Add to the player's coin count
            if (GameManager.instance != null)
            {
                GameManager.instance.AddCoins(value);
                
                // Play sound if available
                if (GameManager.instance.coinPickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(GameManager.instance.coinPickupSound, transform.position);
                }
            }
            
            // Destroy the coin
            Destroy(gameObject);
        }
    }
}