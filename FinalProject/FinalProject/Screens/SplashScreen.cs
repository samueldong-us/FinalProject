using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace FinalProject.Screens
{
    internal class SplashScreen : Screen
    {
        public ScreenEvent FinishedTransitioningOut;
        public ScreenEvent SplashScreenFinishedPlaying;
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

        public override void LoadContent()
        {
            splashVideo = content.Load<Video>("Intro");
            base.LoadContent();
        }

        public override void Reset()
        {
        }

        public override void Start()
        {
            state = ScreenState.Active;
            splashVideoPlayer.Play(splashVideo);
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TransitionOut()
        {
            FinishedTransitioningOut("");
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
            if (state == ScreenState.Active)
            {
                if (splashVideoPlayer.State == MediaState.Stopped && lastVideoState == MediaState.Playing)
                {
                    SplashScreenFinishedPlaying("");
                }
                lastVideoState = splashVideoPlayer.State;
            }
        }

        protected override void Set()
        {
            lastVideoState = MediaState.Stopped;
        }
    }
}