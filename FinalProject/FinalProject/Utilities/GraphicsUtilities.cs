using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FinalProject.Utilities
{
    internal static class GraphicsUtilities
    {
        public static Texture2D PlainTexture = null;
        private static Effect circularWipe;
        private static Matrix currentMatrix;
        private static RenderTarget2D renderTarget = null;

        public static void BeginDrawingPixelated(SpriteBatch spriteBatch, Vector2 position, int width, int height, float scale, GraphicsDevice graphicsDevice)
        {
            Rectangle scaled = new Rectangle(0, 0, (int)(width * scale), (int)(height * scale));
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

        public static void CreateRenderTarget(GraphicsDevice graphicsDevice)
        {
            renderTarget = new RenderTarget2D(
                graphicsDevice,
               Constants.VirtualWidth,
                Constants.VirtualHeight,
                false,
                graphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
        }

        public static void DrawStringVerticallyCentered(SpriteBatch spriteBatch, SpriteFont spriteFont, String text, Vector2 location, Color color)
        {
            Vector2 stringSize = spriteFont.MeasureString(text);
            location.Y -= stringSize.Y / 2;
            spriteBatch.DrawString(spriteFont, text, location, color);
        }

        public static void EndDrawingPixelated(SpriteBatch spriteBatch, int width, int height, Vector2 position, float scale, GraphicsDevice graphicsDevice)
        {
            Rectangle destination = new Rectangle((int)position.X, (int)position.Y, width, height);
            Rectangle scaled = new Rectangle(0, 0, (int)(width * scale), (int)(height * scale));
            spriteBatch.End();
            currentMatrix = GameUtilities.GetResizeMatrix(graphicsDevice);
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, GameUtilities.GetResizeMatrix(graphicsDevice));
            spriteBatch.Draw((Texture2D)renderTarget, destination, scaled, Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, GameUtilities.GetResizeMatrix(graphicsDevice));
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

        public static Color[,] GetColorsFromTexture(Texture2D texture)
        {
            Color[] rawArray = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(rawArray);
            Color[,] colorArray = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    int rawIndex = y * texture.Width + x;
                    colorArray[x, y] = rawArray[rawIndex];
                }
            }
            return colorArray;
        }

        public static void LoadCircularWipe(ContentManager contentManager)
        {
            circularWipe = contentManager.Load<Effect>("CircularWipe");
            circularWipe.Parameters["Gradient"].SetValue(contentManager.Load<Texture2D>("Circular Gradient"));
        }

        public static void MakePlainTexture(GraphicsDevice graphicsDevice)
        {
            PlainTexture = new Texture2D(graphicsDevice, 1, 1);
            PlainTexture.SetData<Color>(new Color[] { Color.White });
        }
    }
}