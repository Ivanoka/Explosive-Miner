using System;
using System.Collections.Generic;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameData
    {
        public int diamondCount;
        public int shovelCount;
        public List<int> dirtStateMatrix;

        public GameData(int diamondCount, int shovelCount, List<int> dirtStateMatrix)
        {
            this.diamondCount = diamondCount;
            this.shovelCount = shovelCount;
            this.dirtStateMatrix = dirtStateMatrix;
        }
    }
}