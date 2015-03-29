using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Screens
{
    internal class MainMenuScreen : Screen
    {
        private TransitionOutComplete FinishedTransitioningOut;

        public MainMenuScreen(ContentManager contentManager, TransitionOutComplete transitionOutCompleteDelegate)
            : base(contentManager)
        {
            FinishedTransitioningOut = transitionOutCompleteDelegate;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        protected override void LoadContent()
        {
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
        }

        public override void Reset()
        {
        }

        protected override void Set()
        {
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