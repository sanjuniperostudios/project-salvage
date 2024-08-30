using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    //Serialize adds a field under this script which allows input. AudioMixer must be an audio mixer, and Slider must be a slider. Define a mixer and 4 sliders.
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider envmtSlider;
    


    private void Start()
    {
        //On start, if there are player preferences for music volume, then load all volume settings.
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        //Otherwise, simply initialize volume settings for first-time use.
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
            SetENVMTVolume();
        }
    }

    //Define a float by the value of the slider under Master Slider in the script. Set the volume in the mixer under "Master" to be a converted linear to logarithmic volume 
    //Then, change our player's preferences to be that Master Volume. Next time we launch, these settings will be saved. This is called a Key.
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    //Repeat but for music.
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetENVMTVolume()
    {
        float volume = envmtSlider.value;
        myMixer.SetFloat("ENVMT", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("envmtVolume", volume);
    }

    //Since keys exist, get those keys and set the values of our sliders to those keys. Basically, since preferences exist, load those preferences.
    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        envmtSlider.value = PlayerPrefs.GetFloat("envmtVolume");
        
        //After loading preferences, load those settings into our mixer.
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetENVMTVolume();

    }
}
