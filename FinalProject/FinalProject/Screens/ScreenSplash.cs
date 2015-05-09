using FinalProject.GameComponents;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Screens
{
    internal class ScreenSplash : ScreenPixelatedTransition
    {
        private Texture2D background;
        private InterpolatedValue opacity;
        private bool ready;
        private ScrollingBackground scrollingBackground;
        private Texture2D title;

        public ScreenSplash(ContentManager content, GraphicsDevice graphicsDevice)
            : base(content, graphicsDevice)
        {
            opacity = new InterpolatedValueExponential(1, 0, 3);
            opacity.InterpolationFinished = (parameter) => { ready = true; };
            scrollingBackground = new ScrollingBackground("Level1BG");
        }

        public override void KeyPressed(Keys key)
        {
            if (ready && key == Keys.Enter)
            {
                BeginTransitioningOut();
            }
            base.KeyPressed(key);
        }

        public override void LoadContent()
        {
            background = content.Load<Texture2D>("MenuBackgroundWithHole");
            title = content.Load<Texture2D>("TitleScreen");
            scrollingBackground.LoadContent(content);
            base.LoadContent();
        }

        protected override void ActiveUpdate(float secondsPassed)
        {
            scrollingBackground.Update(secondsPassed);
            opacity.Update(secondsPassed);
            base.ActiveUpdate(secondsPassed);
        }

        protected override void BeginTransitioningOut()
        {
            GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Main Menu");
            base.BeginTransitioningOut();
        }

        protected override void DrawScreen(SpriteBatch spriteBatch)
        {
            scrollingBackground.Draw(spriteBatch);
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            spriteBatch.Draw(title, ScreenGame.Visible, Color.White);
            spriteBatch.Draw(UtilitiesGraphics.PlainTexture, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), new Color(0, 0, 0, opacity.GetValue()));
        }

        protected override void Reset()
        {
            opacity.SetParameter(0);
            ready = false;
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Main Menu");
        }
    }
}