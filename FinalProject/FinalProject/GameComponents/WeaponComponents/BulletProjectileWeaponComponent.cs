using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class BulletProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const int Speed = 200;
        private Vector2 closestPosition;
        private int damage;

        public BulletProjectileWeaponComponent(Entity entity, int damage)
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
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X);

            Entity bullet = CreateProjectile(Speed, bulletAngle, GameAssets.BulletTexture, GameAssets.Bullet[3], GameAssets.BulletTriangles[3], new Color(136, 144, 255), "EnemyBullet", "EnemyBullet");
            new DealDamageComponent(bullet, damage);
            GameScreen.Entities.AddEntity(bullet);
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}