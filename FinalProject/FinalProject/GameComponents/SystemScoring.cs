using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class SystemScoring
    {
        private Entity player;
        private int score;

        public SystemScoring()
        {
            score = 0;
        }

        public void AddWorth(int worth)
        {
            score += worth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeNameFont, Fonts.Teal, new Vector2(500, 500), "Score: " + score);
        }

        public void Reset()
        {
            player = null;
            score = 0;
        }

        public void SetPlayer(Entity player)
        {
            this.player = player;
        }
    }
}