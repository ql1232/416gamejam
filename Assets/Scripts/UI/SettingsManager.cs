using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    // Default values
    private const float DEFAULT_MASTER_VOLUME = 1f;
    private const float DEFAULT_MUSIC_VOLUME = 1f;

    private void Start()
    {
        // Initialize display settings
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }

        // Initialize audio settings
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", DEFAULT_MASTER_VOLUME);
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", DEFAULT_MUSIC_VOLUME);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        // Apply saved audio settings
        ApplyAudioSettings();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetMasterVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    private void ApplyAudioSettings()
    {
        if (audioMixer != null)
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", DEFAULT_MASTER_VOLUME));
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", DEFAULT_MUSIC_VOLUME));
        }
    }

    public void ResetToDefaults()
    {
        // Reset display settings
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        }

        // Reset audio settings
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = DEFAULT_MASTER_VOLUME;
            PlayerPrefs.SetFloat("MasterVolume", DEFAULT_MASTER_VOLUME);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = DEFAULT_MUSIC_VOLUME;
            PlayerPrefs.SetFloat("MusicVolume", DEFAULT_MUSIC_VOLUME);
        }

        // Apply the reset audio settings
        ApplyAudioSettings();
    }
} 