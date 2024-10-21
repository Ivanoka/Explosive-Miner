using System;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameSettings
    {
        public int resolutionPreference;
        public bool isFullscreen;
        public int qualitySettingPreference;

        public GameSettings(int resolutionPreference, bool isFullscreen, int qualitySettingPreference)
        {
            this.resolutionPreference = resolutionPreference;
            this.isFullscreen = isFullscreen;
            this.qualitySettingPreference = qualitySettingPreference;
        }
    }
}