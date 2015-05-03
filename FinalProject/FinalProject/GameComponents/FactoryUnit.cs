using FinalProject.GameWaves;

namespace FinalProject.GameComponents
{
    internal static class FactoryUnit
    {
        public static Entity CreateEntityFromSpawnInformation(SpawnInformation spawnInformation)
        {
            Entity unit = new Entity();
            return unit;
        }
    }
}