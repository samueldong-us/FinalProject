using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponHoming : ComponentProjectileWeapon
    {
        private const int Speed = 500;

        private int damage;

        private float rotation;

        public ComponentProjectileWeaponHoming(Entity entity, int damage, float rotation, Vector2 offset)
            : base(entity, offset)
        {
            this.damage = damage;
            this.rotation = rotation;
        }

        protected override void Fire()
        {
            Entity bullet = CreateProjectile(Speed, rotation, GameAssets.BulletTexture, GameAssets.Bullet[1], GameAssets.BulletTriangles[1], Color.Red, "PlayerBullet", "PlayerBullet");
            bullet.Scale = .75f;
            new ComponentDealDamage(bullet, damage);
            new ComponentHomingToEnemy(bullet);
            ScreenGame.Entities.AddEntity(bullet);
        }
    }
}