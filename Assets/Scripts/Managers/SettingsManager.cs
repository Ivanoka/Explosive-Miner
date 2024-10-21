using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace ExplosiveMiner.Managers
{
    public class SettingsManager : MonoBehaviour
    {
        private const string SettingsFileName = "settings";
        private string _settingsFilePath;

        [Header("UI")]
        [SerializeField] private Dropdown _resolutionDropdown;
        [SerializeField] private Toggle _fullscreenToggle;
        [SerializeField] private Dropdown _qualityDropdown;

        private Resolution[] _resolutions;
        private int _currentResolutionIndex;
        
        private void Start()
        {
            _settingsFilePath = Application.persistentDataPath + "/" + SettingsFileName +".json";

            InitializeResolutionDropdown();
            LoadData(_currentResolutionIndex);          
        }

        private void InitializeResolutionDropdown()
        {
            _resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            Resolution[] allResolutions = Screen.resolutions;
            List<Resolution> availableResolutions = new List<Resolution>();
            int currentRefreshRate = Screen.currentResolution.refreshRate;
            for (int i = 0; i < allResolutions.Length; i++)
            {
                if (allResolutions[i].refreshRate == currentRefreshRate)
                {
                    availableResolutions.Add(allResolutions[i]);

                    options.Add(allResolutions[i].width + "x" + allResolutions[i].height + " (" + allResolutions[i].refreshRateRatio + "Hz)");
                    
                    if (allResolutions[i].width == Screen.currentResolution.width && allResolutions[i].height == Screen.currentResolution.height)
                    {
                        _currentResolutionIndex = i;
                    }
                }
            }
            _resolutions = availableResolutions.ToArray();
            _resolutionDropdown.AddOptions(options);
            _resolutionDropdown.RefreshShownValue(); 
        }

        public void NextResolution()
        {
            _resolutionDropdown.value++;
            ApplyResolution();
        }

        public void PreviousResolution()
        {
            _resolutionDropdown.value--;
            ApplyResolution();
        }

        private void ApplyResolution()
        {
            Resolution resolution = _resolutions[_resolutionDropdown.value];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void NextQuality()
        {
            _qualityDropdown.value++;
            ApplyQuality();
        }

        public void PreviousQuality()
        {
            _qualityDropdown.value--;
            ApplyQuality();
        }

        private void ApplyQuality()
        {
            QualitySettings.SetQualityLevel(_qualityDropdown.value);
        }

        private void SaveData()
        {
            try
            {
                Serialization.GameSettings settingsFileData = new Serialization.GameSettings(_resolutionDropdown.value, Screen.fullScreen, _qualityDropdown.value);
                string jsonData = JsonConvert.SerializeObject(settingsFileData, Formatting.Indented);
                System.IO.File.WriteAllText(_settingsFilePath, jsonData);
                Debug.Log("Settings saved in the path: " + _settingsFilePath);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error while saving settings: " + ex.Message);
            }
        }
        
        private void LoadData(int currentResolutionIndex)
        {
            try
            {
                if (System.IO.File.Exists(_settingsFilePath))
                {
                    string jsonData = System.IO.File.ReadAllText(_settingsFilePath);
                    Serialization.GameSettings settingsFileData = JsonConvert.DeserializeObject<Serialization.GameSettings>(jsonData);

                    _resolutionDropdown.value = settingsFileData.resolutionPreference;
                    _qualityDropdown.value = settingsFileData.qualitySettingPreference;

                    SetFullscreen(_fullscreenToggle.isOn);
                    ApplyResolution();
                    ApplyQuality();

                    Debug.Log("The settings has been successfully uploaded.");
                }
                else
                {
                    _resolutionDropdown.value = currentResolutionIndex;
                    _qualityDropdown.value = 3;
                }

                _fullscreenToggle.isOn = Screen.fullScreen;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error while loading settings: " + ex.Message);
            }
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }
    }
}