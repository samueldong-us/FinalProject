using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProject.Screens
{
    internal class SplashScreen : Screen
    {
        private MediaState lastVideoState;
        private Video splashVideo;
        private VideoPlayer splashVideoPlayer;

        public SplashScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            splashVideoPlayer = new VideoPlayer();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == ScreenState.Active)
            {
                spriteBatch.Draw(splashVideoPlayer.GetTexture(), new Rectangle(0, 0, 1920, 1080), Color.White);
            }
        }

        public override void KeyPressed(Keys key)
        {
            if (key == Keys.Space)
            {
                FinishTransitioningOut();
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Start()
        {
            lastVideoState = MediaState.Stopped;
            FinishTransitioningOut();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TransitionOut()
        {
        }

        protected override void BeginTransitioningOut()
        {
        }

        protected override void FinishedLoading()
        {
        }

        protected override void FinishTransitioningOut()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Main Menu");
        }

        protected override void Reset()
        {
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
            if (state == ScreenState.Active)
            {
                if (splashVideoPlayer.State == MediaState.Stopped && lastVideoState == MediaState.Playing)
                {
                    BeginTransitioningOut();
                }
                lastVideoState = splashVideoPlayer.State;
            }
        }
    }
}