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
        private VideoPlayer splashVideoPlayer;
        private Video splashVideo;
        private MediaState lastVideoState;

        public SplashScreen(ContentManager contentManager)
            : base(contentManager)
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

        protected override void ScreenUpdate(float secondsPassed)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        state = ScreenState.Active;
                        splashVideoPlayer.Play(splashVideo);
                    } break;
                case ScreenState.Active:
                    {
                        if (splashVideoPlayer.State == MediaState.Stopped && lastVideoState == MediaState.Playing)
                        {
                            SplashScreenFinishedPlaying();
                        }
                        lastVideoState = splashVideoPlayer.State;
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        FinishedTransitioningOut();
                    } break;
            }
        }

        public override void Reset()
        {
        }

        protected override void Set()
        {
            lastVideoState = MediaState.Stopped;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TransitionOut()
        {
            base.TransitionOut();
        }
    }
}