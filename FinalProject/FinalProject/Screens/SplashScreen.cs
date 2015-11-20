using FinalProject.GameComponents;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class SplashScreen : PixelatedTransitionScreen
    {
        private Texture2D background;
        private InterpolatedValue opacity;
        private bool ready;
        private ScrollingBackground scrollingBackground;
        private Texture2D title;

        public SplashScreen(ContentManager content, GraphicsDevice graphicsDevice)
            : base(content, graphicsDevice)
        {
            opacity = new LinearInterpolatedValue(1, 0, 2);
            opacity.InterpolationFinished = (parameter) => { ready = true; };
            scrollingBackground = new ScrollingBackground("Level3BG");
        }

        public override void KeyPressed(Keys key)
        {
            if (state == ScreenState.Active && ready && key == Keys.Enter)
            {
                BeginTransitioningOut();
                GameMain.Audio.PlayOneTimeSound("Menu Sound");
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
            spriteBatch.Draw(title, GameScreen.Visible, Color.White);
            spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), new Color(0, 0, 0, opacity.GetValue()));
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