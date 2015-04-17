using FinalProject.Messaging;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class DrawingComponent : Component
    {
        private Texture2D texture;
        private TransformComponent transform;
        private List<DrawingComponent> list;

        public DrawingComponent(MessageCenter messageCenter, Texture2D texture, TransformComponent transform, List<DrawingComponent> list)
            : base(messageCenter)
        {
            this.texture = texture;
            this.transform = transform;
            list.Add(this);
            this.list = list;
        }

        public override void Dispose()
        {
            list.Remove(this);
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