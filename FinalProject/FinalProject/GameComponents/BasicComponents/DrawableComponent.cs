using FinalProject.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.GameComponents
{
    internal abstract class DrawableComponent : Component
    {
        private string drawableLayer;

        public DrawableComponent(Entity entity, string drawableLayer)
            : base(entity)
        {
            GameScreen.Drawing.AddDrawable(drawableLayer, this);
            this.drawableLayer = drawableLayer;
        }

        public override void Dispose()
        {
            GameScreen.Drawing.RemoveDrawable(drawableLayer, this);
            base.Dispose();
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}