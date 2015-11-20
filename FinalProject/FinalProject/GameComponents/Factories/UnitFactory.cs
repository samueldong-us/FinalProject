using FinalProject.GameSaving;
using FinalProject.GameWaves;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal static class UnitFactory
    {
        public static SaveGame.Difficulty Difficulty;
        public static int Stage;

        public static Entity CreateEntityFromSpawnInformation(SpawnInformation spawnInformation)
        {
            string unitName = spawnInformation.GetInformation<string>("Unit Name");
            Entity unit = new Entity();
            unit.Rotation = spawnInformation.GetInformation<float>("Starting Rotation");
            new VelocityAccelerationComponent(unit, Vector2.Zero, Vector2.Zero);
            new HealthComponent(unit, Values.UnitValues[Difficulty][Stage][spawnInformation.GetInformation<string>("Unit Name")].Health);
            if (Values.UnitHealthBars.ContainsKey(unitName))
            {
                new HealthBarComponent(unit, Values.UnitHealthBars[unitName].BarRectangle, Values.UnitHealthBars[unitName].Offset);
            }
            new ColliderComponent(unit, GameAssets.Unit[unitName], GameAssets.UnitTriangles[unitName], "Enemy");
            new RemoveOnDeathComponent(unit);
            if (spawnInformation.GetInformation<bool>("Rotate Based On Velocity"))
            {
                new VelocityBasedRotationComponent(unit);
            }
            new TextureRendererComponent(unit, GameAssets.UnitTexture, GameAssets.Unit[unitName], Color.White, "Enemy");
            new AddWorthOnDeathComponent(unit, Values.UnitWorth[unitName]);
            new DealDamageComponent(unit, 1000000);
            BehaviorFactory.AddBehavior(unit, spawnInformation);
            WeaponFactory.AddWeapon(unit, spawnInformation, Difficulty, Stage);
            return unit;
        }
    }
}