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
        public ExampleProjectileWeaponComponent(Entity entity, Vector2 offset)
            : base(entity, offset)
        {
        }

        protected override void Fire()
        {
            for (int i = 0; i < 100; i++)
            {
                Entity centerBullet = CreateProjectile(1000, -(float)(Math.PI * i / 50), GameAssets.BulletTexture, GameAssets.BulletTexture.Bounds, new List<Triangle>() { new Triangle(new Vector2(10, 10), new Vector2(10, 40), new Vector2(45, 25)) }, GameScreen.LayerBullets, GameScreen.CollidersPlayerBullets);
                centerBullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
                centerBullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
                projectiles.Add(centerBullet);
            }
        }

        private void DealDamage(Entity projectile, Entity entity)
        {
            entity.MessageCenter.Broadcast<int>("Take Damage", 1);
            RemoveProjectile(projectile);
        }
    }
}