using FinalProject.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class RenderComponent : Component, Drawable
    {
        private List<Drawable> drawingLayer;
        private Texture2D texture;
        private TransformComponent transform;

        public RenderComponent(MessageCenter messageCenter, Texture2D texture, TransformComponent transform, List<Drawable> drawingLayer)
            : base(messageCenter)
        {
            this.texture = texture;
            this.transform = transform;
            this.drawingLayer = drawingLayer;
            drawingLayer.Add(this);
        }

        public override void Dispose()
        {
            drawingLayer.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, transform.Position, null, Color.White, MathHelper.ToRadians(transform.Theta), new Vector2(texture.Width / 2, texture.Height / 2), transform.Scale, SpriteEffects.None, 0);
        }

        public override void Update(float secondsPassed)
        {
        }
    }
}