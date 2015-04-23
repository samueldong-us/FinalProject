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
        public static Texture2D BulletTexture;

        public static void LoadContent(ContentManager content)
        {
            BulletTexture = content.Load<Texture2D>("TestBulletTri");
        }
    }
}