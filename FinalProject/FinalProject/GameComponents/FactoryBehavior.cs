using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal static class FactoryBehavior
    {
        public static void AddCatmullRomBehavior(Entity entity, List<Vector2> path, float speed, float startFiringPercentage, float stopFiringPercentage)
        {
            entity.Position = path[0];
            new ComponentBehaviorCatmullRom(entity, path, speed, startFiringPercentage, stopFiringPercentage);
        }

        public static void AddInFireOutBehavior(Entity entity, Vector2 spawnPosition, Vector2 switchPosition, float inSpeed, float outSpeed, int ceaseFireY)
        {
            entity.Position = spawnPosition;
            new ComponentBehaviorInFireOut(entity, switchPosition, inSpeed, outSpeed, ceaseFireY);
        }
    }
}