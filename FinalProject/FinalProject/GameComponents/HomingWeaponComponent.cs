using FinalProject.Messaging;
using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class HomingWeaponComponent : BasicWeaponComponent
    {
        private float maxAngularSpeed;

        public HomingWeaponComponent(MessageCenter messageCenter, TransformComponent transform, Texture2D bulletImage, float weaponCooldown, Vector2 bulletVelocity, Vector2 bulletOffset, float maxAngularSpeed)
            : base(messageCenter, transform, bulletImage, weaponCooldown, bulletVelocity, bulletOffset)
        {
            this.maxAngularSpeed = maxAngularSpeed;
        }

        protected override void CollidedWithEntity(Entity entity, Entity bullet)
        {
            entity.MessageCenter.Broadcast<int>("Take Damage", 1);
            RemoveBullet(bullet);
        }

        protected override void FireBullet()
        {
            Entity bullet = new Entity();
            bullet.MessageCenter.AddListener<Entity>("Exited", RemoveBullet);
            bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", CollidedWithEntity);
            ExitSignalingTransformComponent bulletTransform = new ExitSignalingTransformComponent(bullet, bulletImage.Bounds, GameScreen.Bounds);
            bulletTransform.Position = transform.Position + bulletOffset;
            bulletTransform.SetVelocity(bulletVelocity);
            bulletTransform.Theta = -90;
            EnemyHomingComponent bulletEnemyHoming = new EnemyHomingComponent(bullet.MessageCenter, maxAngularSpeed, bulletVelocity, bulletTransform);
            ColliderComponent bulletCollider = new ColliderComponent(bullet, 15, 50, 50, new List<Triangle>() { new Triangle(new Vector2(10, 10), new Vector2(10, 40), new Vector2(45, 25)) }, bulletTransform, GameScreen.CollidersPlayerBullets);
            RenderComponent bulletRender = new RenderComponent(bullet.MessageCenter, bulletImage, bulletTransform, bulletCollider, GameScreen.LayerBullets);
            bullet.AddComponent(bulletEnemyHoming);
            bullet.AddComponent(bulletCollider);
            bullet.AddComponent(bulletTransform);
            bullet.AddComponent(bulletRender);
            bullets.Add(bullet);
        }
    }
}