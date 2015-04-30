using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Screens
{
    internal class UpgradeItem : MenuItem
    {
        public int level;

        public UpgradeItem(Vector2 position, string text, int level)
            : base(position, text)
        {
            this.level = level;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Selected)
            {
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)position.X, (int)position.Y, 100, 55), Fonts.Teal);
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)position.X + 100, (int)position.Y, 475, 55), Fonts.Teal * 0.27f);
            }
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeNameFont, Fonts.Teal, position + new Vector2(100, 27), Text);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeNameFont, Fonts.Teal, position + new Vector2(620, 27), "LVL: " + level);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeNameFont, Fonts.Red, position + new Vector2(1000, 27), "COST: " + GetCost());
        }

        public int GetCost()
        {
            return level * level;
        }
    }
}