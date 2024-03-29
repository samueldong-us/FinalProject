﻿using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.GameComponents
{
    internal class HealthBarComponent : DrawableComponent
    {
        protected float health;
        protected float maxHealth;
        private Rectangle backing;
        private Rectangle bar;

        public HealthBarComponent(Entity entity, Rectangle size, Vector2 offset)
            : base(entity, "HealthBar")
        {
            int x = (int)(offset.X - size.Width / 2.0);
            int y = (int)(offset.Y - size.Height / 2.0);
            bar = new Rectangle(x, y, size.Width, size.Height);
            backing = new Rectangle(x - 2, y - 2, size.Width + 4, size.Height + 4);
            entity.MessageCenter.AddListener<float, float>("Health", UpdateHealth);
            health = 1;
            maxHealth = 1;
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<float, float>("Health", UpdateHealth);
            base.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthAmount = health / maxHealth;
            Vector3 healthColor = Vector3.Zero;
            if (healthAmount > .5f)
            {
                healthColor = Vector3.Lerp(new Vector3(1, 1, 0), new Vector3(0, 1, 0), (healthAmount - .5f) * 2);
            }
            else
            {
                healthColor = Vector3.Lerp(new Vector3(1, 0, 0), new Vector3(1, 1, 0), healthAmount * 2);
            }
            spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle(backing.X + (int)entity.Position.X, backing.Y + (int)entity.Position.Y, backing.Width, backing.Height), Color.DarkSlateGray);
            spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle(bar.X + (int)entity.Position.X, bar.Y + (int)entity.Position.Y, (int)(bar.Width * healthAmount), bar.Height), new Color(healthColor));
        }

        public override void Update(float secondsPassed)
        {
            entity.MessageCenter.Broadcast("Get Health");
        }

        protected void UpdateHealth(float parameterOne, float parameterTwo)
        {
            health = parameterOne;
            maxHealth = parameterTwo;
        }
    }
}