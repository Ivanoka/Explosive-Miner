using System;
using UnityEngine;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameSettings : SerializableData<GameSettings>
    {
        public int resolutionPreference;
        public bool isFullscreen;
        public int qualitySettingPreference;

        public override string FilePath => Application.persistentDataPath + "/settings.json";

        public GameSettings() {}

        public GameSettings(int resolutionPreference, bool isFullscreen, int qualitySettingPreference)
        {
            this.resolutionPreference = resolutionPreference;
            this.isFullscreen = isFullscreen;
            this.qualitySettingPreference = qualitySettingPreference;
        }
    }
}