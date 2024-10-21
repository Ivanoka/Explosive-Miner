using System;

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