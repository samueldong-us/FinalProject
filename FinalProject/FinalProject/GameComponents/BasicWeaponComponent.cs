using FinalProject.Messaging;
using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class BasicWeaponComponent : Component
    {
        protected Texture2D bulletImage;
        protected Color[,] bulletImageArray;
        protected Vector2 bulletOffset;
        protected List<Entity> bullets;
        protected Vector2 bulletVelocity;
        protected bool shooting;
        protected float timeUntilReady;
        protected List<Entity> toRemove;
        protected TransformComponent transform;
        protected float weaponCooldown;

        public BasicWeaponComponent(MessageCenter messageCenter, TransformComponent transform, Texture2D bulletImage, float weaponCooldown, Vector2 bulletVelocity, Vector2 bulletOffset)
            : base(messageCenter)
        {
            bullets = new List<Entity>();
            toRemove = new List<Entity>();
            this.transform = transform;
            this.bulletImage = bulletImage;
            messageCenter.AddListener("Start Shooting", StartShooting);
            messageCenter.AddListener("Stop Shooting", StopShooting);
            messageCenter.AddListener("Clean Up", CleanUp);
            this.weaponCooldown = weaponCooldown;
            this.bulletVelocity = bulletVelocity;
            this.bulletOffset = bulletOffset;
            bulletImageArray = GraphicsUtilities.GetColorsFromTexture(bulletImage);
        }

        public override void Dispose()
        {
            messageCenter.RemoveListener("Start Shooting", StartShooting);
            messageCenter.RemoveListener("Stop Shooting", StopShooting);
            messageCenter.RemoveListener("Clean Up", CleanUp);
        }

        public override void Update(float secondsPassed)
        {
            foreach (Entity bullet in bullets)
            {
                bullet.Update(secondsPassed);
            }
            if (shooting && timeUntilReady == 0)
            {
                FireBullet();
                timeUntilReady = weaponCooldown;
            }
            if (timeUntilReady > 0)
            {
                timeUntilReady = Math.Max(timeUntilReady - secondsPassed, 0);
            }
        }

        protected virtual void CollidedWithEntity(Entity entity, Entity bullet)
        {
            entity.MessageCenter.Broadcast<int>("Take Damage", 1);
            RemoveBullet(bullet);
        }

        protected virtual void FireBullet()
        {
            Entity bullet = new Entity();
            bullet.MessageCenter.AddListener<Entity>("Exited", RemoveBullet);
            bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", CollidedWithEntity);
            ExitSignalingTransformComponent bulletTransform = new ExitSignalingTransformComponent(bullet, bulletImage.Bounds, GameScreen.Bounds);
            bulletTransform.Position = transform.Position + bulletOffset;
            bulletTransform.SetVelocity(bulletVelocity);
            ColliderComponent bulletCollider = new ColliderComponent(bullet, 15, bulletImageArray, bulletTransform, GameScreen.PlayerBulletColliders);
            RenderComponent bulletRender = new RenderComponent(bullet.MessageCenter, bulletImage, bulletTransform, GameScreen.BulletLayer);
            bullet.AddComponent(bulletCollider);
            bullet.AddComponent(bulletTransform);
            bullet.AddComponent(bulletRender);
            bullets.Add(bullet);
        }

        protected void RemoveBullet(Entity bullet)
        {
            toRemove.Add(bullet);
        }

        private void CleanUp()
        {
            foreach (Entity bullet in toRemove)
            {
                bullets.Remove(bullet);
                bullet.Dispose();
            }
            toRemove.Clear();
        }

        private void StartShooting()
        {
            shooting = true;
        }

        private void StopShooting()
        {
            shooting = false;
        }
    }
}