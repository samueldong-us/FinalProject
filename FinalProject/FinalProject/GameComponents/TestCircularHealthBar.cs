using FinalProject.Messaging;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class TestCircularHealthBar : HealthBarComponent
    {
        private Texture2D healthBar;

        public TestCircularHealthBar(MessageCenter messageCenter, int width, int height, Vector2 offset, HealthComponent health, TransformComponent transform, List<Drawable> healthBarLayer, Texture2D healthBar)
            : base(messageCenter, width, height, offset, health, transform, healthBarLayer)
        {
            this.healthBar = healthBar;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthNumber = health.Health / (float)health.MaxHealth;
            Rectangle destination = new Rectangle((int)(transform.Position.X - healthBar.Width / 2.0), (int)(transform.Position.Y - healthBar.Height / 2.0), healthBar.Width, healthBar.Height);
            float redComponent = healthNumber > .5 ? MathHelper.Lerp(1, 0, (healthNumber - .5f) * 2) : 1;
            float greenComponent = healthNumber > .5 ? 1 : MathHelper.Lerp(0, 1, healthNumber * 2);
            GraphicsUtilities.BeginDrawingWithCircularWipe(spriteBatch, 1 - healthNumber, .5f);
            spriteBatch.Draw(healthBar, destination, new Color(redComponent, greenComponent, 0));
            GraphicsUtilities.EndDrawingWithCircularWipe(spriteBatch);
        }
    }
}