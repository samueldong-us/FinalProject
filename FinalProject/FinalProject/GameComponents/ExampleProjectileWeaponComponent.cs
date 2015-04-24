using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ExampleProjectileWeaponComponent : ProjectileWeaponComponent
    {
        private Vector2 closestPosition;

        public ExampleProjectileWeaponComponent(Entity entity, Vector2 offset)
            : base(entity, offset)
        {
            closestPosition = new Vector2(-1, -1);
            entity.MessageCenter.AddListener<Vector2>("Closest Player", SetClosestPosition);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Closest Player", SetClosestPosition);
            base.Dispose();
        }

        protected override void Fire()
        {
            GameScreen.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
            Vector2 fromTo = closestPosition - entity.Position;
            float angle = (float)Math.Atan2(fromTo.Y, fromTo.X);
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                angle = (float)(Math.PI / 2);
            }
            Entity centerBullet = CreateProjectile(100, angle, GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, GameScreen.LayerBullets, GameScreen.CollidersEnemyBullets);
            centerBullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
            centerBullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
            projectiles.Add(centerBullet);
        }

        private void DealDamage(Entity projectile, Entity entity)
        {
            entity.MessageCenter.Broadcast<int>("Take Damage", 1);
            RemoveProjectile(projectile);
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}