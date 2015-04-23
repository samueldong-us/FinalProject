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
                Entity centerBullet = CreateProjectile(1000, -(float)(Math.PI * i / 50), GameAssets.BulletTexture, GameAssets.BulletTexture.Bounds, new List<Triangle>(), GameScreen.LayerBullets, GameScreen.CollidersPlayerBullets);
                centerBullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
                projectiles.Add(centerBullet);
            }
        }
    }
}