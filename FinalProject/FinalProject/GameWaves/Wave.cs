using FinalProject.GameComponents;
using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal class Wave
    {
        private List<SpawnInformation> contents;

        private float timePassed;

        public Wave()
        {
            contents = new List<SpawnInformation>();
            timePassed = 0;
        }

        public void AddSpawnInformation(SpawnInformation spawnInformation)
        {
            contents.Add(spawnInformation);
        }

        public bool Finished()
        {
            return contents.Count == 0;
        }

        public List<SpawnInformation> GetSpawnInformationToSpawn()
        {
            List<SpawnInformation> toSpawn = new List<SpawnInformation>();
            for (int i = contents.Count - 1; i >= 0; i--)
            {
                if (contents[i].SpawnTime < timePassed)
                {
                    toSpawn.Add(contents[i]);
                    contents.RemoveAt(i);
                }
            }
            return toSpawn;
        }

        public int TotalPossibleScore()
        {
            int total = 0;
            foreach (SpawnInformation spawnInformation in contents)
            {
                total += Values.UnitWorth[spawnInformation.GetInformation<string>("Unit Name")];
            }
            return total;
        }

        public void Update(float secondsPassed)
        {
            timePassed += secondsPassed;
        }
    }
}