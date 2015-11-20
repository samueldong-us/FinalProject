using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class CircularBounceProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const int Speed = 200;
        private int damage;
        private int numberOfBullets;
        private float rotation;
        private float rotationalDelta;

        public CircularBounceProjectileWeaponComponent(Entity entity, int numberOfBullets, int damage, float rotationalDelta)
            : base(entity, Vector2.Zero)
        {
            this.numberOfBullets = numberOfBullets;
            this.damage = damage;
            this.rotationalDelta = rotationalDelta;
            rotation = (float)(GameMain.RNG.NextDouble() * 2 * Math.PI);
        }

        protected override void Fire()
        {
            GameMain.Audio.PlayOneTimeSound("Enemy Sound");
            for (int i = 0; i < numberOfBullets; i++)
            {
                Entity bullet = CreateProjectile(Speed, (float)(2 * Math.PI * i / numberOfBullets + rotation), GameAssets.BulletTexture, GameAssets.Bullet[8], GameAssets.BulletTriangles[8], new Color(137, 112, 232), "EnemyBullet", "EnemyBullet");
                new BounceComponent(bullet);
                new DealDamageComponent(bullet, damage);
                GameScreen.Entities.AddEntity(bullet);
            }
            rotation += rotationalDelta;
        }
    }
}