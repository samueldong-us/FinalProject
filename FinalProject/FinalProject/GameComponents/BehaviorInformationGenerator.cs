using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal static class BehaviorInformationGenerator
    {
        public static List<Vector2> GenerateLoopBack()
        {
            int randomInteger = GameMain.RNG.Next(4);
            int row = 1;
            int column = 0;
            if (randomInteger % 2 == 0)
            {
                column = 3;
            }
            if (randomInteger / 2 == 1)
            {
                row = 2;
            }
            return LoopBackFromQuadrant(row, column);
        }

        public static List<Vector2> GenerateSigmoid()
        {
            int randomInteger = GameMain.RNG.Next(4);
            int row = 1;
            int column = 0;
            if (randomInteger % 2 == 0)
            {
                column = 3;
            }
            if (randomInteger / 2 == 1)
            {
                row = 2;
            }
            return SigmoidFromQuadrant(row, column);
        }

        public static List<Vector2> GenerateStraightWithLoop()
        {
            int randomInteger = GameMain.RNG.Next(4);
            int row = 1;
            int column = 0;
            if (randomInteger % 2 == 0)
            {
                column = 3;
            }
            if (randomInteger / 2 == 1)
            {
                row = 2;
            }
            return StraightWithLoopFromQuadrant(row, column);
        }

        public static Vector4 RandomSpawnAndSwitchPosition()
        {
            int column = GameMain.RNG.Next(4);
            int newColumn = column > 1 ? 2 : 1;
            Vector2 spawnPosition = RandomPointInQuadrant(0, column);
            Vector2 switchPosition = RandomPointInQuadrant(1, newColumn);
            return new Vector4(spawnPosition.X, spawnPosition.Y, switchPosition.X, switchPosition.Y);
        }

        private static List<Vector2> LoopBackFromQuadrant(int row, int column)
        {
            Vector2 spawn = RandomPointInQuadrant(row, column);
            int newRow = row == 1 ? 2 : 1;
            Vector2 end = RandomPointInQuadrant(newRow, column);
            int newColumn = column == 0 ? 2 : 1;
            Vector2 otherPoint = RandomPointInQuadrant(row, newColumn);
            float height = end.Y - spawn.Y;
            List<Vector2> path = new List<Vector2>();
            path.Add(spawn);
            path.Add(spawn + new Vector2((otherPoint.X - spawn.X) * .5f, height * .1f));
            path.Add(spawn + new Vector2((otherPoint.X - spawn.X), height * .3f));
            path.Add(end + new Vector2((otherPoint.X - end.X), -height * .3f));
            path.Add(end + new Vector2((otherPoint.X - end.X) * .5f, -height * .1f));
            path.Add(end);
            return path;
        }

        private static float RandomHorizontalPoint()
        {
            return (float)(GameMain.RNG.NextDouble() * ScreenGame.Bounds.Width * .8 + ScreenGame.Bounds.Left + ScreenGame.Bounds.Width * .1);
        }

        private static Vector2 RandomPointInQuadrant(int row, int column)
        {
            int originX = ScreenGame.Bounds.Left - 550;
            int originY = ScreenGame.Bounds.Top - 250;
            int quadrantX = originX + column * 525;
            int quadrantY = originY + row * 250;
            float x = (float)(GameMain.RNG.NextDouble() * 525 + quadrantX);
            float y = (float)(GameMain.RNG.NextDouble() * 250 + quadrantY);
            return new Vector2(x, y);
        }

        private static List<Vector2> SigmoidFromQuadrant(int row, int column)
        {
            Vector2 spawn = RandomPointInQuadrant(row, column);
            int newRow = row == 1 ? 2 : 1;
            int newColumn = column == 0 ? 3 : 0;
            Vector2 end = RandomPointInQuadrant(newRow, newColumn);
            float horizontalPosition = RandomHorizontalPoint();
            float height = end.Y - spawn.Y;
            List<Vector2> path = new List<Vector2>();
            path.Add(spawn);
            path.Add(spawn + new Vector2((horizontalPosition - spawn.X) * .5f, height * .1f));
            path.Add(spawn + new Vector2((horizontalPosition - spawn.X), height * .3f));
            path.Add(end + new Vector2((horizontalPosition - end.X), -height * .3f));
            path.Add(end + new Vector2((horizontalPosition - end.X) * .5f, -height * .1f));
            path.Add(end);
            return path;
        }

        private static List<Vector2> StraightWithLoopFromQuadrant(int row, int column)
        {
            Vector2 spawn = RandomPointInQuadrant(row, column);
            float horizontalPosition = RandomHorizontalPoint();
            float verticalDirection = row == 1 ? 1 : -1;
            float horizontalDirection = column == 0 ? 1 : -1;
            List<Vector2> path = new List<Vector2>();
            path.Add(spawn);
            path.Add(new Vector2(horizontalPosition + horizontalDirection * 50, spawn.Y + verticalDirection * 25));
            path.Add(new Vector2(horizontalPosition + horizontalDirection * 50, spawn.Y + verticalDirection * 200));
            path.Add(new Vector2(horizontalPosition - horizontalDirection * 50, spawn.Y + verticalDirection * 200));
            path.Add(new Vector2(horizontalPosition - horizontalDirection * 50, spawn.Y + verticalDirection * 25));
            int newColumn = column == 0 ? 3 : 0;
            path.Add(RandomPointInQuadrant(row, newColumn));
            return path;
        }
    }
}