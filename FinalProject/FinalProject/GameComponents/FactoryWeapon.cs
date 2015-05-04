using FinalProject.GameSaving;
using FinalProject.GameWaves;

namespace FinalProject.GameComponents
{
    internal static class FactoryWeapon
    {
        public static void AddWeapon(Entity entity, SpawnInformation spawnInformation, SaveGame.Difficulty difficulty, int stage)
        {
            switch (spawnInformation.GetInformation<string>("Weapon Name"))
            {
                case "Circular Fire":
                    {
                        AddCircularFireWeapon(entity, difficulty, stage);
                    } break;
                case "Fan":
                    {
                        AddFanWeapon(entity, difficulty, stage);
                    } break;
            }
        }

        private static void AddCircularFireWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Circular Fire"]["Fire Rate"];
            int numberOfBullets = (int)Values.WeaponValues[difficulty][stage]["Circular Fire"]["Number Of Bullets"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Circular Fire"]["Damage"];
            float rotationalDelta = (float)Values.WeaponValues[difficulty][stage]["Circular Fire"]["Rotational Delta"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponCircularFire(entity, numberOfBullets, damage, rotationalDelta);
        }

        private static void AddFanWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Fan"]["Fire Rate"];
            int numberOfBullets = (int)Values.WeaponValues[difficulty][stage]["Fan"]["Number Of Bullets"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Fan"]["Damage"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponFan(entity, numberOfBullets, damage);
        }
    }
}