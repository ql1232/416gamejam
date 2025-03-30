/*
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody rigidbody;
    private bool canJump = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rigidbody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidbody.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            canJump = true;
        }
    }
}





/*


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject projectilePrefab; // Reference to projectile
    public Transform shootPoint; // Point where projectiles spawn

    private Rigidbody rb;
    private bool canJump = true;
    private Transform cachedShootPoint; // Backup in case shootPoint gets unassigned

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Create shoot point if none assigned
        if (shootPoint == null)
        {
            GameObject sp = new GameObject("ShootPoint");
            sp.transform.parent = transform;
            sp.transform.localPosition = new Vector3(1f, 0.5f, 0); // To the right of the player
            shootPoint = sp.transform;
        }

        // Cache the reference to prevent loss
        cachedShootPoint = shootPoint;
    }

    void Update()
    {
        // Restore shoot point if it was lost
        if (shootPoint == null && cachedShootPoint != null)
        {
            shootPoint = cachedShootPoint;
        }

        // Movement code
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0); // FIXED: using correct property

        // Jump code
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0); // FIXED: using correct property
            canJump = false;
        }

        // ONLY shoot when right mouse button is clicked
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }

        // Also allow F key as backup fire
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Safety check for projectile prefab
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not assigned!");
            return;
        }

        // Safety check for shoot point
        if (shootPoint == null)
        {
            Debug.LogError("Shoot Point is missing!");
            return;
        }

        // Check if player has ammo
        if (GameManager.instance != null)
        {
            if (GameManager.instance.UseAmmo(1))
            {
                Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
                Debug.Log("Shot fired! Ammo left: " + GameManager.instance.ammunition);
            }
            else
            {
                Debug.Log("No ammo!");
            }
        }
        else
        {
            // If no GameManager, shoot anyway
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Debug.Log("Shot fired without GameManager!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) // Ground layer
        {
            canJump = true;
        }
    }
}
*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchHeight = 0.5f;  // How short player gets when crouching
    public float crouchSpeed = 0.1f;    // How quickly player crouches
    
    private Rigidbody rb;
    private bool canJump = true;
    private float originalHeight;      // To store the original height
    private bool isCrouching = false;  // Track crouch state
    private Transform playerModel;     // Reference to the visual model
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Get reference to the player's visual model (assuming it's the first child)
        // If your hierarchy is different, you might need to adjust this
        if (transform.childCount > 0)
        {
            playerModel = transform.GetChild(0);
            originalHeight = playerModel.localScale.y;
        }
        else
        {
            // If no child, use this object
            playerModel = transform;
            originalHeight = transform.localScale.y;
        }
    }
    
    void Update()
    {
        // Only process keyboard inputs if we're not in the MainMenu scene
        if (!IsInMainMenu())
        {
            // Movement code
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            
            // Jump code
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canJump = false;
            }
            
            // Crouch when X is pressed
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCrouch();
            }
            
            // Stand back up when X is released
            if (Input.GetKeyUp(KeyCode.X))
            {
                StopCrouch();
            }
        }
        
        // Handle the smooth transition for crouching/standing
        UpdateCrouchState();
    }
    
    private bool IsInMainMenu()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu";
    }
    
    void StartCrouch()
    {
        isCrouching = true;
    }
    
    void StopCrouch()
    {
        isCrouching = false;
    }
    
    void UpdateCrouchState()
    {
        Vector3 targetScale = playerModel.localScale;
        
        if (isCrouching)
        {
            // Gradually decrease height to crouch height
            targetScale.y = Mathf.Lerp(targetScale.y, crouchHeight, crouchSpeed);
        }
        else
        {
            // Gradually increase height back to original
            targetScale.y = Mathf.Lerp(targetScale.y, originalHeight, crouchSpeed);
        }
        
        // Apply the new scale
        playerModel.localScale = targetScale;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            canJump = true;
        }
    }
}