using UnityEngine;

public class SpeedReducerPowerUp : MonoBehaviour
{
    public float slowdownFactor = 0.5f;    // How much to slow the cube (0.5 = half speed)
    public float duration = 5f;            // How long the effect lasts (seconds)
    public float rotateSpeed = 50f;        // How fast the powerup rotates
    
    void Update()
    {
        // Rotate the power-up to make it more noticeable
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if player collected the power-up
        if (other.CompareTag("Player"))
        {
            Debug.Log("Power-up collected!");
            
            // Find all DeathCubes in the scene
            DeathCubeController[] deathCubes = FindObjectsOfType<DeathCubeController>();
            
            // Apply slowdown to each death cube
            foreach (DeathCubeController cube in deathCubes)
            {
                cube.ApplySpeedModifier(slowdownFactor, duration);
            }
            
            // Hide the power-up
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            
            // Destroy the power-up after the effect duration
            Destroy(gameObject, duration);
        }
    }
}