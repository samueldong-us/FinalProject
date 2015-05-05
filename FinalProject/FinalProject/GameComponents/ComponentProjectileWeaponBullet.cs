using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponBullet : ComponentProjectileWeapon
    {
        private const int Speed = 200;

        private Vector2 closestPosition;

        private int damage;

        private int numberOfBullets;

        public ComponentProjectileWeaponBullet(Entity entity, int numberOfBullets, int damage)
            : base(entity, Vector2.Zero)
        {
            closestPosition = new Vector2(-1, -1);
            entity.MessageCenter.AddListener<Vector2>("Closest Player", SetClosestPosition);
            this.numberOfBullets = numberOfBullets;
            this.damage = damage;
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Closest Player", SetClosestPosition);
            base.Dispose();
        }

        protected override void Fire()
        {
            ScreenGame.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
            Vector2 direction = closestPosition - entity.Position;
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                direction = new Vector2(0, 1);
            }
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X);

            Entity bullet = CreateProjectile(Speed, bulletAngle, GameAssets.BulletTexture, GameAssets.Bullet[3], GameAssets.BulletTriangles[3], new Color(136, 144, 255), "EnemyBullet", "EnemyBullet");
            new ComponentDealDamage(bullet, damage);
            ScreenGame.Entities.AddEntity(bullet);
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}