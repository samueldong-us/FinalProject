using FinalProject.Screens;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponHoming : ComponentProjectileWeapon
    {
        private const int Speed = 500;

        private int damage;

        private float rotation;

        public ComponentProjectileWeaponHoming(Entity entity, int damage, float rotation, Vector2 offset)
            : base(entity, offset)
        {
            this.damage = damage;
            this.rotation = rotation;
        }

        protected override void Fire()
        {
            Entity bullet = CreateProjectile(Speed, rotation, GameAssets.BulletTexture, GameAssets.Bullet[1], GameAssets.BulletTriangles[1], new Color(127, 255, 124), "PlayerBullet", "PlayerBullet");
            bullet.Scale = .75f;
            new ComponentDealDamage(bullet, damage);
            new ComponentHomingToEnemy(bullet);
            ScreenGame.Entities.AddEntity(bullet);
        }
    }
}