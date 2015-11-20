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
            float requiredWidthScale = graphicsDevice.Viewport.Width / (float)GameMain.VirtualWidth;
            float requiredHeightScale = graphicsDevice.Viewport.Height / (float)GameMain.VirtualHeight;
            float desiredScale = Math.Max(requiredWidthScale, requiredHeightScale);
            float requiredXTranslation = (graphicsDevice.Viewport.Width / 2) - (GameMain.VirtualWidth * desiredScale / 2);
            float requiredYTranslation = (graphicsDevice.Viewport.Height / 2) - (GameMain.VirtualHeight * desiredScale / 2);
            Matrix scaleMatrix = Matrix.CreateScale(desiredScale);
            Matrix translateMatrix = Matrix.CreateTranslation(new Vector3(requiredXTranslation, requiredYTranslation, 0));
            return translateMatrix * scaleMatrix;
        }
    }
}