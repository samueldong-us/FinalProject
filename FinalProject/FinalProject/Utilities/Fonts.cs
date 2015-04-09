using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Utilities
{
    internal static class Fonts
    {
        public static SpriteFont Debug;
        public static SpriteFont MenuItems, MenuTitle, UpgradeOptions, UpgradeBoldCredits, UpgradeLightCredits;
        public static Color Teal = new Color(32, 241, 175), Green = new Color(167, 240, 37), Red = new Color(241, 36, 79);

        public static void LoadFonts(ContentManager contentManager)
        {
            Debug = contentManager.Load<SpriteFont>("Debug");
            MenuItems = contentManager.Load<SpriteFont>("MenuItems");
            MenuTitle = contentManager.Load<SpriteFont>("MenuTitle");
            UpgradeOptions = contentManager.Load<SpriteFont>("UpgradeOptions");
            UpgradeBoldCredits = contentManager.Load<SpriteFont>("UpgradeBoldCredits");
            UpgradeLightCredits = contentManager.Load<SpriteFont>("UpgradeLightCredits");
        }
    }
}