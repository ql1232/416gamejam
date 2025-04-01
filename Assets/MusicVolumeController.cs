using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeController : MonoBehaviour
{
    private Slider masterVolumeSlider;
    
    void Start()
    {
        // Get the slider component
        masterVolumeSlider = GetComponent<Slider>();
        
        if (masterVolumeSlider != null)
        {
            // Set initial value based on current volume
            if (BackgroundMusicManager.instance != null && 
                BackgroundMusicManager.instance.GetComponent<AudioSource>() != null)
            {
                masterVolumeSlider.value = BackgroundMusicManager.instance.GetComponent<AudioSource>().volume;
            }
            
            // Add listener for when value changes
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }
    }
    
    void OnMasterVolumeChanged(float value)
    {
        // Update the master volume when slider changes
        if (BackgroundMusicManager.instance != null)
        {
            BackgroundMusicManager.instance.SetMasterVolume(value);
        }
    }
}