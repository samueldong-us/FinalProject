using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Screens
{
    internal class MenuItem
    {
        private Vector2 position;
        public bool Selected;
        public bool Disabled;

        public string Text { get; private set; }

        public MenuItem(Vector2 position, string text)
        {
            this.position = position;
            this.Text = text;
            Selected = false;
            Disabled = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Selected)
            {
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)position.X, (int)position.Y, 100, 100), Fonts.Teal);
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)position.X + 100, (int)position.Y, 860, 100), Fonts.Teal * 0.27f);
            }
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuItems, Text, position + new Vector2(100, 55), Disabled ? Fonts.Teal * 0.27f : Fonts.Teal);
        }
    }
}