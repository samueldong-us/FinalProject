using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FinalProject.Utilities
{
    internal static class UtilitiesGraphics
    {
        public static Texture2D PlainTexture = null;

        private static Effect circularWipe;

        private static Matrix currentMatrix;

        private static RenderTarget2D renderTarget = null;

        public static void BeginDrawingPixelated(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float scale)
        {
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateScale(scale));
            currentMatrix = Matrix.CreateScale(scale);
        }

        public static void BeginDrawingWithCircularWipe(SpriteBatch spriteBatch, float amount)
        {
            if (circularWipe != null)
            {
                spriteBatch.End();
                circularWipe.Parameters["Amount"].SetValue(amount);
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, circularWipe, currentMatrix);
            }
            else
            {
                throw new Exception("Circular Wipe Must Be Loaded First");
            }
        }

        public static void DrawStringVerticallyCentered(SpriteBatch spriteBatch, SpriteFont spriteFont, Color color, Vector2 position, String text)
        {
            Vector2 stringSize = spriteFont.MeasureString(text);
            Vector2 centeredPosition = position - new Vector2(0, stringSize.Y / 2);
            spriteBatch.DrawString(spriteFont, text, centeredPosition, color);
        }

        public static void EndDrawingPixelated(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float scale)
        {
            Rectangle destinationRectangle = new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight);
            Rectangle scaledRectangle = new Rectangle(0, 0, (int)(GameMain.VirtualWidth * scale), (int)(GameMain.VirtualHeight * scale));
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, UtilitiesGame.GetResizeMatrix(graphicsDevice));
            spriteBatch.Draw((Texture2D)renderTarget, destinationRectangle, scaledRectangle, Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, UtilitiesGame.GetResizeMatrix(graphicsDevice));
            currentMatrix = UtilitiesGame.GetResizeMatrix(graphicsDevice);
        }

        public static void EndDrawingWithCircularWipe(SpriteBatch spriteBatch)
        {
            if (circularWipe != null)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, currentMatrix);
            }
            else
            {
                throw new Exception("Circular Wipe Must Be Loaded First");
            }
        }

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            CreateRenderTarget(graphicsDevice);
            MakePlainTexture(graphicsDevice);
        }

        public static void LoadCircularWipe(ContentManager contentManager)
        {
            circularWipe = contentManager.Load<Effect>("CircularWipe");
            circularWipe.Parameters["Gradient"].SetValue(contentManager.Load<Texture2D>("Circular Gradient"));
        }

        private static void CreateRenderTarget(GraphicsDevice graphicsDevice)
        {
            renderTarget = new RenderTarget2D(
                graphicsDevice,
               GameMain.VirtualWidth,
                GameMain.VirtualHeight,
                false,
                graphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
        }

        private static void MakePlainTexture(GraphicsDevice graphicsDevice)
        {
            PlainTexture = new Texture2D(graphicsDevice, 1, 1);
            PlainTexture.SetData<Color>(new Color[] { Color.White });
        }
    }
}