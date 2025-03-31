using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Variables that you can adjust in the Unity Inspector
    public float rollSpeed = 5f;    // How fast the barrel rolls
    public float gravity = 9.8f;    // Gravity force pulling the barrel down
    
    // Private variables for internal use
    private Rigidbody rb;
    private bool isGrounded = false;
    
    // Called when the script instance is being loaded
    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        
        // Make sure our Rigidbody uses physics properly
        if (rb != null)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            
            // Prevent falling through by adjusting collision detection
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            
            // Add a bit of drag to prevent excessive speed
            rb.linearDamping = 0.5f;
        }
    }
    
    // Called once per physics frame
    void FixedUpdate()
    {
        // Check if we're on the ground
        CheckGrounded();
        
        // If we're on the ground, handle rolling based on the slope
        if (isGrounded)
        {
            HandleRolling();
        }
    }
    
    // Determines if the barrel is touching the ground
    void CheckGrounded()
    {
        // Cast a ray downward to see if we're on ground
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.6f);
    }
    
    // Handle the rolling motion of the barrel
    void HandleRolling()
    {
        // Get information about the ground below us
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.6f))
        {
            // Find the slope angle
            Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.right, hit.normal);
            
            // Calculate slope force based on angle
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            float slopeForce = slopeAngle / 45.0f * gravity;
            
            // Apply force in the direction of the slope
            rb.AddForce(slopeDirection.normalized * slopeForce, ForceMode.Acceleration);
            
            // Add some torque to make the barrel roll with its movement
            float rollAmount = rb.linearVelocity.x * rollSpeed;
            rb.AddTorque(Vector3.forward * -rollAmount);
        }
    }
    
    // Called when a collision occurs
    void OnCollisionEnter(Collision collision)
    {
        // Optional: Add some bounce or sound effect when the barrel hits something
        // You could play a sound here or add a small force
    }
}