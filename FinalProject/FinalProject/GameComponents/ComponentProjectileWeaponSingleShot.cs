using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponSingleShot : ComponentProjectileWeapon
    {
        private Vector2 closestPlayer;
        private int damage;
        private float firerate;
        private const int Speed = 400;


        public ComponentProjectileWeaponSingleShot(Entity entity, int damage, float firerate)
            : base(entity, Vector2.Zero)
        {
            entity.MessageCenter.AddListener<Vector2>("Closest Player", SetClosestPosition);
            this.damage = damage;
            this.firerate = firerate;
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Closest Player", SetClosestPosition);
            base.Dispose();
        }

        protected override void Fire()
        {
            ScreenGame.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
            Vector2 direction = closestPlayer - entity.Position;
            if ( closestPlayer.Equals(new Vector2(-1,-1))) {
                direction = new Vector2(0, 1);
            }
            float angleToPlayer = (float)Math.Atan2(direction.Y, direction.X);
                //Entity bullet = CreateProjectile(Speed, (float)(2 * Math.PI * i / numberOfBullets + rotation), GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "EnemyBullet", "EnemyBullet");
                Entity bullet = CreateProjectile(Speed, angleToPlayer, GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "EnemyBullet", "EnemyBullet");
                bullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
                bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
                projectiles.Add(bullet);
        }

        private void DealDamage(Entity projectile, Entity collidedEntity)
        {
            collidedEntity.MessageCenter.Broadcast<float>("Take Damage", damage);
            RemoveProjectile(projectile);
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPlayer = parameterOne;
        }
    }
}
