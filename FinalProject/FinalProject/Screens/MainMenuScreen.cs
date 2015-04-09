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
        public ScreenEvent StartingTransitioningOut;
        private Texture2D placeholder;
        private Texture2D background;
        private InterpolatedValue scaleIn, scaleOut;
        private Texture2D buttonImage;
        private Texture2D buttonImageBuffer;
        private bool first;
        private SpriteFont menuText;

        public MainMenuScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.005f, 1, 2);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(1, .005f, 2);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            first = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        if (first == true)
                        {
                            first = false;
                            TextureUtilities.BeginDrawingToTexture(spriteBatch, graphicsDevice);
                            spriteBatch.Draw(background, new Rectangle(0, 0,  Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
                            spriteBatch.Draw(buttonImage, new Rectangle(288, 288, buttonImage.Width, buttonImage.Height), Color.White);
                            TextureUtilities.EndDrawingToTexture(spriteBatch, graphicsDevice);
                            buttonImageBuffer = TextureUtilities.DuplicateTexture(TextureUtilities.GetTexture(), graphicsDevice);
                        }
                        TextureUtilities.DrawPixelatedTexture(spriteBatch, buttonImageBuffer, Vector2.Zero, scaleIn.GetValue(), graphicsDevice);
                    } break;
                case ScreenState.Active:
                    {
                        TextureUtilities.BeginDrawingToTexture(spriteBatch, graphicsDevice);
                        spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
                        spriteBatch.Draw(buttonImage, new Rectangle(288, 288, buttonImage.Width, buttonImage.Height), Color.White);
                        spriteBatch.DrawString(menuText, "SETTINGS", new Vector2(500, 500), Color.White);
                        TextureUtilities.EndDrawingToTexture(spriteBatch, graphicsDevice);
                        buttonImageBuffer = TextureUtilities.DuplicateTexture(TextureUtilities.GetTexture(), graphicsDevice);
                        spriteBatch.Draw(buttonImageBuffer, new Rectangle(0, 0, buttonImageBuffer.Width, buttonImageBuffer.Height), Color.White);
                    } break;
                case ScreenState.TransitioningOut:
                    {

                        TextureUtilities.DrawPixelatedTexture(spriteBatch, buttonImageBuffer, Vector2.Zero, scaleOut.GetValue(), graphicsDevice);
                    } break;
            }
        }

        public override void LoadContent()
        {
            placeholder = content.Load<Texture2D>("PixelateTest");
            background = content.Load<Texture2D>("MenuBackground");
            buttonImage = content.Load<Texture2D>("button_0");
            menuText = content.Load<SpriteFont>("DebugFont");
            base.LoadContent();
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        scaleIn.Update(secondsPassed);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        scaleOut.Update(secondsPassed);
                    } break;
            }
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
            buttonImageBuffer = TextureUtilities.DuplicateTexture(TextureUtilities.GetTexture(), graphicsDevice);
            base.TransitionOut();
        }

        public override void KeyPressed(Keys key)
        {
            if (state == ScreenState.Active && key == Keys.Space)
            {
                StartingTransitioningOut();
            }
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishedTransitioningOut();
        }
    }
}