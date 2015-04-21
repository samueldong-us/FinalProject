using FinalProject.GameSaving;
using FinalProject.Messaging;
using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FinalProject
{
    public class GameMain : Microsoft.Xna.Framework.Game
    {
        public static MessageCenter MessageCenter;
        private Screen currentScreen;
        private GraphicsDeviceManager graphics;
        private Keys[] lastPressedKeys;
        private Dictionary<string, Screen> screens;
        private SpriteBatch spriteBatch;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, GameUtilities.GetResizeMatrix(GraphicsDevice));
            if (currentScreen != null)
            {
                currentScreen.Draw(spriteBatch);
            }
            if (gameTime.ElapsedGameTime.TotalSeconds != 0)
            {
                spriteBatch.DrawString(Fonts.Debug, string.Format("FPS: {0:00.00}", 1 / gameTime.ElapsedGameTime.TotalSeconds), new Vector2(300, 10), Fonts.Red);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsFixedTimeStep = false;
            GraphicsUtilities.CreateRenderTarget(GraphicsDevice);
            GraphicsUtilities.MakePlainTexture(GraphicsDevice);
            SaveGameManager.CreateSaveDirectory();
            InitializeMessageCenter();
            InitializeScreens();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Fonts.LoadFonts(Content);
            GraphicsUtilities.LoadCircularWipe(Content);
            screens["Splash Screen"].LoadContent();
            screens["Main Menu"].LoadContent();
            currentScreen = screens["Splash Screen"];
            currentScreen.Start();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            CheckPressedKeys();
            if (currentScreen != null)
            {
                currentScreen.Update(gameTime);
            }
            base.Update(gameTime);
        }

        private void CheckPressedKeys()
        {
            if (lastPressedKeys != null && currentScreen != null)
            {
                Keys[] currentPressedKeys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys key in currentPressedKeys)
                {
                    if (!Array.Exists<Keys>(lastPressedKeys, element => element == key))
                    {
                        currentScreen.KeyPressed(key);
                    }
                }
                foreach (Keys key in lastPressedKeys)
                {
                    if (!Array.Exists<Keys>(currentPressedKeys, element => element == key))
                    {
                        currentScreen.KeyReleased(key);
                    }
                }
            }
            lastPressedKeys = Keyboard.GetState().GetPressedKeys();
        }

        private void InitializeMessageCenter()
        {
            MessageCenter = new MessageCenter();
            MessageCenter.AddListener<string>("Start Loading Content", StartLoadingContent);
            MessageCenter.AddListener<string>("Switch Screens", SwitchScreens);
            MessageCenter.AddListener("Quit", Exit);
        }

        private void InitializeScreens()
        {
            screens = new Dictionary<string, Screen>();
            screens["Command Center"] = new CommandCenterScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Load Game"] = new LoadGameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Main Menu"] = new MainMenuScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["New Game"] = new NewGameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Select Character"] = new SelectCharacterScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Select Difficulty"] = new SelectDifficultyScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Select Stage"] = new SelectStageScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Splash Screen"] = new SplashScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Upgrade"] = new UpgradeScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Game"] = new GameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
        }

        private void StartLoadingContent(string screen)
        {
            screens[screen].LoadContentAsynchronously();
        }

        private void SwitchScreens(string screen)
        {
            Screen oldScreen = currentScreen;
            currentScreen = screens[screen];
            currentScreen.Start();
            oldScreen.Stop();
            oldScreen.UnloadContent();
        }
    }
}