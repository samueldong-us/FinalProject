using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class CircularFireProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private const int Speed = 200;
        private int damage;
        private int numberOfBullets;
        private float rotation;
        private float rotationalDelta;

        public CircularFireProjectileWeaponComponent(Entity entity, int numberOfBullets, int damage, float rotationalDelta)
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
                Entity bullet = CreateProjectile(Speed, (float)(2 * Math.PI * i / numberOfBullets + rotation), GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, GameScreen.LayerEnemyBullets, GameScreen.CollidersEnemyBullets);
                bullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
                bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
                projectiles.Add(bullet);
            }
            rotation += rotationalDelta;
        }

        private void DealDamage(Entity projectile, Entity collidedEntity)
        {
            collidedEntity.MessageCenter.Broadcast<int>("Take Damage", damage);
            RemoveProjectile(projectile);
        }
    }
}