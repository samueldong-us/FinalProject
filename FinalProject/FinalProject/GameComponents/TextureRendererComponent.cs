using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class TextureRendererComponent : Component, Drawable
    {
        private List<Drawable> layer;
        private Rectangle source;
        private Texture2D texture;

        public TextureRendererComponent(Entity entity, Texture2D texture, Rectangle source, List<Drawable> layer)
            : base(entity)
        {
            this.texture = texture;
            this.source = source;
            this.layer = layer;
            layer.Add(this);
        }

        public override void Dispose()
        {
            layer.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, entity.Position, source, Color.White, entity.Rotation, new Vector2(source.Width / 2, source.Height / 2), entity.Scale, SpriteEffects.None, 0);
        }

        public override void Update(float secondsPassed)
        {
        }
    }
}