using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameConfig
    {
        public int matrixWidth;
        public int matrixHeight;
        public int matrixDepth;
        public int startShovelCount;
        public float diamondSpawnRate;

        [JsonIgnore] private static string _filePath = Application.dataPath + "/config.json";

        public GameConfig(int matrixWidth, int matrixHeight, int matrixDepth, int startShovelCount, float diamondSpawnRate)
        {
            this.matrixWidth = matrixWidth;
            this.matrixHeight = matrixHeight;
            this.matrixDepth = matrixDepth;
            this.startShovelCount = startShovelCount;
            this.diamondSpawnRate = diamondSpawnRate;
        }

        public static void SaveData(GameConfig gameConfig)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(gameConfig, Formatting.Indented);
                File.WriteAllText(_filePath, jsonData);
                Debug.Log("Data saved in the path: " + _filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GameConfig LoadData()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonData = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<GameConfig>(jsonData);
                }

                throw new IOException("The file does not exist");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}