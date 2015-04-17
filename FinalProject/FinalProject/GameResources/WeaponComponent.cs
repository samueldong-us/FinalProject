using FinalProject.Messaging;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class WeaponComponent : Component
    {
        private List<Entity> bullets;
        private MessageCenter weaponMessager;
        private TransformComponent playerShipInfo;
        private Texture2D bulletImage;
        private float bulletCooldown = .25f;
        private float currentTime;
        private Boolean isShooting;

        public WeaponComponent(MessageCenter entityMessageCenter, TransformComponent inputShipInfo, Texture2D inputBulletImage)
            : base(entityMessageCenter)
        {
            bullets = new List<Entity>();
            weaponMessager = new MessageCenter();
            playerShipInfo = inputShipInfo;
            bulletImage = inputBulletImage;
            entityMessageCenter.AddListener("start shooting", startShooting);
            entityMessageCenter.AddListener("stop shooting", stopShooting);
            weaponMessager.AddListener<Entity>("out of bounds", removeBullet);
        }

        private Entity makeBullets()
        {
            Entity bullet = new Entity();
            TransformBoundSignaler bulletBounds = new TransformBoundSignaler(bullet.messageCenter, weaponMessager, 0,1920,200,1080, bullet);
            bulletBounds.Position = playerShipInfo.Position + new Vector2(0, -100);
            bulletBounds.SetVelocity(new Vector2(0, -100));
            DrawingComponent drawBullet = new DrawingComponent(bullet.messageCenter, bulletImage, bulletBounds, GameScreen.BulletLayer);
            bullet.AddComponent(bulletBounds);
            bullet.AddComponent(drawBullet);
            return bullet;
        }

        private void startShooting()
        {
            isShooting = true;
        }

        private void stopShooting()
        {
            isShooting = false;
        }

        private void removeBullet(Entity strayBullet)
        {
            bullets.Remove(strayBullet);
            strayBullet.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                       bullets[i].Update(secondsPassed);
            }
                if (currentTime < 0)
                {
                    bullets.Add(makeBullets());
                    if (isShooting)
                    {
                        currentTime = bulletCooldown;
                    }
                }
            if (currentTime >= 0)
            {
                currentTime -= secondsPassed;
            }
            if (!isShooting)
            {
                if (currentTime < 0)
                {
                    currentTime = 0;
                }
            }
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
       
    }
}