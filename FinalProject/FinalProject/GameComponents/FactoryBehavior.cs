using FinalProject.GameWaves;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal static class FactoryBehavior
    {
        public static void AddBehavior(Entity entity, SpawnInformation spawnInformation)
        {
            switch (spawnInformation.GetInformation<string>("Behavior Name"))
            {
                case "CatmullRom":
                    {
                        AddCatmullRomBehavior(entity, spawnInformation);
                    } break;
                case "InFireOut":
                    {
                        AddInFireOutBehavior(entity, spawnInformation);
                    } break;
            }
        }

        private static void AddCatmullRomBehavior(Entity entity, SpawnInformation spawnInformation)
        {
            List<Vector2> path = spawnInformation.GetInformation<List<Vector2>>("Path");
            float speed = spawnInformation.GetInformation<float>("Speed");
            float startFiringPercentage = spawnInformation.GetInformation<float>("Start Firing Percentage");
            float stopFiringPercentage = spawnInformation.GetInformation<float>("Stop Firing Percentage");
            entity.Position = path[0];
            new ComponentBehaviorCatmullRom(entity, path, speed, startFiringPercentage, stopFiringPercentage);
        }

        private static void AddInFireOutBehavior(Entity entity, SpawnInformation spawnInformation)
        {
            Vector2 spawnPosition = spawnInformation.GetInformation<Vector2>("Spawn Position");
            Vector2 switchPosition = spawnInformation.GetInformation<Vector2>("Switch Position");
            float inSpeed = spawnInformation.GetInformation<float>("In Speed");
            float outSpeed = spawnInformation.GetInformation<float>("Out Speed");
            int ceaseFireY = spawnInformation.GetInformation<int>("Cease Fire Y");
            entity.Position = spawnPosition;
            new ComponentBehaviorInFireOut(entity, switchPosition, inSpeed, outSpeed, ceaseFireY);
        }
    }
}