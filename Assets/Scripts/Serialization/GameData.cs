using System;
using UnityEngine;
using System.Collections.Generic;

namespace ExplosiveMiner.Serialization
{
    [Serializable]
    public class GameData : SerializableData<GameData>
    {
        public int diamondCount;
        public int shovelCount;
        public List<int> dirtStateMatrix;

        public override string FilePath => Application.persistentDataPath + "/data.json";

        public GameData() {}

        public GameData(int diamondCount, int shovelCount, List<int> dirtStateMatrix)
        {
            this.diamondCount = diamondCount;
            this.shovelCount = shovelCount;
            this.dirtStateMatrix = dirtStateMatrix;
        }
    }
}