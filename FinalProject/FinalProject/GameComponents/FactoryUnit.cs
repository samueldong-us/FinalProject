using FinalProject.GameSaving;
using FinalProject.GameWaves;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal static class FactoryUnit
    {
        public static SaveGame.Difficulty Difficulty;

        public static int Stage;

        public static Entity CreateEntityFromSpawnInformation(SpawnInformation spawnInformation)
        {
            string unitName = spawnInformation.GetInformation<string>("Unit Name");
            Entity unit = new Entity();
            unit.Rotation = spawnInformation.GetInformation<float>("Starting Rotation");
            new ComponentVelocityAcceleration(unit, Vector2.Zero, Vector2.Zero);
            FactoryBehavior.AddBehavior(unit, spawnInformation);
            FactoryWeapon.AddWeapon(unit, spawnInformation, Difficulty, Stage);
            new ComponentHealth(unit, Values.UnitValues[Difficulty][Stage][spawnInformation.GetInformation<string>("Unit Name")].Health);
            if (Values.UnitHealthBars.ContainsKey(unitName))
            {
                new ComponentHealthBar(unit, Values.UnitHealthBars[unitName].BarRectangle, Values.UnitHealthBars[unitName].Offset);
            }
            new ComponentCollider(unit, GameAssets.Unit[unitName], GameAssets.UnitTriangles[unitName], "Enemy");
            new ComponentRemoveOnDeath(unit);
            if (spawnInformation.GetInformation<bool>("Rotate Based On Velocity"))
            {
                new ComponentVelocityBasedRotation(unit);
            }
            new ComponentTextureRenderer(unit, GameAssets.UnitTexture, GameAssets.Unit[unitName], Color.White, "Enemy");
            return unit;
        }
    }
}