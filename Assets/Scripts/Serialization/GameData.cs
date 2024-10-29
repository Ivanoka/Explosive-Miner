using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameData
    {
        public int diamondCount;
        public int shovelCount;
        public List<int> dirtStateMatrix;

        [JsonIgnore] private static string _filePath = Application.persistentDataPath + "/data.json";

        public GameData(int diamondCount, int shovelCount, List<int> dirtStateMatrix)
        {
            this.diamondCount = diamondCount;
            this.shovelCount = shovelCount;
            this.dirtStateMatrix = dirtStateMatrix;
        }

        public static void SaveData(GameData gameData)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(gameData, Formatting.Indented);
                File.WriteAllText(_filePath, jsonData);
                Debug.Log("Data saved in the path: " + _filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GameData LoadData()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonData = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<GameData>(jsonData);
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