using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class ComponentTextureRenderer : DrawableComponent
    {
        private Rectangle source;

        private Texture2D texture;

        private Color tint;

        public ComponentTextureRenderer(Entity entity, Texture2D texture, Rectangle source, Color tint, string drawableLayer)
            : base(entity, drawableLayer)
        {
            this.texture = texture;
            this.source = source;
            this.tint = tint;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, entity.Position, source, tint, entity.Rotation, new Vector2(source.Width / 2, source.Height / 2), entity.Scale, SpriteEffects.None, 0);
        }

        public override void Update(float secondsPassed)
        {
        }
    }
}