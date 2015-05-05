using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Screens
{
    internal class ItemUpgrade : ItemMenu
    {
        public int level;

        public ItemUpgrade(Vector2 position, string text, int level)
            : base(position, text)
        {
            this.level = level;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Selected)
            {
                spriteBatch.Draw(UtilitiesGraphics.PlainTexture, new Rectangle((int)position.X, (int)position.Y, 100, 55), Fonts.Teal);
                spriteBatch.Draw(UtilitiesGraphics.PlainTexture, new Rectangle((int)position.X + 100, (int)position.Y, 475, 55), Fonts.Teal * 0.27f);
            }
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeNameFont, Fonts.Teal, position + new Vector2(100, 27), Text);
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeNameFont, Fonts.Teal, position + new Vector2(620, 27), "LVL: " + level);
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeNameFont, Fonts.Red, position + new Vector2(1000, 27), "COST: " + (level == 10 ? "MAX" : GetCost() + ""));
        }

        public int GetCost()
        {
            return level == 10 ? 0 : level * level;
        }
    }
}