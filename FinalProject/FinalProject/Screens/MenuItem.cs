using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Screens
{
    internal class MenuItem
    {
        public bool Disabled;

        public bool Selected;

        public string Text;

        protected Vector2 position;

        public MenuItem(Vector2 position, string text)
        {
            this.position = position;
            this.Text = text;
            Selected = false;
            Disabled = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Selected && !Disabled)
            {
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)position.X, (int)position.Y, 100, 100), Fonts.Teal);
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)position.X + 100, (int)position.Y, 860, 100), Fonts.Teal * 0.27f);
            }
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuItemFont, Disabled ? Fonts.Teal * 0.27f : Fonts.Teal, position + new Vector2(100, 55), Text);
        }
    }
}