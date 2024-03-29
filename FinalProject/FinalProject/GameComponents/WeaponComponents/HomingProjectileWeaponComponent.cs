﻿using FinalProject.Screens;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class HomingProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const int Speed = 500;
        private int damage;
        private float rotation;

        public HomingProjectileWeaponComponent(Entity entity, int damage, float rotation, Vector2 offset)
            : base(entity, offset)
        {
            this.damage = damage;
            this.rotation = rotation;
        }

        protected override void Fire()
        {
            GameMain.Audio.PlayOneTimeSound("Homing Sound");
            Entity bullet = CreateProjectile(Speed, rotation, GameAssets.BulletTexture, GameAssets.Bullet[1], GameAssets.BulletTriangles[1], new Color(127, 255, 124), "PlayerBullet", "PlayerBullet");
            bullet.Scale = .75f;
            new DealDamageComponent(bullet, damage);
            new HomeToEnemyComponent(bullet);
            GameScreen.Entities.AddEntity(bullet);
        }
    }
}