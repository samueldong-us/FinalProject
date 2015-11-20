using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class SwingProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const float FieldOfFire = (float)(Math.PI / 8);
        private const int Speed = 200;
        private Vector2 closestPosition;
        private int damage;
        private float deltaTheta;
        private float theta;

        public SwingProjectileWeaponComponent(Entity entity, int damage, float deltaTheta)

            : base(entity, Vector2.Zero)
        {
            closestPosition = new Vector2(-1, -1);
            entity.MessageCenter.AddListener<Vector2>("Closest Player", SetClosestPosition);
            this.damage = damage;
            this.deltaTheta = deltaTheta;
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Closest Player", SetClosestPosition);
            base.Dispose();
        }

        protected override void Fire()
        {
            GameMain.Audio.PlayOneTimeSound("Enemy Sound");
            Vector2 direction = closestPosition - entity.Position;
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                direction = new Vector2(0, 1);
            }
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X) + FieldOfFire * (float)Math.Sin(theta);
            Entity bullet = CreateProjectile(Speed, bulletAngle, GameAssets.BulletTexture, GameAssets.Bullet[3], GameAssets.BulletTriangles[3], new Color(136, 144, 255), "EnemyBullet", "EnemyBullet");
            new DealDamageComponent(bullet, damage);
            GameScreen.Entities.AddEntity(bullet);
            theta += deltaTheta;
            if (theta > Math.PI * 2)
            {
                theta -= (float)Math.PI * 2;
            }
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}