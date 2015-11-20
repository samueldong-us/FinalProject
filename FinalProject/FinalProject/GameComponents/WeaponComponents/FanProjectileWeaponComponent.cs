using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class FanProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const int Speed = 200;
        private Vector2 closestPosition;
        private int damage;
        private double FieldOfFire = Math.PI / 5;
        private int numberOfBullets;

        public FanProjectileWeaponComponent(Entity entity, int numberOfBullets, int damage)
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
            GameScreen.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
            Vector2 direction = closestPosition - entity.Position;
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                direction = new Vector2(0, 1);
            }
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X);
            for (int i = -numberOfBullets / 2; i <= numberOfBullets / 2; i++)
            {
                Entity bullet = CreateProjectile(Speed, bulletAngle + ((float)(FieldOfFire * i / numberOfBullets)), GameAssets.BulletTexture, GameAssets.Bullet[10], GameAssets.BulletTriangles[10], new Color(102, 218, 232), "EnemyBullet", "EnemyBullet");
                new DealDamageComponent(bullet, damage);
                GameScreen.Entities.AddEntity(bullet);
            }
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}