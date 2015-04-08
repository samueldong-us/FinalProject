using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Screens
{
    internal class MainMenuScreen : Screen
    {
        public ScreenEvent FinishedTransitioningOut;

        public MainMenuScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
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