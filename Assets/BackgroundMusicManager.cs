using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    
    // Singleton pattern to ensure only one music manager exists
    public static BackgroundMusicManager instance;
    
    void Awake()
    {
        // If an instance already exists, destroy this one
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        // Make this the instance and don't destroy it when loading new scenes
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Add and configure AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f; // Default volume - can be adjusted through your UI
    }
    
    void Start()
    {
        // Start playing the music
        if (backgroundMusic != null)
        {
            audioSource.Play();
        }
    }
    
    // Method to set music volume from your UI slider
    public void SetMusicVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}