using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal class SpawnInformation
    {
        public Dictionary<string, object> Information;
        public float SpawnTime;

        public SpawnInformation(float spawnTime)
        {
            SpawnTime = spawnTime;
            Information = new Dictionary<string, object>();
        }
    }
}