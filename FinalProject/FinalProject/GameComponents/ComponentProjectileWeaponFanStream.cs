using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponFanStream : ComponentProjectileWeapon
    {
        private const int Speed = 100;
        private Vector2 closestPosition;
        private int damage;
        private double FieldOfFire = Math.PI / 5;
        private int numberOfBullets;

        public ComponentProjectileWeaponFanStream(Entity entity, int numberOfBullets, int damage)
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
            GameMain.Audio.PlayOneTimeSound("Enemy Sound");
            ScreenGame.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
            Vector2 direction = closestPosition - entity.Position;
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                direction = new Vector2(0, 1);
            }
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X);
            for (int j = 0; j < 10; j++)
            {
                for (int i = -numberOfBullets / 2; i <= numberOfBullets / 2; i++)
                {
                    float modifiedBulletAngle = bulletAngle + ((float)(FieldOfFire * i / numberOfBullets));
                    Entity bullet = CreateProjectile(Speed - j * 2, modifiedBulletAngle, GameAssets.BulletTexture, GameAssets.Bullet[15], GameAssets.BulletTriangles[15], new Color(112, 255, 231), "EnemyBullet", "EnemyBullet");
                    new ComponentDelayedSetVelocity(bullet, 1, UtilitiesMath.VectorFromMagnitudeAndAngle(Speed * (15 - j), modifiedBulletAngle));
                    new ComponentDealDamage(bullet, damage);
                    ScreenGame.Entities.AddEntity(bullet);
                }
            }
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}