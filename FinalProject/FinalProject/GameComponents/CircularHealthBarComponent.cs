using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class CircularHealthBarComponent : HealthBarComponent
    {
        public CircularHealthBarComponent(Entity entity)
            : base(entity, Rectangle.Empty, Vector2.Zero)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GraphicsUtilities.BeginDrawingWithCircularWipe(spriteBatch, 1 - health / (float)maxHealth, .625f);
            spriteBatch.Draw(GameAssets.CircularHealthBar1, entity.Position, null, Color.White, 0, new Vector2(GameAssets.CircularHealthBar1.Width / 2.0f, GameAssets.CircularHealthBar1.Height / 2.0f), 1, SpriteEffects.None, 0);
            GraphicsUtilities.EndDrawingWithCircularWipe(spriteBatch);
        }

        public override void Update(float secondsPassed)
        {
            base.Update(secondsPassed);
        }
    }
}