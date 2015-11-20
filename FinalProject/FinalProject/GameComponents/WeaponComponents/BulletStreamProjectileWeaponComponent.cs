using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class BulletStreamProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const int Speed = 200;
        private Vector2 closestPosition;
        private int damage;

        public BulletStreamProjectileWeaponComponent(Entity entity, int damage)
            : base(entity, Vector2.Zero)
        {
            closestPosition = new Vector2(-1, -1);
            entity.MessageCenter.AddListener<Vector2>("Closest Player", SetClosestPosition);
            this.damage = damage;
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Closest Player", SetClosestPosition);
            base.Dispose();
        }

        protected override void Fire()
        {
            GameMain.Audio.PlayOneTimeSound("Enemy Sound");
            GameScreen.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
            Vector2 direction = closestPosition - entity.Position;
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                direction = new Vector2(0, 1);
            }
            float randomAngle = (float)(Math.PI / 16 * (GameMain.RNG.NextDouble() - .5));
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X);

            Entity bullet = CreateProjectile(Speed, bulletAngle + randomAngle, GameAssets.BulletTexture, GameAssets.Bullet[6], GameAssets.BulletTriangles[6], new Color(112, 152, 232), "EnemyBullet", "EnemyBullet");
            new DealDamageComponent(bullet, damage);
            GameScreen.Entities.AddEntity(bullet);
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}