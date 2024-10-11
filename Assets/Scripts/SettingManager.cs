using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Dropdown qualityDropdown;
    
    private Resolution[] resolutions;

    private void Start()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        Resolution[] allResolutions = Screen.resolutions;
        List<Resolution> availableResolutions = new List<Resolution>();

        int currentRefreshRate = Screen.currentResolution.refreshRate;
        int currentResolutionIndex = 0;

        for (int i = 0; i < allResolutions.Length; i++)
        {
            if (allResolutions[i].refreshRate == currentRefreshRate)
            {
                availableResolutions.Add(allResolutions[i]);

                options.Add(allResolutions[i].width + "x" + allResolutions[i].height + " (" + allResolutions[i].refreshRateRatio + "Hz)");
                
                if (allResolutions[i].width == Screen.currentResolution.width && allResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }

        resolutions = availableResolutions.ToArray();

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue(); 

        LoadData(currentResolutionIndex);          
    }

    public void NextResolution()
    {
        resolutionDropdown.value++;
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void PreviousResolution()
    {
        resolutionDropdown.value--;
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void NextQuality()
    {
        qualityDropdown.value++;
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void PreviousQuality()
    {
        qualityDropdown.value--;
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    private void LoadData(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        }
        else
        {
            resolutionDropdown.value = currentResolutionIndex;
            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        }

        fullscreenToggle.isOn = Screen.fullScreen;

        if (PlayerPrefs.HasKey("QualitySettingPreference"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        }
        else
        {
            qualityDropdown.value = 3;
            PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);

        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
