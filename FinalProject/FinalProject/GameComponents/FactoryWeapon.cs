namespace FinalProject.GameComponents
{
    internal static class FactoryWeapon
    {
        public static void AddCircularFireWeapon(Entity entity, float fireRate, int numberOfBullets, int damage, float rotationalDelta)
        {
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponCircularFire(entity, numberOfBullets, damage, rotationalDelta);
        }

        public static void AddFanWeapon(Entity entity, float fireRate, int numberOfBullets, int damage)
        {
            new ComponentConstantRateFire(entity, fireRate);
            new ComponentProjectileWeaponFan(entity, numberOfBullets, damage);
        }
    }
}