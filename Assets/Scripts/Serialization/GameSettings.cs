using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameSettings
    {
        public int resolutionPreference;
        public bool isFullscreen;
        public int qualitySettingPreference;

        [JsonIgnore] private static string _filePath = Application.persistentDataPath + "/settings.json";

        public GameSettings(int resolutionPreference, bool isFullscreen, int qualitySettingPreference)
        {
            this.resolutionPreference = resolutionPreference;
            this.isFullscreen = isFullscreen;
            this.qualitySettingPreference = qualitySettingPreference;
        }

        public static void SaveData(GameSettings gameSettings)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(gameSettings, Formatting.Indented);
                File.WriteAllText(_filePath, jsonData);
                Debug.Log("Settings saved in the path: " + _filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GameSettings LoadData()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonData = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<GameSettings>(jsonData);
                }
                else
                {
                    throw new IOException("The file does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}