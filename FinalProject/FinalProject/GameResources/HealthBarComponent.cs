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

        public HealthBarComponent(MessageCenter messageCenter, int width, int height, Vector2 offset, HealthComponent health, TransformComponent transform)
            : base(messageCenter)
        {
            bar = new Rectangle(0, 0, width, height);
            backing = new Rectangle(0, 0, width + 4, height + 4);
            this.offset = offset;
            this.health = health;
            this.transform = transform;
        }

        public override void Dispose()
        {
            healthBarLayer.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 targetOrigin = transform.Position + offset;
            Rectangle backingDestination = new Rectangle((int)(targetOrigin.X - backing.Width / 2.0), (int)(targetOrigin.Y - backing.Height / 2.0), backing.Width, backing.Height);
            Rectangle barDestination = new Rectangle((int)(targetOrigin.X - bar.Width / 2.0), (int)(targetOrigin.Y - bar.Height / 2.0), bar.Width, bar.Height);
            float healthNumber = health.Health / (float)health.MaxHealth;
            Vector3 healthColor = healthNumber > .5 ? Vector3.Lerp(new Vector3(255, 255, 0), new Vector3(0, 255, 0), (healthNumber - .5f) * 2) : Vector3.Lerp(new Vector3(255, 0, 0), new Vector3(255, 255, 0), healthNumber * 2);
            spriteBatch.Draw(GraphicsUtilities.PlainTexture, backingDestination, new Color(40, 30, 30));
            spriteBatch.Draw(GraphicsUtilities.PlainTexture, barDestination, new Color(healthColor));
        }

        public override void Update(float secondsPassed)
        {
        }
    }
}