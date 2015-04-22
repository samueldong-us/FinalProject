using FinalProject.Messaging;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class RenderComponent : Component, Drawable
    {
        private ColliderComponent collider;
        private List<Drawable> drawingLayer;
        private Texture2D texture;
        private TransformComponent transform;

        public RenderComponent(MessageCenter messageCenter, Texture2D texture, TransformComponent transform, ColliderComponent collider, List<Drawable> drawingLayer)
            : base(messageCenter)
        {
            this.texture = texture;
            this.transform = transform;
            this.drawingLayer = drawingLayer;
            this.collider = collider;
            drawingLayer.Add(this);
        }

        public override void Dispose()
        {
            drawingLayer.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, transform.Position, null, Color.White, MathHelper.ToRadians(transform.Theta), new Vector2(texture.Width / 2, texture.Height / 2), transform.Scale, SpriteEffects.None, 0);
            if (collider != null)
            {
                foreach (Triangle triangle in collider.TransformedTriangles())
                {
                    spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)triangle.a.X, (int)triangle.a.Y, 3, 3), Color.Orange);
                    spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)triangle.b.X, (int)triangle.b.Y, 3, 3), Color.Orange);
                    spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)triangle.c.X, (int)triangle.c.Y, 3, 3), Color.Orange);
                }
            }
        }

        public override void Update(float secondsPassed)
        {
        }
    }
}