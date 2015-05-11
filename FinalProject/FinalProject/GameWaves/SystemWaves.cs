using FinalProject.GameComponents;
using FinalProject.Screens;
using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal class SystemWaves
    {
        private const float TimeBetweenWaves = 2;
        private float timePassed;
        private List<Wave> waves;

        public SystemWaves(List<Wave> waves)
        {
            this.waves = waves;
            timePassed = 0;
        }

        public int GetNumberOfWaves()
        {
            return waves.Count;
        }

        public int GetTotalPossibleScore()
        {
            int total = 0;
            foreach (Wave wave in waves)
            {
                total += wave.TotalPossibleScore();
            }
            return total;
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
            if (waves.Count > 0)
            {
                foreach (SpawnInformation info in waves[0].GetSpawnInformationToSpawn())
                {
                    ScreenGame.Entities.AddEntity(FactoryUnit.CreateEntityFromSpawnInformation(info));
                }
            }
        }

        private bool CurrentWaveOver()
        {
            return waves[0].Finished() && ScreenGame.Collisions.GetCount("Enemy") == 0;
        }
    }
}