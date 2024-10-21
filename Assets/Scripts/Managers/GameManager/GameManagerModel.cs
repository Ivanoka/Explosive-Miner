using System.Collections.Generic;

namespace ExplosiveMiner.Managers
{
    public class GameManagerModel
    {
        public int ShovelCount { get; set; }
        public int StartShovelCount { get; set; }
        public int DiamondCount { get; set; }
        public float DiamondSpawnRate { get; set; }
        public int MatrixWidth { get; set; }
        public int MatrixHeight { get; set; }
        public int MatrixDepth { get; set; }
        public List<int> DirtStateMatrix { get; set; }

        public GameManagerModel(int shovelCount, int diamondCount, float diamondSpawnRate, int matrixWidth, int matrixHeight, int matrixDepth)
        {
            ShovelCount = StartShovelCount = shovelCount;
            DiamondCount = diamondCount;
            DiamondSpawnRate = diamondSpawnRate;
            MatrixWidth = matrixWidth;
            MatrixHeight = matrixHeight;
            MatrixDepth = matrixDepth;
            DirtStateMatrix = new List<int>();
        }
    }
}