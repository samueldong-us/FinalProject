using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ScrollingBackground
    {
        private const int PixelsPerSecond = 200;
        private int currentRow;
        private float currentY;
        private int nextRow;

        public ScrollingBackground()
        {
            currentY = 0;
            currentRow = 0;
            nextRow = 0;
            IncrementRows();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentY > 4096 - ScreenGame.Bounds.Height)
            {
                spriteBatch.Draw(GameAssets.BGTexture, new Rectangle(ScreenGame.Bounds.Left, ScreenGame
                    .Bounds.Bottom - 4096 + (int)currentY, ScreenGame
                    .Bounds.Width, 4096 - (int)currentY), new Rectangle(currentRow * 1024, 0, 1024, 4096 - (int)currentY), Color.White);

                spriteBatch.Draw(GameAssets.BGTexture, new Rectangle(ScreenGame.Bounds.Left, ScreenGame
                    .Bounds.Top, ScreenGame
                    .Bounds.Width, ScreenGame.Bounds.Height - 4096 + (int)currentY), new Rectangle(nextRow * 1024, 8192 - (int)currentY - ScreenGame.Bounds.Height, 1024, ScreenGame.Bounds.Height - 4096 + (int)currentY), Color.White);
            }
            else
            {
                spriteBatch.Draw(GameAssets.BGTexture, ScreenGame.Bounds, new Rectangle(currentRow * 1024, 4096 - (int)currentY - ScreenGame.Bounds.Height, 1024, ScreenGame.Bounds.Height), Color.White);
            }
        }

        public void Update(float secondsPassed)
        {
            currentY += PixelsPerSecond * secondsPassed;
            if (currentY > 4096)
            {
                currentY -= 4096;
                IncrementRows();
            }
        }

        private void IncrementRows()
        {
            currentRow = nextRow;
            if (currentRow % 2 == 0)
            {
                nextRow = GameMain.RNG.Next(2) * 2 + 1;
            }
            else
            {
                nextRow = GameMain.RNG.Next(2) * 2;
            }
        }
    }
}