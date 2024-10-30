using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace ExplosiveMiner.Serialization
{
    public abstract class SerializableData<T> where T : class
    {
        [JsonIgnore] public abstract string FilePath{ get; }

        public void SaveData()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(FilePath, jsonData);
                Debug.Log(GetType().Name + " saved in the path: " + FilePath);
            }
            catch (Exception ex)
            {
                Debug.LogError(GetType().Name + "Failed to save data: " + ex.Message);
                throw;
            }
        }

        public static T LoadData(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }

                throw new IOException("The file does not exist " + filePath);
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to load data: " + ex.Message);
                throw;
            }
        }
    }
}