using FinalProject.Messaging;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FinalProject.GameResources
{
    internal class BasicWeaponComponent : Component
    {
        private Texture2D bulletImage;
        private Vector2 bulletOffset;
        private List<Entity> bullets;
        private Vector2 bulletVelocity;
        private bool shooting;
        private float timeUntilReady;
        private TransformComponent transform;
        private float weaponCooldown;
        private MessageCenter weaponMessenger;

        public BasicWeaponComponent(MessageCenter messageCenter, TransformComponent transform, Texture2D bulletImage, float weaponCooldown, Vector2 bulletVelocity, Vector2 bulletOffset)
            : base(messageCenter)
        {
            bullets = new List<Entity>();
            weaponMessenger = new MessageCenter();
            this.transform = transform;
            this.bulletImage = bulletImage;
            messageCenter.AddListener("Start Shooting", StartShooting);
            messageCenter.AddListener("Stop Shooting", StopShooting);
            weaponMessenger.AddListener<Entity>("Exited", RemoveBullet);
            this.weaponCooldown = weaponCooldown;
            this.bulletVelocity = bulletVelocity;
            this.bulletOffset = bulletOffset;
        }

        public override void Dispose()
        {
        }

        public override void Update(float secondsPassed)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update(secondsPassed);
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

        private void FireBullet()
        {
            Entity bullet = new Entity();
            ExitSignalingTransformComponent bulletTransform = new ExitSignalingTransformComponent(bullet, weaponMessenger, bulletImage.Bounds, GameScreen.Bounds);
            bulletTransform.Position = transform.Position + bulletOffset;
            bulletTransform.SetVelocity(bulletVelocity);
            RenderComponent bulletRender = new RenderComponent(bullet.MessageCenter, bulletImage, bulletTransform, GameScreen.BulletLayer);
            bullet.AddComponent(bulletTransform);
            bullet.AddComponent(bulletRender);
            bullets.Add(bullet);
        }

        private void RemoveBullet(Entity bullet)
        {
            bullets.Remove(bullet);
            bullet.Dispose();
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