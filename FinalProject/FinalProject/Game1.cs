using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FinalProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Screen current, next;
        private const float VirtualWidth = 1920.0f, VirtualHeight = 1080.0f;

        private SplashScreen splashScreen1, splashScreen2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, GetResizeMatrix());
            if (current != null)
            {
                current.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsFixedTimeStep = false;

            splashScreen1 = new SplashScreen(GenerateNewContentManager());
            splashScreen1.SplashScreenFinishedPlaying = Screen1FinishedPlaying;
            splashScreen1.FinishedTransitioningOut = Screen1FinishedTransitioningOut;
            splashScreen2 = new SplashScreen(GenerateNewContentManager());
            splashScreen2.SplashScreenFinishedPlaying = Screen2FinishedPlaying;
            splashScreen2.FinishedTransitioningOut = Screen2FinishedTransitioningOut;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            splashScreen1.LoadContent();
            splashScreen2.LoadContent();
            current = splashScreen1;
            current.Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (current != null)
            {
                current.Update(gameTime);
            }

            base.Update(gameTime);
        }

        private void Screen1FinishedPlaying()
        {
            splashScreen1.TransitionOut();
        }

        private void Screen2FinishedPlaying()
        {
            splashScreen2.TransitionOut();
        }

        private void Screen1FinishedTransitioningOut()
        {
            current = splashScreen2;
            splashScreen2.Start();
            splashScreen1.Stop();
        }

        private void Screen2FinishedTransitioningOut()
        {
            current = splashScreen1;
            splashScreen1.Start();
            splashScreen2.Stop();
        }

        private ContentManager GenerateNewContentManager()
        {
            ContentManager contentManager = new ContentManager(Services, "Content");
            return contentManager;
        }

        private Matrix GetResizeMatrix()
        {
            float widthScale = GraphicsDevice.Viewport.Width / VirtualWidth;
            float heightScale = GraphicsDevice.Viewport.Height / VirtualHeight;
            float scale = Math.Max(widthScale, heightScale);
            float xChange = (GraphicsDevice.Viewport.Width / 2) - (VirtualWidth * scale / 2);
            float yChange = (GraphicsDevice.Viewport.Height / 2) - (VirtualHeight * scale / 2);
            Matrix scaleMatrix = Matrix.CreateScale(scale);
            Matrix translateMatrix = Matrix.CreateTranslation(new Vector3(xChange, yChange, 0));
            return translateMatrix * scaleMatrix;
        }
    }
}