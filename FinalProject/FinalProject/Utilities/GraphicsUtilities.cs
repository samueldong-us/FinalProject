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
        private static RenderTarget2D renderTarget = null;

        public static void BeginDrawingToTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (renderTarget != null)
            {
                spriteBatch.End();
                graphicsDevice.SetRenderTarget(renderTarget);
                spriteBatch.Begin();
            }
            else
            {
                throw new Exception("Render Target Must Be Created First");
            }
        }

        public static void BeginDrawingWithCircularWipe(SpriteBatch spriteBatch, float amount)
        {
            if (circularWipe != null)
            {
                spriteBatch.End();
                circularWipe.Parameters["Amount"].SetValue(amount);
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, circularWipe);
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

        public static void DrawPixelatedTexture(SpriteBatch spriteBatch, Texture2D source, Vector2 position, float scale, GraphicsDevice graphicsDevice)
        {
            Rectangle destination = new Rectangle((int)position.X, (int)position.Y, source.Width, source.Height);
            Rectangle scaled = new Rectangle(0, 0, (int)(source.Width * scale), (int)(source.Height * scale));
            BeginDrawingToTexture(spriteBatch, graphicsDevice);
            spriteBatch.Draw(source, scaled, Color.White);
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, GameUtilities.GetResizeMatrix(graphicsDevice));
            spriteBatch.Draw(GetTexture(), destination, scaled, Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, GameUtilities.GetResizeMatrix(graphicsDevice));
        }

        public static void DrawStringVerticallyCentered(SpriteBatch spriteBatch, SpriteFont spriteFont, String text, Vector2 location, Color color)
        {
            Vector2 stringSize = spriteFont.MeasureString(text);
            location.Y -= stringSize.Y / 2;
            spriteBatch.DrawString(spriteFont, text, location, color);
        }

        public static Texture2D DuplicateTexture(Texture2D texture, GraphicsDevice graphicsDevice)
        {
            Texture2D duplicate = new Texture2D(graphicsDevice, texture.Width, texture.Height);
            Color[] rawArray = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(rawArray);
            duplicate.SetData<Color>(rawArray);
            return duplicate;
        }

        public static void EndDrawingToTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (renderTarget != null)
            {
                spriteBatch.End();
                graphicsDevice.SetRenderTarget(null);
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, GameUtilities.GetResizeMatrix(graphicsDevice));
            }
            else
            {
                throw new Exception("Render Target Must Be Created First");
            }
        }

        public static void EndDrawingWithCircularWipe(SpriteBatch spriteBatch)
        {
            if (circularWipe != null)
            {
                spriteBatch.End();
                spriteBatch.Begin();
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

        public static Texture2D GetTexture()
        {
            return (Texture2D)renderTarget;
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

        private static Texture2D GetTextureFromColors(Color[,] colors, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, colors.GetLength(0), colors.GetLength(1));
            Color[] rawArray = new Color[colors.Length];
            for (int x = 0; x < colors.GetLength(0); x++)
            {
                for (int y = 0; y < colors.GetLength(1); y++)
                {
                    int rawIndex = y * colors.GetLength(0) + x;
                    rawArray[rawIndex] = colors[x, y];
                }
            }
            texture.SetData<Color>(rawArray);
            return texture;
        }
    }
}