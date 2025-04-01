using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchHeight = 0.5f;  // How short player gets when crouching
    public float crouchSpeed = 0.1f;   // How quickly player crouches
		private GameUI gameUI;
    
    [Header("Sound Effects")]
    public AudioClip jumpSound;        // Sound when jumping
    public AudioClip crouchSound;      // Sound when crouching
    public AudioClip standSound;       // Sound when standing back up
    [Range(0, 1)]
    public float soundVolume = 0.7f;   // Volume control for all sounds
    
    private Rigidbody rb;
    private bool canJump = true;
    private float originalHeight;      // To store the original height
    private bool isCrouching = false;  // Track crouch state
    private Transform playerModel;     // Reference to the visual model
    private AudioSource audioSource;   // Reference to the audio source
    
    void Start()
    {
	gameUI = FindObjectOfType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("OutOfFrame: GameUI not found!");
        }
        rb = GetComponent<Rigidbody>();
        
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Configure audio source
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 0 = 2D sound, 1 = 3D sound
        audioSource.volume = soundVolume;
        
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
        if (!IsInMainMenu() && gameUI.GetCurrentHP() > 0)
        {
            // Movement code
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            
            // Jump code
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canJump = false;
                
                // Play jump sound
                PlaySound(jumpSound);
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
        if (!isCrouching)
        {
            isCrouching = true;
            PlaySound(crouchSound);
        }
    }
    
    void StopCrouch()
    {
        if (isCrouching)
        {
            isCrouching = false;
            PlaySound(standSound);
        }
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
    
    // Method to handle playing audio clips
    public void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, soundVolume);
        }
    }
}