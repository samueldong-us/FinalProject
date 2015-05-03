using FinalProject.GameComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Screens
{
    internal static class FactoryBehavior
    {
        public static void AddCatmullRomBehavior(Entity entity, List<Vector2> path, float speed, float startFiringPercentage, float stopFiringPercentage)
        {
            entity.Position = path[0];
            new ComponentCatmullRomBehavior(entity, path, speed, startFiringPercentage, stopFiringPercentage);
        }

        public static void AddInFireOutBehavior(Entity entity, Vector2 spawnPosition, Vector2 switchPosition, float inSpeed, float outSpeed)
        {
            entity.Position = spawnPosition;
            new ComponentInFireOutBehavior(entity, switchPosition, inSpeed, outSpeed);
        }
    }
}