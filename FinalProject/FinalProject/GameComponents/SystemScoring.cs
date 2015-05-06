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
        private int maxScore;
        private Entity player;
        private int score;

        public SystemScoring()
        {
            score = 0;
            maxScore = 1;
        }

        public void AddWorth(int worth)
        {
            score += worth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeCreditsFont, Fonts.Teal, new Vector2(430, 40), "SCORE: " + score + "/" + maxScore);
        }

        public void Reset()
        {
            player = null;
            score = 0;
        }

        public void SetMaxScore(int max)
        {
            maxScore = max;
        }

        public void SetPlayer(Entity player)
        {
            this.player = player;
        }
    }
}