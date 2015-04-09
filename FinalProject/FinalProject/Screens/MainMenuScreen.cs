using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class MainMenuScreen : Screen
    {
        public ScreenEvent FinishedTransitioningOut;
        private Texture2D placeholder;
        private InterpolatedValue scale;

        public MainMenuScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scale = new ExponentialInterpolatedValue(1, .01f, 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            TextureUtilities.DrawPixelatedTexture(spriteBatch, placeholder, Vector2.Zero, scale.GetValue(), graphicsDevice);
        }

        public override void LoadContent()
        {
            placeholder = content.Load<Texture2D>("PixelateTest");
            base.LoadContent();
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
            scale.Update(secondsPassed);
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

        public override void KeyPressed(Keys key)
        {
            if (key == Keys.Space)
            {
                scale.SetParameter(0);
            }
        }
    }
}