using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class GameAssets
    {
        public static List<Rectangle> Bullet;
        public static Texture2D BulletTexture;
        public static List<List<Triangle>> BulletTriangles;
        public static Dictionary<string, Rectangle> Unit;
        public static Texture2D UnitTexture;
        public static Dictionary<string, List<Triangle>> UnitTriangles;

        public static void LoadContent(ContentManager content)
        {
            UnitTexture = content.Load<Texture2D>("UnitTexture");
            Unit = new Dictionary<string, Rectangle>();
            Unit["Spread Shot Ship"] = new Rectangle(0, 0, 100, 100);
            BulletTexture = content.Load<Texture2D>("BulletTexture");
            Bullet = new List<Rectangle>();
            for (int i = 0; i < 18; i++)
            {
                int x = (i % 10) * 100;
                int y = (i / 10) * 100;
                Bullet.Add(new Rectangle(x, y, 100, 100));
            }
            SetupTriangles();
        }

        private static void SetupTriangles()
        {
            BulletTriangles = new List<List<Triangle>>();
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(40, 53), new Vector2(55, 45), new Vector2(45, 56))
            });
            UnitTriangles = new Dictionary<string, List<Triangle>>();
            UnitTriangles["Spread Shot Ship"] = new List<Triangle>() {
                new Triangle(new Vector2(42, 50), new Vector2(50, 44), new Vector2(58, 50)),
                new Triangle(new Vector2(42, 50), new Vector2(50, 56), new Vector2(58, 50))
            };
        }
    }
}