﻿using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponCircularFire : ComponentProjectileWeapon
    {
        private const int Speed = 200;

        private int damage;

        private int numberOfBullets;

        private float rotation;

        private float rotationalDelta;

        public ComponentProjectileWeaponCircularFire(Entity entity, int numberOfBullets, int damage, float rotationalDelta)
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
                Entity bullet = CreateProjectile(Speed, (float)(2 * Math.PI * i / numberOfBullets + rotation), GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "EnemyBullet", "EnemyBullet");
                bullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
                bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
                projectiles.Add(bullet);
            }
            rotation += rotationalDelta;
        }

        private void DealDamage(Entity projectile, Entity collidedEntity)
        {
            collidedEntity.MessageCenter.Broadcast<float>("Take Damage", damage);
            RemoveProjectile(projectile);
        }
    }
}