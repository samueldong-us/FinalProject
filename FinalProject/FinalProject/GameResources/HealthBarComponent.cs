using FinalProject.Messaging;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class HealthBarComponent : Component, Drawable
    {
        private Rectangle backing;
        private Rectangle bar;
        private HealthComponent health;
        private List<Drawable> healthBarLayer;
        private Vector2 offset;
        private TransformComponent transform;

        public HealthBarComponent(MessageCenter messageCenter, int width, int height, Vector2 offset, HealthComponent health, TransformComponent transform, List<Drawable> healthBarLayer)
            : base(messageCenter)
        {
            bar = new Rectangle(0, 0, width, height);
            backing = new Rectangle(0, 0, width + 4, height + 4);
            this.offset = offset;
            this.health = health;
            this.transform = transform;
            this.healthBarLayer = healthBarLayer;
            healthBarLayer.Add(this);
        }

        public override void Dispose()
        {
            healthBarLayer.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float healthNumber = health.Health / (float)health.MaxHealth;
            Vector2 targetOrigin = transform.Position + offset;
            Rectangle backingDestination = new Rectangle((int)(targetOrigin.X - backing.Width / 2.0), (int)(targetOrigin.Y - backing.Height / 2.0), backing.Width, backing.Height);
            Rectangle barDestination = new Rectangle((int)(targetOrigin.X - bar.Width / 2.0), (int)(targetOrigin.Y - bar.Height / 2.0), (int)(bar.Width * healthNumber), bar.Height);
            float redComponent = healthNumber > .5 ? MathHelper.Lerp(1, 0, (healthNumber - .5f) * 2) : 1;
            float greenComponent = healthNumber > .5 ? 1 : MathHelper.Lerp(0, 1, healthNumber * 2);
            spriteBatch.Draw(GraphicsUtilities.PlainTexture, backingDestination, new Color(35, 30, 30));
            spriteBatch.Draw(GraphicsUtilities.PlainTexture, barDestination, new Color(redComponent, greenComponent, 0));
        }

        public override void Update(float secondsPassed)
        {
        }
    }
}