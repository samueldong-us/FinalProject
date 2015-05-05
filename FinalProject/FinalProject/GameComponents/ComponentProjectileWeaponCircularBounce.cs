﻿using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponCircularBounce : ComponentProjectileWeapon
    {
        private const int Speed = 200;

        private int damage;

        private int numberOfBullets;

        private float rotation;

        private float rotationalDelta;

        public ComponentProjectileWeaponCircularBounce(Entity entity, int numberOfBullets, int damage, float rotationalDelta)
            : base(entity, Vector2.Zero)
        {
            this.numberOfBullets = numberOfBullets;
            this.damage = damage;
            this.rotationalDelta = rotationalDelta;
            rotation = 0;
        }

        protected override void Fire()
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                Entity bullet = CreateProjectile(Speed, (float)(2 * Math.PI * i / numberOfBullets + rotation), GameAssets.BulletTexture, GameAssets.Bullet[8], GameAssets.BulletTriangles[8], Color.Red, "EnemyBullet", "EnemyBullet");
                new ComponentBounce(bullet);
                new ComponentDealDamage(bullet, damage);
                ScreenGame.Entities.AddEntity(bullet);
            }
            rotation += rotationalDelta;
        }
    }
}