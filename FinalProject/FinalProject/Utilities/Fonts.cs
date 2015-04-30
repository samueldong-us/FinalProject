using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Utilities
{
    internal static class Fonts
    {
        public static SpriteFont DebugFont;
        public static SpriteFont MenuItemFont, MenuTitleFont, UpgradeNameFont, UpgradeCreditTextFont, UpgradeCreditsFont;
        public static Color Teal = new Color(32, 241, 175), Green = new Color(167, 240, 37), Red = new Color(241, 36, 79);

        public static void LoadFonts(ContentManager contentManager)
        {
            DebugFont = contentManager.Load<SpriteFont>("Debug");
            MenuItemFont = contentManager.Load<SpriteFont>("MenuItem");
            MenuTitleFont = contentManager.Load<SpriteFont>("MenuTitle");
            UpgradeNameFont = contentManager.Load<SpriteFont>("UpgradeName");
            UpgradeCreditTextFont = contentManager.Load<SpriteFont>("UpgradeCreditText");
            UpgradeCreditsFont = contentManager.Load<SpriteFont>("UpgradeCredits");
        }
    }
}