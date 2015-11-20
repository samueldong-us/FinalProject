using FinalProject.GameWaves;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal static class BehaviorFactory
    {
        public static void AddBehavior(Entity entity, SpawnInformation spawnInformation)
        {
            switch (spawnInformation.GetInformation<string>("Behavior Name"))
            {
                case "Catmull Rom":
                    {
                        AddCatmullRomBehavior(entity, spawnInformation);
                    } break;
                case "In Fire Out":
                    {
                        AddInFireOutBehavior(entity, spawnInformation);
                    } break;
            }
        }

        private static void AddCatmullRomBehavior(Entity entity, SpawnInformation spawnInformation)
        {
            List<Vector2> path = spawnInformation.GetInformation<List<Vector2>>("Path");
            float speed = Values.UnitMovementSpeeds[spawnInformation.GetInformation<string>("Unit Name")];
            float startFiringPercentage = spawnInformation.GetInformation<float>("Start Firing Percentage");
            float stopFiringPercentage = spawnInformation.GetInformation<float>("Stop Firing Percentage");
            entity.Position = path[0];
            new CatmullRomBehaviorComponent(entity, path, speed, startFiringPercentage, stopFiringPercentage);
        }

        private static void AddInFireOutBehavior(Entity entity, SpawnInformation spawnInformation)
        {
            Vector2 spawnPosition = spawnInformation.GetInformation<Vector2>("Spawn Position");
            Vector2 switchPosition = spawnInformation.GetInformation<Vector2>("Switch Position");
            float inSpeed = Values.UnitMovementSpeeds[spawnInformation.GetInformation<string>("Unit Name")] * 4;
            float outSpeed = Values.UnitMovementSpeeds[spawnInformation.GetInformation<string>("Unit Name")];
            int ceaseFireY = spawnInformation.GetInformation<int>("Cease Fire Y");
            entity.Position = spawnPosition;
            new InFireOutBehaviorComponent(entity, switchPosition, inSpeed, outSpeed, ceaseFireY);
        }
    }
}