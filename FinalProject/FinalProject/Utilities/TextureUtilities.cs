using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FinalProject.Utilities
{
    internal static class TextureUtilities
    {
        private static RenderTarget2D renderTarget = null;
        public static Texture2D PlainTexture = null;

        public static void MakePlainTexture(GraphicsDevice graphicsDevice)
        {
            PlainTexture = new Texture2D(graphicsDevice, 1, 1);
            PlainTexture.SetData<Color>(new Color[] { Color.White });
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

        public static Texture2D GetTexture()
        {
            return (Texture2D)renderTarget;
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