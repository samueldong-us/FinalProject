using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.GameComponents
{
    internal class ComponentHealthBarCircular : ComponentHealthBar
    {
        private float rotation;

        public ComponentHealthBarCircular(Entity entity, float rotation)
            : base(entity, Rectangle.Empty, Vector2.Zero)
        {
            this.rotation = rotation;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            UtilitiesGraphics.BeginDrawingWithCircularWipe(spriteBatch, 1 - health / (float)maxHealth);
            spriteBatch.Draw(GameAssets.CircularHealthBar1, entity.Position, null, Color.White, rotation, new Vector2(GameAssets.CircularHealthBar1.Width / 2.0f, GameAssets.CircularHealthBar1.Height / 2.0f), 1, SpriteEffects.None, 0);
            UtilitiesGraphics.EndDrawingWithCircularWipe(spriteBatch);
        }

        public override void Update(float secondsPassed)
        {
            base.Update(secondsPassed);
        }
    }
}