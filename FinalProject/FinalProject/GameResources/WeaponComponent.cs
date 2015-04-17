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

        public WeaponComponent(MessageCenter entityMessageCenter, TransformComponent inputShipInfo, Texture2D inputBulletImage)
            : base(entityMessageCenter)
        {
            bullets = new List<Entity>();
            weaponMessager = new MessageCenter();
            playerShipInfo = inputShipInfo;
            bulletImage = inputBulletImage;
        }

        private Entity makeBullets()
        {
            Entity bullet = new Entity();
            TransformBoundSignaler bulletBounds = new TransformBoundSignaler();
            return bullet;
        }
       
    }
}