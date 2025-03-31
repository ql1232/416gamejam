using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    private Slider musicVolumeSlider;
    
    void Start()
    {
        // Get the slider component
        musicVolumeSlider = GetComponent<Slider>();
        
        if (musicVolumeSlider != null)
        {
            // Set initial value based on current volume
            if (BackgroundMusicManager.instance != null && 
                BackgroundMusicManager.instance.GetComponent<AudioSource>() != null)
            {
                musicVolumeSlider.value = BackgroundMusicManager.instance.GetComponent<AudioSource>().volume;
            }
            
            // Add listener for when value changes
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
    }
    
    void OnMusicVolumeChanged(float value)
    {
        // Update the music volume when slider changes
        if (BackgroundMusicManager.instance != null)
        {
            BackgroundMusicManager.instance.SetMusicVolume(value);
        }
    }
}