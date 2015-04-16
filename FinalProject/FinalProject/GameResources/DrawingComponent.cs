using FinalProject.Messaging;
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

        public DrawingComponent(MessageCenter messageCenter, Texture2D texture, TransformComponent transform)
            : base(messageCenter)
        {
            this.texture = texture;
            this.transform = transform;
        }

        public override void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Update(float secondsPassed)
        {
        }
    }
}