using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponSplitShot : ComponentProjectileWeapon
    {
        private const int Speed = 200;

        private Vector2 closestPosition;

        private int damage;

        private int numberOfBullets;

        public ComponentProjectileWeaponSplitShot(Entity entity, int numberOfBullets, int damage)
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
            ScreenGame.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
            Vector2 direction = closestPosition - entity.Position;
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                direction = new Vector2(0, 1);
            }
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X);

            //Middle
            Entity bullet = CreateProjectile(Speed, bulletAngle, GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "EnemyBullet", "EnemyBullet");
            bullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
            bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
            projectiles.Add(bullet);
            //Left
            Entity bullet2 = CreateProjectile(Speed, bulletAngle, GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "EnemyBullet", "EnemyBullet");
            new ComponentDelayedSetVelocity(bullet2, 1f, UtilitiesMath.VectorFromMagnitudeAndAngle(Speed, bulletAngle + ((float)Math.PI / 8)));
            bullet2.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
            bullet2.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
            projectiles.Add(bullet2);
            //Right
            Entity bullet3 = CreateProjectile(Speed, bulletAngle, GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "EnemyBullet", "EnemyBullet");
            new ComponentDelayedSetVelocity(bullet3, 1f, UtilitiesMath.VectorFromMagnitudeAndAngle(Speed, bulletAngle - ((float)Math.PI / 8)));
            bullet3.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
            bullet3.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
            projectiles.Add(bullet3);
        }

        private void DealDamage(Entity projectile, Entity collidedEntity)
        {
            collidedEntity.MessageCenter.Broadcast<float>("Take Damage", damage);
            RemoveProjectile(projectile);
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}