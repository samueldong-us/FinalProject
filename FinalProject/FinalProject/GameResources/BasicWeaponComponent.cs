using FinalProject.Messaging;
using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FinalProject.GameResources
{
    internal class BasicWeaponComponent : Component
    {
        private Texture2D bulletImage;
        private Color[,] bulletImageArray;
        private Vector2 bulletOffset;
        private List<Entity> bullets;
        private Vector2 bulletVelocity;
        private bool shooting;
        private float timeUntilReady;
        private List<Entity> toRemove;
        private TransformComponent transform;
        private float weaponCooldown;

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

        private void CleanUp()
        {
            foreach (Entity bullet in toRemove)
            {
                bullets.Remove(bullet);
                bullet.Dispose();
            }
            toRemove.Clear();
        }

        private void CollidedWithEntity(Entity entity, Entity bullet)
        {
            entity.MessageCenter.Broadcast<int>("Take Damage", 1);
            RemoveBullet(bullet);
        }

        private void FireBullet()
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

        private void RemoveBullet(Entity bullet)
        {
            toRemove.Add(bullet);
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