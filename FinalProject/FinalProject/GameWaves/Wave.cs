using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal class Wave
    {
        private List<SpawnInformation> spawnInformation;
        private float timePassed;

        public Wave()
        {
            spawnInformation = new List<SpawnInformation>();
            timePassed = 0;
        }

        public void AddSpawnInformation(SpawnInformation spawnInformation)
        {
            this.spawnInformation.Add(spawnInformation);
        }

        public bool Finished()
        {
            return spawnInformation.Count == 0;
        }

        public List<SpawnInformation> GetToSpawn()
        {
            List<SpawnInformation> toSpawn = new List<SpawnInformation>();
            for (int i = spawnInformation.Count - 1; i >= 0; i--)
            {
                if (spawnInformation[i].SpawnTime < timePassed)
                {
                    toSpawn.Add(spawnInformation[i]);
                    spawnInformation.RemoveAt(i);
                }
            }
            return toSpawn;
        }

        public void Update(float secondsPassed)
        {
            timePassed += secondsPassed;
        }
    }
}