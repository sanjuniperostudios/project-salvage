using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;

    //Define a new list of resolutions called resolutions.
    Resolution[] resolutions;

    //Define a new list of resolutions called filteredResolutions.
    List<Resolution> filteredResolutions;
    
    private double currentRefreshRate;
    void Start()
    {
        //Intialize the resolutions list using available resolutions.
        resolutions = Screen.resolutions;

        //Clear all options in dropdown.
        resolutionDropdown.ClearOptions();

        //Define new memory for filteredResolutions to be the right size.
        filteredResolutions = new List<Resolution>();

        //Get the current refresh rate of the monitor.
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        //Make a new list of strings called options.
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        //Filter out resolutions with a refresh rate that is not current. Not interested in those.
        for(int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        //Convert available resolutions to strings so AddOptions can read them.
        for(int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(resolutionOption);
            if(filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //If preferences exist, set the resolution to be that.
        if(PlayerPrefs.HasKey("resolutionPrefs"))
        {
            SetResolution(PlayerPrefs.GetInt("resolutionPrefs"));
            return;
        }

        //Otherwise, set to highest available resolution (which is likely desired).
        SetResolution(filteredResolutions.Count);
    }

    public void SetResolution (int resolutionIndex)
    {
        //Set the resolution to be the option thats the index.
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        //Set the index of this option to be the new preference.
        PlayerPrefs.SetInt("resolutionPrefs", resolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
