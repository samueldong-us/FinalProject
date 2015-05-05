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
                case "Bullet":
                    {
                        AddBulletWeapon(entity, difficulty, stage);
                    } break;
                case "Bullet Stream":
                    {
                        AddBulletStreamWeapon(entity, difficulty, stage);
                    } break;
                case "Circular Bounce":
                    {
                        AddCircularBounceWeapon(entity, difficulty, stage);
                    } break;
                case "Fan Stream":
                    {
                        AddFanStreamWeapon(entity, difficulty, stage);
                    } break;
                case "Split Shot":
                    {
                        AddSplitShotWeapon(entity, difficulty, stage);
                    } break;
                case "Spiral":
                    {
                        AddSpiralFireWeapon(entity, difficulty, stage);
                    } break;
            }
        }

        private static void AddBulletStreamWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Bullet Stream"]["Fire Rate"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Bullet Stream"]["Damage"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponBulletStream(entity, damage);
        }

        private static void AddBulletWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Bullet"]["Fire Rate"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Bullet"]["Damage"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponBullet(entity, damage);
        }

        private static void AddCircularBounceWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Circular Bounce"]["Fire Rate"];
            int numberOfBullets = (int)Values.WeaponValues[difficulty][stage]["Circular Bounce"]["Number Of Bullets"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Circular Bounce"]["Damage"];
            float rotationalDelta = (float)Values.WeaponValues[difficulty][stage]["Circular Bounce"]["Rotational Delta"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponCircularBounce(entity, numberOfBullets, damage, rotationalDelta);
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

        private static void AddFanStreamWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Fan Stream"]["Fire Rate"];
            int numberOfBullets = (int)Values.WeaponValues[difficulty][stage]["Fan Stream"]["Number Of Bullets"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Fan Stream"]["Damage"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponFanStream(entity, numberOfBullets, damage);
        }

        private static void AddFanWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Fan"]["Fire Rate"];
            int numberOfBullets = (int)Values.WeaponValues[difficulty][stage]["Fan"]["Number Of Bullets"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Fan"]["Damage"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponFan(entity, numberOfBullets, damage);
        }

        private static void AddSpiralFireWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Spiral"]["Fire Rate"];
            int numberOfBullets = (int)Values.WeaponValues[difficulty][stage]["Spiral"]["Number Of Bullets"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Spiral"]["Damage"];
            float rotationalDelta = (float)Values.WeaponValues[difficulty][stage]["Spiral"]["Rotational Delta"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponCircularFire(entity, numberOfBullets, damage, rotationalDelta);
        }

        private static void AddSplitShotWeapon(Entity entity, SaveGame.Difficulty difficulty, int stage)
        {
            float fireRate = (float)Values.WeaponValues[difficulty][stage]["Split Shot"]["Fire Rate"];
            int numberOfBullets = (int)Values.WeaponValues[difficulty][stage]["Split Shot"]["Number Of Bullets"];
            int damage = (int)Values.WeaponValues[difficulty][stage]["Split Shot"]["Damage"];
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponSplitShot(entity, numberOfBullets, damage);
        }
    }
}