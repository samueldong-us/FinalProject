using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class ScrollingBackground
    {
        private const float MaxRot = (float)(Math.PI / 2);

        private const float MaxTime = 10;

        private const float MinRot = (float)(Math.PI / 8);

        private const float MinTime = 2;

        private const int PixelsPerSecond = 100;

        private const int PlanetSpeed = 150;

        private Texture2D background;

        private int currentRow;

        private float currentY;

        private string levelBackground;

        private int nextRow;

        private List<Vector4> planetInfo;

        private Texture2D planets;

        private List<Point> planetTexture;

        private float untilNextPlanet;

        public ScrollingBackground(string levelBackground)
        {
            currentY = 0;
            currentRow = 0;
            nextRow = 0;
            planetInfo = new List<Vector4>();
            planetTexture = new List<Point>();
            IncrementRows();
            this.levelBackground = levelBackground;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentY > 4096 - ScreenGame.Visible.Height)
            {
                spriteBatch.Draw(background, new Rectangle(ScreenGame.Visible.Left, ScreenGame
                    .Visible.Bottom - 4096 + (int)currentY, ScreenGame
                    .Visible.Width, 4096 - (int)currentY), new Rectangle(currentRow * 1024, 0, 1024, 4096 - (int)currentY), Color.White);

                spriteBatch.Draw(background, new Rectangle(ScreenGame.Visible.Left, ScreenGame
                    .Visible.Top, ScreenGame
                    .Visible.Width, ScreenGame.Visible.Height - 4096 + (int)currentY), new Rectangle(nextRow * 1024, 8192 - (int)currentY - ScreenGame.Visible.Height, 1024, ScreenGame.Visible.Height - 4096 + (int)currentY), Color.White);
            }
            else
            {
                spriteBatch.Draw(background, ScreenGame.Visible, new Rectangle(currentRow * 1024, 4096 - (int)currentY - ScreenGame.Visible.Height, 1024, ScreenGame.Visible.Height), Color.White);
            }
            for (int i = 0; i < planetTexture.Count; i++)
            {
                spriteBatch.Draw(planets, new Rectangle((int)planetInfo[i].X, (int)planetInfo[i].Y, 400, 400), new Rectangle(planetTexture[i].X, planetTexture[i].Y, 700, 700), Color.White, planetInfo[i].Z, new Vector2(350, 350), SpriteEffects.None, 0);
            }
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(levelBackground);
            planets = content.Load<Texture2D>("PlanetSprites");
        }

        public void Update(float secondsPassed)
        {
            untilNextPlanet -= secondsPassed;
            if (untilNextPlanet < 0)
            {
                UpdateNextTime();
                AddPlanet();
            }
            for (int i = planetInfo.Count - 1; i >= 0; i--)
            {
                planetInfo[i] += new Vector4(0, PlanetSpeed * secondsPassed, planetInfo[i].W * secondsPassed, 0);
                if (planetInfo[i].Y > ScreenGame.Bounds.Bottom + 700)
                {
                    planetInfo.RemoveAt(i);
                    planetTexture.RemoveAt(i);
                    continue;
                }
            }
            currentY += PixelsPerSecond * secondsPassed;
            if (currentY > 4096)
            {
                currentY -= 4096;
                IncrementRows();
            }
        }

        private void AddPlanet()
        {
            int randomRow = GameMain.RNG.Next(2);
            int randomColumn = GameMain.RNG.Next(4);
            planetTexture.Add(new Point(randomColumn * 700, randomRow * 700));
            float x = (float)(GameMain.RNG.NextDouble() * ScreenGame.Bounds.Width + ScreenGame.Bounds.Left);
            float rotSpeed = RandomRotationSpeed();
            Vector4 info = new Vector4(x, ScreenGame.Bounds.Top - 700, (float)(Math.PI * 2 * GameMain.RNG.NextDouble()), rotSpeed);
            planetInfo.Add(info);
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

        private float RandomRotationSpeed()
        {
            return (float)(GameMain.RNG.NextDouble() * (MaxRot - MinRot) + MinRot);
        }

        private void UpdateNextTime()
        {
            untilNextPlanet = (float)(GameMain.RNG.NextDouble() * (MaxTime - MinTime) + MinTime);
        }
    }
}