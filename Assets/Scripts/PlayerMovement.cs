using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Horizontal movement speed
    public float jumpForce = 10f;  // Force applied when jumping

    private Rigidbody rb;
    private bool canJump = true;   // Tracks if the player can jump

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get horizontal input (A for left, D for right).
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Apply horizontal movement while preserving the vertical velocity.
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump when Space is pressed and the player is allowed to jump.
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            canJump = false;  // Consume the jump until grounded again.
        }
    }

    // Reset jump when colliding with objects on the "Ground" layer.
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object's layer is "Ground"
        if (collision.gameObject.layer == 6)
        {
            canJump = true;
        }
    }
}
