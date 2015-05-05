using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponExample : ComponentProjectileWeapon
    {
        private Vector2 closestPosition;

        public ComponentProjectileWeaponExample(Entity entity, Vector2 offset)
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
            for (int i = 0; i < 6; i++)
            {
                Entity centerBullet = CreateProjectile(100, (float)(Math.PI * i / 3), GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "PlayerBullet", "EnemyBullet");
                new ComponentDealDamage(centerBullet, 1);
                new ComponentDelayedTargeting(centerBullet, .5f, .1f * i, 300);
                centerBullet.MoveComponent(5, 1);
                ScreenGame.Entities.AddEntity(centerBullet);
            }
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
    }
}