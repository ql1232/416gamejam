using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    public static BackgroundMusicManager instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        
        GameObject musicObject = new GameObject("BackgroundMusic");
        audioSource = musicObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f; // Default volume
        audioSource.ignoreListenerPause = true; // Prevent music from freezing when game is paused
        
        // Make the music object persist between scenes
        DontDestroyOnLoad(musicObject);
    }
    
    void Start()
    {
        // Start playing the music
        if (backgroundMusic != null)
        {
            audioSource.Play();
        }
    }
}