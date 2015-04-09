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
        private Texture2D buttonStale;
        private Texture2D screenShot;
        private SpriteFont currFont;
        private int buttonX;
        private int buttonY;
        private bool first = true;

        private InterpolatedValue scaleIn, scaleOut;

        public MainMenuScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.005f, 1, 3);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(1, .005f, 3);
            scaleOut.InterpolationFinished = ScaleOutFinished;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        if (first)
                        {
                            first = false;
                            TextureUtilities.BeginDrawingToTexture(spriteBatch, graphicsDevice);
                            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
                            spriteBatch.Draw(buttonStale, new Rectangle(buttonX, buttonY, buttonStale.Width, buttonStale.Height), Color.White);
                            spriteBatch.Draw(TextureUtilities.PlainTexture, new Rectangle(buttonX-3,buttonY-1,120, 30), Color.Blue);
                            spriteBatch.DrawString(currFont, "Butt Stuff", new Vector2(buttonX, buttonY), Color.PaleGoldenrod);
                            TextureUtilities.EndDrawingToTexture(spriteBatch, graphicsDevice);
                            screenShot = TextureUtilities.DuplicateTexture(TextureUtilities.GetTexture(), graphicsDevice);
                        }
                        TextureUtilities.DrawPixelatedTexture(spriteBatch, screenShot, Vector2.Zero, scaleIn.GetValue(), graphicsDevice);
                    } break;
                case ScreenState.Active:
                    {
                        TextureUtilities.BeginDrawingToTexture(spriteBatch, graphicsDevice);

                        spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
                        spriteBatch.Draw(buttonStale, new Rectangle(buttonX, buttonY, buttonStale.Width, buttonStale.Height), Color.White);
                        spriteBatch.Draw(TextureUtilities.PlainTexture, new Rectangle(buttonX - 3, buttonY -1, 120, 30), Color.Blue);
                        spriteBatch.DrawString(currFont, "Butt Stuff", new Vector2(buttonX, buttonY), Color.PaleGoldenrod);
                        TextureUtilities.EndDrawingToTexture(spriteBatch, graphicsDevice);
                        spriteBatch.Draw(TextureUtilities.GetTexture(), new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        TextureUtilities.DrawPixelatedTexture(spriteBatch, screenShot, Vector2.Zero, scaleOut.GetValue(), graphicsDevice);
                    } break;
            }
        }

        public override void LoadContent()
        {
            placeholder = content.Load<Texture2D>("PixelateTest");
            background = content.Load<Texture2D>("MenuBackground");
            buttonStale = content.Load<Texture2D>("Button_0");
            currFont = content.Load<SpriteFont>("DebugFont");
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
            buttonX = Constants.VirtualWidth / 2;
            buttonY = Constants.VirtualHeight / 2;
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
            screenShot = TextureUtilities.DuplicateTexture(TextureUtilities.GetTexture(), graphicsDevice);
            base.TransitionOut();
        }

        public override void KeyPressed(Keys key)
        {
            if (state == ScreenState.Active && key == Keys.Space)
            {
                StartingTransitioningOut();
            }
            if (state == ScreenState.Active && key == Keys.Up)
            {
                buttonY -= 10;
            }
            if (state == ScreenState.Active && key == Keys.Down)
            {
                buttonY += 10;
            }
            if (state == ScreenState.Active && key == Keys.Left)
            {
                buttonX -= 10;
            }
            if (state == ScreenState.Active && key == Keys.Right)
            {
                buttonX += 10;
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