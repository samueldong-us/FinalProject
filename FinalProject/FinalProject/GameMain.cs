using FinalProject.GameSaving;
using FinalProject.Messaging;
using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FinalProject
{
    public class GameMain : Microsoft.Xna.Framework.Game
    {
        public const int VirtualHeight = 1080;

        public const int VirtualWidth = 1920;

        public static AudioManager Audio;

        public static MessageCenter MessageCenter;

        public static Random RNG;

        private Screen currentScreen;

        private GraphicsDeviceManager graphics;

        private Dictionary<string, Screen> screens;

        private SpriteBatch spriteBatch;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            RNG = new Random();

            IntPtr hWnd = this.Window.Handle;
            var control = System.Windows.Forms.Control.FromHandle(hWnd);
            var form = control.FindForm();
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, UtilitiesGame.GetResizeMatrix(GraphicsDevice));
            DrawCurrentScreen();
            //DrawFPSCounter(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            InitializeGraphicsSettings();
            InitializeMessageCenter();
            Audio = new AudioManager();
            InitializeScreens();
            UtilitiesGraphics.Initialize(GraphicsDevice);
            SaveGameManager.CreateSaveDirectory();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Fonts.LoadFonts(Content);
            Audio.LoadContent(Content);
            UtilitiesGraphics.LoadCircularWipe(Content);
            screens["Splash Screen"].LoadContent();
            currentScreen = screens["Splash Screen"];
            currentScreen.Start();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardManager.BroadcastChanges(currentScreen);
            Audio.Update();
            if (currentScreen != null)
            {
                currentScreen.Update(gameTime);
            }
            base.Update(gameTime);
        }

        private void DrawCurrentScreen()
        {
            if (currentScreen != null)
            {
                currentScreen.Draw(spriteBatch);
            }
        }

        private void DrawFPSCounter(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.TotalSeconds != 0)
            {
                spriteBatch.DrawString(Fonts.DebugFont, string.Format("FPS: {0:00.00}", 1 / gameTime.ElapsedGameTime.TotalSeconds), new Vector2(300, 10), Fonts.Red);
            }
        }

        private void InitializeGraphicsSettings()
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = true;
            IsFixedTimeStep = false;
            graphics.ApplyChanges();
        }

        private void InitializeMessageCenter()
        {
            MessageCenter = new MessageCenter();
            MessageCenter.AddListener<string>("Start Loading Content", screen => screens[screen].LoadContentAsynchronously());
            MessageCenter.AddListener<string>("Switch Screens", SwitchScreens);
            MessageCenter.AddListener("Quit", Exit);
        }

        private void InitializeScreens()
        {
            screens = new Dictionary<string, Screen>();
            screens["Command Center"] = new ScreenCommandCenter(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Game"] = new ScreenGame(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Load Game"] = new ScreenLoadGame(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Main Menu"] = new ScreenMainMenu(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["New Game"] = new ScreenNewGame(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Select Character"] = new ScreenSelectCharacter(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Select Difficulty"] = new ScreenSelectDifficulty(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Select Stage"] = new ScreenSelectStage(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Settings"] = new ScreenSettings(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Show Credits"] = new ScreenCredits(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Splash Screen"] = new ScreenSplash(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
            screens["Upgrade"] = new ScreenUpgrade(UtilitiesGame.GenerateNewContentManager(Services), GraphicsDevice);
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