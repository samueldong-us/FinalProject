using FinalProject.Screens;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class SpreadShotProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const int Speed = 1000;
        private int damage;
        private float rotation;

        public SpreadShotProjectileWeaponComponent(Entity entity, int damage, float rotation, Vector2 offset)
            : base(entity, offset)
        {
            this.damage = damage;
            this.rotation = rotation;
        }

        protected override void Fire()
        {
            GameMain.Audio.PlayOneTimeSound("Spread Sound");
            Entity bullet = CreateProjectile(Speed, rotation, GameAssets.BulletTexture, GameAssets.Bullet[9], GameAssets.BulletTriangles[9], new Color(101, 232, 136), "PlayerBullet", "PlayerBullet");
            new DealDamageComponent(bullet, damage);
            GameScreen.Entities.AddEntity(bullet);
        }
    }
}