using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class GameAssets
    {
        public static Texture2D BGTexture;

        public static List<Rectangle> Bullet;

        public static Texture2D BulletTexture;

        public static List<List<Triangle>> BulletTriangles;

        public static Texture2D CircularHealthBar1;

        public static Dictionary<string, Rectangle> Unit;

        public static Texture2D UnitTexture;

        public static Dictionary<string, List<Triangle>> UnitTriangles;

        public static void LoadBackground(ContentManager content, string file)
        {
            BGTexture = content.Load<Texture2D>(file);
        }

        public static void LoadContent(ContentManager content)
        {
            CircularHealthBar1 = content.Load<Texture2D>("CircularHealthBarTest");
            UnitTexture = content.Load<Texture2D>("UnitTexture");
            Unit = new Dictionary<string, Rectangle>();
            Unit["Spread Shot Ship"] = new Rectangle(0, 0, 100, 100);
            Unit["Homing Ship"] = new Rectangle(100, 0, 100, 100);
            Unit["Laser Ship"] = new Rectangle(200, 0, 100, 100);
            Unit["Walking Fish01"] = new Rectangle(300, 0, 100, 100);
            Unit["Walking Fish02"] = new Rectangle(400, 0, 100, 100);
            Unit["Jellyfish"] = new Rectangle(500, 0, 100, 100);
            Unit["Sea Slug"] = new Rectangle(600, 0, 100, 100);
            Unit["Laser Beam"] = new Rectangle(0, 924, 1024, 100);
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
                new Triangle(new Vector2(40, 53), new Vector2(55, 45), new Vector2(45, 56)),
                new Triangle(new Vector2(60, 47), new Vector2(55, 45), new Vector2(45, 56)),
                new Triangle(new Vector2(40, 47), new Vector2(45, 44), new Vector2(60, 53)),
                new Triangle(new Vector2(40, 47), new Vector2(55, 56), new Vector2(60, 53))
            });
            UnitTriangles = new Dictionary<string, List<Triangle>>();
            UnitTriangles["Spread Shot Ship"] = new List<Triangle>() {
                new Triangle(new Vector2(42, 50), new Vector2(50, 44), new Vector2(58, 50)),
                new Triangle(new Vector2(42, 50), new Vector2(50, 56), new Vector2(58, 50))
            };
            UnitTriangles["Homing Ship"] = new List<Triangle>()
            {
                new Triangle(new Vector2(43, 49), new Vector2(51, 46), new Vector2(51, 52)),
                new Triangle(new Vector2(60, 49), new Vector2(51, 46), new Vector2(51, 52))
            };
            UnitTriangles["Laser Ship"] = new List<Triangle>()
            {
                new Triangle(new Vector2(34, 50), new Vector2(42, 45), new Vector2(42, 55)),
                new Triangle(new Vector2(50, 50), new Vector2(42, 45), new Vector2(42, 55))
            };
            UnitTriangles["Walking Fish01"] = new List<Triangle>()
            {
                new Triangle(new Vector2(32, 50), new Vector2(51, 28), new Vector2(51, 72)),
                new Triangle(new Vector2(68, 50), new Vector2(51, 28), new Vector2(51, 72))
            };
            UnitTriangles["Walking Fish02"] = new List<Triangle>()
            {
                new Triangle(new Vector2(30, 50), new Vector2(51, 22), new Vector2(51, 79)),
                new Triangle(new Vector2(74, 50), new Vector2(51, 22), new Vector2(51, 78))
            };
            UnitTriangles["Jellyfish"] = new List<Triangle>()
            {
                new Triangle(new Vector2(25, 50), new Vector2(36, 9), new Vector2(36, 91)),
                new Triangle(new Vector2(80, 50), new Vector2(36, 9), new Vector2(36, 91))
            };
            UnitTriangles["Sea Slug"] = new List<Triangle>()
            {
                new Triangle(new Vector2(30, 39), new Vector2(65, 22), new Vector2(76, 50)),
                new Triangle(new Vector2(30, 39), new Vector2(51, 52), new Vector2(76, 50)),
                new Triangle(new Vector2(26, 69), new Vector2(51, 52), new Vector2(76, 50)),
                new Triangle(new Vector2(26, 69), new Vector2(65, 72), new Vector2(76, 50))
            };
            UnitTriangles["Laser Beam"] = new List<Triangle>()
            {
                new Triangle(new Vector2(55, 23), new Vector2(1024, 23), new Vector2(1024, 77)),
                new Triangle(new Vector2(55, 77), new Vector2(55, 23), new Vector2(1024, 77))
            };
        }
    }
}