using FinalProject.GameComponents;
using FinalProject.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameWaves
{
    internal class WaveManager
    {
        private const float TimeBetweenWaves = 3;
        private float timePassed;
        private List<Wave> waves;

        public WaveManager(List<Wave> waves)
        {
            this.waves = waves;
            timePassed = 0;
        }

        public List<Entity> GetToSpawn()
        {
            if (waves.Count > 0)
            {
                List<Entity> toSpawn = new List<Entity>();
                List<SpawnInformation> spawnInformation = waves[0].GetToSpawn();
                foreach (SpawnInformation info in spawnInformation)
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
                }
                if (waves[0].Finished() && GameScreen.CollidersEnemies.Count == 0)
                {
                    waves.RemoveAt(0);
                    timePassed = 0;
                }
            }
        }
    }
}