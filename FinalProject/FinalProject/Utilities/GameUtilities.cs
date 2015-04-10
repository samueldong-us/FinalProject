using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FinalProject.Utilities
{
    internal static class GameUtilities
    {
        public static ContentManager GenerateNewContentManager(GameServiceContainer gameService)
        {
            ContentManager contentManager = new ContentManager(gameService, "Content");
            return contentManager;
        }

        public static Matrix GetResizeMatrix(GraphicsDevice graphicsDevice)
        {
            float widthScale = graphicsDevice.Viewport.Width / Constants.VirtualWidth;
            float heightScale = graphicsDevice.Viewport.Height / Constants.VirtualHeight;
            float scale = Math.Max(widthScale, heightScale);
            float xChange = (graphicsDevice.Viewport.Width / 2) - (Constants.VirtualWidth * scale / 2);
            float yChange = (graphicsDevice.Viewport.Height / 2) - (Constants.VirtualHeight * scale / 2);
            Matrix scaleMatrix = Matrix.CreateScale(scale);
            Matrix translateMatrix = Matrix.CreateTranslation(new Vector3(xChange, yChange, 0));
            return translateMatrix * scaleMatrix;
        }
    }
}