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
            Unit["Walking Fish01"] = new Rectangle(300, 0, 150, 150);
            Unit["Walking Fish02"] = new Rectangle(450, 0, 150, 150);
            Unit["Sea Slug"] = new Rectangle(600, 0, 150, 150);
            Unit["Jellyfish"] = new Rectangle(750, 0, 150, 150);
            Unit["Squid"] = new Rectangle(0, 150, 150, 150);
            Unit["Turtle"] = new Rectangle(150, 150, 150, 150);
            Unit["Starfish"] = new Rectangle(300, 150, 150, 150);
            Unit["Lobster"] = new Rectangle(450, 150, 150, 150);
            Unit["Flying Fish"] = new Rectangle(600, 150, 150, 150);
            Unit["Super Jelly"] = new Rectangle(750, 150, 150, 150);
            Unit["Bobbit Worm"] = new Rectangle(0, 300, 300, 300);
            Unit["Giant Isopod"] = new Rectangle(300, 300, 300, 300);
            Unit["Lantern Fish"] = new Rectangle(600, 300, 300, 300);
            Unit["Laser Beam"] = new Rectangle(0, 924, 1024, 100);
            BulletTexture = content.Load<Texture2D>("BulletTexture");
            Bullet = new List<Rectangle>();

            for (int i = 0; i < 17; i++)
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
            //0
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(40, 53), new Vector2(55, 45), new Vector2(45, 56)),
                new Triangle(new Vector2(60, 47), new Vector2(55, 45), new Vector2(45, 56)),
                new Triangle(new Vector2(40, 47), new Vector2(45, 44), new Vector2(60, 53)),
                new Triangle(new Vector2(40, 47), new Vector2(55, 56), new Vector2(60, 53))
            });
            //1
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(63, 46), new Vector2(63, 56), new Vector2(33, 56)),
                new Triangle(new Vector2(63, 46), new Vector2(63, 56), new Vector2(33, 46))
            });
            //2
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(43, 45), new Vector2(56, 45), new Vector2(56, 56)),
                new Triangle(new Vector2(43, 56), new Vector2(56, 45), new Vector2(56, 56))
            });
            //3
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(44, 35), new Vector2(55, 50), new Vector2(44, 65))
            });
            //4
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(60, 50), new Vector2(53, 45), new Vector2(39, 45)),
                new Triangle(new Vector2(60, 50), new Vector2(53, 55), new Vector2(39, 55))
            });
            //5
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(66, 50), new Vector2(50, 40), new Vector2(50, 60)),
                new Triangle(new Vector2(33, 50), new Vector2(50, 40), new Vector2(50, 60))
            });
            //6
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //7
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //8
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //9
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //10
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //11
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //12
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //13
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //14
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //15
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
            });
            //16
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(36, 32), new Vector2(36, 68), new Vector2(63, 50))
            });
            //17
            BulletTriangles.Add(new List<Triangle>() {
                new Triangle(new Vector2(), new Vector2(), new Vector2())
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
                new Triangle(new Vector2(50, 74), new Vector2(69, 43), new Vector2(69, 103)),
                new Triangle(new Vector2(84, 74), new Vector2(69, 43), new Vector2(69, 103))
            };
            UnitTriangles["Walking Fish02"] = new List<Triangle>()
            {
                new Triangle(new Vector2(43, 74), new Vector2(59, 33), new Vector2(59, 113)),
                new Triangle(new Vector2(101, 74), new Vector2(59, 33), new Vector2(59, 113))
            };
            UnitTriangles["Jellyfish"] = new List<Triangle>()
            {
                new Triangle(new Vector2(47, 74), new Vector2(65, 10), new Vector2(65, 137)),
                new Triangle(new Vector2(131, 74), new Vector2(65, 10), new Vector2(65, 137))
            };
            UnitTriangles["Sea Slug"] = new List<Triangle>()
            {
                new Triangle(new Vector2(117, 74), new Vector2(97, 29), new Vector2(33, 104)),
                new Triangle(new Vector2(117, 74), new Vector2(33, 104), new Vector2(77, 77)),
                new Triangle(new Vector2(117, 74), new Vector2(33, 104), new Vector2(97, 117)),
                new Triangle(new Vector2(117, 74), new Vector2(42, 55), new Vector2(77, 77))
            };
            UnitTriangles["Squid"] = new List<Triangle>()
            {
                new Triangle(new Vector2(3, 74), new Vector2(67, 63), new Vector2(67, 85)),
                new Triangle(new Vector2(146, 74), new Vector2(67, 63), new Vector2(67, 85)),
                new Triangle(new Vector2(146, 74), new Vector2(67, 63), new Vector2(124, 49)),
                new Triangle(new Vector2(146, 74), new Vector2(110, 102), new Vector2(67, 85))
            };
            UnitTriangles["Turtle"] = new List<Triangle>()
            {
                new Triangle(new Vector2(17, 74), new Vector2(64, 40), new Vector2(64, 111)),
                new Triangle(new Vector2(132, 74), new Vector2(64, 40), new Vector2(64, 111)),
                new Triangle(new Vector2(98, 3), new Vector2(121, 33), new Vector2(98, 61)),
                new Triangle(new Vector2(98, 146), new Vector2(121, 116), new Vector2(98, 89))
            };
            UnitTriangles["Starfish"] = new List<Triangle>()
            {
                new Triangle(new Vector2(21, 74), new Vector2(60, 13), new Vector2(60, 137)),
                new Triangle(new Vector2(60, 74), new Vector2(127, 106), new Vector2(60, 137)),
                new Triangle(new Vector2(60, 74), new Vector2(127, 44), new Vector2(60, 13)),
                new Triangle(new Vector2(60, 74), new Vector2(127, 44), new Vector2(127, 106))
            };
            UnitTriangles["Lobster"] = new List<Triangle>()
            {
                new Triangle(new Vector2(30, 74), new Vector2(17, 46), new Vector2(30, 29)),
                new Triangle(new Vector2(30, 74), new Vector2(17, 103), new Vector2(30, 120)),
                new Triangle(new Vector2(30, 74), new Vector2(58, 2), new Vector2(58, 146)),
                new Triangle(new Vector2(58, 2), new Vector2(136, 57), new Vector2(98, 74)),
                new Triangle(new Vector2(58, 146), new Vector2(136, 90), new Vector2(98, 74))
            };
            UnitTriangles["Flying Fish"] = new List<Triangle>()
            {
                new Triangle(new Vector2(42, 74), new Vector2(72, 3), new Vector2(99, 51)),
                new Triangle(new Vector2(42, 74), new Vector2(72, 147), new Vector2(99, 98)),
                new Triangle(new Vector2(42, 74), new Vector2(103, 50), new Vector2(103, 100))
            };
            UnitTriangles["Super Jelly"] = new List<Triangle>()
            {
                new Triangle(new Vector2(56, 74), new Vector2(34, 34), new Vector2(41, 18)),
                new Triangle(new Vector2(56, 74), new Vector2(34, 115), new Vector2(41, 131)),

                new Triangle(new Vector2(104, 25), new Vector2(66, 2), new Vector2(41, 18)),
                new Triangle(new Vector2(104, 126), new Vector2(66, 148), new Vector2(41, 131)),

                new Triangle(new Vector2(104, 25), new Vector2(105, 74), new Vector2(117, 45)),
                new Triangle(new Vector2(104, 126), new Vector2(105, 74), new Vector2(117, 103))
            };
            UnitTriangles["Bobbit Worm"] = new List<Triangle>()
            {
                new Triangle(new Vector2(103, 108), new Vector2(103, 190), new Vector2(111, 150)),

                new Triangle(new Vector2(103, 108), new Vector2(133, 85), new Vector2(143, 89)),
                new Triangle(new Vector2(103, 108), new Vector2(133, 214), new Vector2(143, 210)),

                new Triangle(new Vector2(134, 12), new Vector2(155, 0), new Vector2(143, 89)),
                new Triangle(new Vector2(134, 287), new Vector2(155, 299), new Vector2(143, 210)),

                new Triangle(new Vector2(180, 11), new Vector2(155, 0), new Vector2(143, 89)),
                new Triangle(new Vector2(180, 287), new Vector2(155, 299), new Vector2(143, 210)),

                new Triangle(new Vector2(158, 150), new Vector2(158, 55), new Vector2(193, 133)),
                new Triangle(new Vector2(158, 150), new Vector2(158, 243), new Vector2(193, 166)),

                new Triangle(new Vector2(103, 108), new Vector2(103, 111), new Vector2(60, 93)),
                new Triangle(new Vector2(103, 186), new Vector2(103, 189), new Vector2(60, 206)),
                new Triangle(new Vector2(187, 119), new Vector2(189, 125), new Vector2(256, 136)),
                new Triangle(new Vector2(187, 180), new Vector2(180, 174), new Vector2(256, 161))
            };
            UnitTriangles["Giant Isopod"] = new List<Triangle>()
            {
                new Triangle(new Vector2(15, 150), new Vector2(33, 77), new Vector2(82, 22)),
                new Triangle(new Vector2(15, 150), new Vector2(33, 221), new Vector2(82, 277)),

                new Triangle(new Vector2(159, 150), new Vector2(120, 22), new Vector2(82, 22)),
                new Triangle(new Vector2(159, 150), new Vector2(120, 277), new Vector2(82, 277)),

                new Triangle(new Vector2(130, 51), new Vector2(139, 77), new Vector2(233, 77)),
                new Triangle(new Vector2(130, 248), new Vector2(139, 221), new Vector2(233, 221)),

                new Triangle(new Vector2(146, 97), new Vector2(158, 135), new Vector2(278, 130)),
                new Triangle(new Vector2(146, 199), new Vector2(158, 161), new Vector2(278, 166))
            };
            UnitTriangles["Lantern Fish"] = new List<Triangle>()
            {
                new Triangle(new Vector2(24, 150), new Vector2(123, 132), new Vector2(123, 166)),
                new Triangle(new Vector2(134, 150), new Vector2(123, 132), new Vector2(123, 166)),

                new Triangle(new Vector2(119, 123), new Vector2(119, 102), new Vector2(134, 115)),
                new Triangle(new Vector2(119, 176), new Vector2(119, 196), new Vector2(134, 183)),

                new Triangle(new Vector2(101, 41), new Vector2(129, 41), new Vector2(109, 61)),
                new Triangle(new Vector2(101, 258), new Vector2(129, 258), new Vector2(109, 237)),

                new Triangle(new Vector2(136, 150), new Vector2(177, 167), new Vector2(177, 232)),

                new Triangle(new Vector2(187, 67), new Vector2(248, 106), new Vector2(260, 150)),
                new Triangle(new Vector2(187, 232), new Vector2(248, 190), new Vector2(260, 150)),

                new Triangle(new Vector2(210, 27), new Vector2(246, 4), new Vector2(274, 37)),
                new Triangle(new Vector2(210, 271), new Vector2(246, 292), new Vector2(274, 261))
            };
            UnitTriangles["Laser Beam"] = new List<Triangle>()
            {
                new Triangle(new Vector2(55, 23), new Vector2(1024, 23), new Vector2(1024, 77)),
                new Triangle(new Vector2(55, 77), new Vector2(55, 23), new Vector2(1024, 77))
            };
        }
    }
}
