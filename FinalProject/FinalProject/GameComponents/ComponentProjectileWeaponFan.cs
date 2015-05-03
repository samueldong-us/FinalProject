using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;


namespace FinalProject.GameComponents
{
    class ComponentProjectileWeaponFan : ComponentProjectileWeapon
    {
        private const int Speed = 200;

        private int damage;

        private int numberOfBullets;

        private Vector2 closestPosition;

        public ComponentProjectileWeaponFan(Entity entity, int numberOfBullets, int damage)
            : base(entity, Vector2.Zero)
        {
            closestPosition = new Vector2(-1, -1);
            entity.MessageCenter.AddListener<Vector2>("Closest Player", SetClosestPosition);
            this.numberOfBullets = numberOfBullets;
            this.damage = damage;
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
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
            if (closestPosition.Equals( new Vector2(-1,-1))){
                direction = new Vector2(0, 1);
            }
            float bulletAngle = (float)Math.Atan2(direction.Y, direction.X);
            for (int i = -numberOfBullets/2; i <= numberOfBullets/2; i++)
            {
                Entity bullet = CreateProjectile(Speed, bulletAngle+((float)(Math.PI /5*i/numberOfBullets)), GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "EnemyBullet", "EnemyBullet");
                bullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
                bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
                projectiles.Add(bullet);
            }
        }

        private void DealDamage(Entity projectile, Entity collidedEntity)
        {
            collidedEntity.MessageCenter.Broadcast<float>("Take Damage", damage);
            RemoveProjectile(projectile);
        }
    }
}