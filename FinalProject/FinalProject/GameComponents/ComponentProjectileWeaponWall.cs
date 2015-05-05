using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponWall : ComponentProjectileWeapon
    {
        private const int Speed = 200;

        private int damage;

        private int numberOfBullets;

        private float rotation;

        private float rotationalDelta;

        public ComponentProjectileWeaponWall(Entity entity, int numberOfBullets, int damage, float rotationalDelta)
            : base(entity, Vector2.Zero)
        {
            this.numberOfBullets = numberOfBullets;
            this.damage = damage;
            this.rotationalDelta = rotationalDelta;
            rotation = (float)(GameMain.RNG.NextDouble() * 2 * Math.PI);
            entity.MessageCenter.AddListener<float>("Rotate", Rotate);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<float>("Rotate", Rotate);
            base.Dispose();
        }

        protected override void Fire()
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                Entity bullet = CreateProjectile(Speed, (float)(2 * Math.PI * i / numberOfBullets + rotation), GameAssets.BulletTexture, GameAssets.Bullet[16], GameAssets.BulletTriangles[16], new Color(186, 124, 255), "EnemyBullet", "EnemyBullet");
                new ComponentDealDamage(bullet, damage);
                ScreenGame.Entities.AddEntity(bullet);
            }
            rotation += rotationalDelta;
            if (rotation > Math.PI * 2)
            {
                rotation -= (float)(Math.PI * 2);
            }
        }

        private void Rotate(float parameterOne)
        {
            rotation += parameterOne;
        }
    }
}