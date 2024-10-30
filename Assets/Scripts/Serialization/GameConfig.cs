using System;
using UnityEngine;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameConfig : SerializableData<GameConfig>
    {
        public int matrixWidth;
        public int matrixHeight;
        public int matrixDepth;
        public int startShovelCount;
        public float diamondSpawnRate;

        public override string FilePath => Application.dataPath + "/config.json";

        public GameConfig() {}

        public GameConfig(int matrixWidth, int matrixHeight, int matrixDepth, int startShovelCount, float diamondSpawnRate)
        {
            this.matrixWidth = matrixWidth;
            this.matrixHeight = matrixHeight;
            this.matrixDepth = matrixDepth;
            this.startShovelCount = startShovelCount;
            this.diamondSpawnRate = diamondSpawnRate;
        }
    }
}