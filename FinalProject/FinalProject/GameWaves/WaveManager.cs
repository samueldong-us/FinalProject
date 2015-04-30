using FinalProject.GameComponents;
using FinalProject.Screens;
using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal class WaveManager
    {
        private const float TimeBetweenWaves = 2;

        private float timePassed;

        private List<Wave> waves;

        public WaveManager(List<Wave> waves)
        {
            this.waves = waves;
            timePassed = 0;
        }

        public List<Entity> GetEntitiesToSpawn()
        {
            if (waves.Count > 0)
            {
                List<Entity> toSpawn = new List<Entity>();
                foreach (SpawnInformation info in waves[0].GetSpawnInformationToSpawn())
                {
                    toSpawn.Add(UnitFactory.CreateFromSpawnInformation(info));
                }
                return toSpawn;
            }
            else
            {
                return null;
            }
        }

        public void Update(float secondsPassed)
        {
            timePassed += secondsPassed;
            if (timePassed > TimeBetweenWaves)
            {
                if (waves.Count > 0)
                {
                    waves[0].Update(secondsPassed);
                    if (CurrentWaveOver())
                    {
                        waves.RemoveAt(0);
                        timePassed = 0;
                    }
                }
            }
        }

        private bool CurrentWaveOver()
        {
            return waves[0].Finished() && ScreenGame.Collisions.GetCount("Enemy") == 0;
        }
    }
}