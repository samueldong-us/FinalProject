using FinalProject.GameResources;
using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private CommandCenterScreen commandCenterScreen;
        private Screen current;
        private GameScreen gameScreen;
        private GraphicsDeviceManager graphics;
        private Keys[] lastPressedKeys;
        private LoadGameScreen loadGameScreen;
        private MainMenuScreen mainMenuScreen;
        private NewGameScreen newGameScreen;
        private SelectCharacterScreen selectCharacterScreen;
        private SelectDifficultyScreen selectDifficultyScreen;
        private SelectStageScreen selectStageScreen;
        private SplashScreen splashScreen;
        private SpriteBatch spriteBatch;
        private UpgradeScreen upgradeScreen;

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
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, GameUtilities.GetResizeMatrix(GraphicsDevice));
            if (current != null)
            {
                current.Draw(spriteBatch);
            }
            if (gameTime.ElapsedGameTime.TotalSeconds != 0)
            {
                spriteBatch.DrawString(Fonts.Debug, string.Format("FPS: {0:00.00}", 1 / gameTime.ElapsedGameTime.TotalSeconds), new Vector2(300, 10), Fonts.Red);
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
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsFixedTimeStep = false;
            GraphicsUtilities.CreateRenderTarget(GraphicsDevice);
            GraphicsUtilities.MakePlainTexture(GraphicsDevice);
            SaveGameManager.CreateSaveDirectory();
            commandCenterScreen = new CommandCenterScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            commandCenterScreen.FinishedTransitioningOut = CommandCenterFinishedTransitioningOut;
            commandCenterScreen.StartingTransitioningOut = CommandCenterStartingTransitioningOut;
            loadGameScreen = new LoadGameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            loadGameScreen.FinishedTransitioningOut = LoadGameScreenFinishedTransitioningOut;
            loadGameScreen.StartingTransitioningOut = LoadGameScreenStartingTransitioningOut;
            mainMenuScreen = new MainMenuScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            mainMenuScreen.FinishedTransitioningOut = MainMenuScreenFinishedTransitioningOut;
            mainMenuScreen.StartingTransitioningOut = MainMenuScreenStartingTransitioningOut;
            newGameScreen = new NewGameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            newGameScreen.FinishedTransitioningOut = NewGameScreenFinishedTransitioningOut;
            newGameScreen.StartingTransitioningOut = NewGameScreenStartingTransitioningOut;
            selectCharacterScreen = new SelectCharacterScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            selectCharacterScreen.FinishedTransitioningOut = SelectCharacterScreenFinishedTransitioningOut;
            selectCharacterScreen.StartingTransitioningOut = SelectCharacterScreenStartingTransitioningOut;
            selectDifficultyScreen = new SelectDifficultyScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            selectDifficultyScreen.FinishedTransitioningOut = SelectDifficultyScreenFinishedTransitioningOut;
            selectDifficultyScreen.StartingTransitioningOut = SelectDifficultyScreenStartingTransitioningOut;
            selectStageScreen = new SelectStageScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            selectStageScreen.FinishedTransitioningOut = SelectStageScreenFinishedTransitioningOut;
            selectStageScreen.StartingTransitioningOut = SelectStageScreenStartingTransitioningOut;
            splashScreen = new SplashScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            splashScreen.FinishedTransitioningOut = SplashScreenFinishedTransitioningOut;
            splashScreen.SplashScreenFinishedPlaying = SplashScreenFinishedPlaying;
            upgradeScreen = new UpgradeScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            upgradeScreen.FinishedTransitioningOut = UpgradeScreenFinishedTransitioningOut;
            upgradeScreen.StartingTransitioningOut = UpgradeScreenStartingTransitioningOut;
            gameScreen = new GameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            gameScreen.FinishedTransitioningOut = GameScreenFinishedTransitioningOut;
            gameScreen.StartingTransitioningOut = GameScreenStartingTransitioningOut;
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
            Fonts.LoadFonts(Content);
            splashScreen.LoadContent();
            mainMenuScreen.LoadContent();
            current = splashScreen;
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
            CheckPressedKeys();
            base.Update(gameTime);
        }

        private void CheckPressedKeys()
        {
            if (lastPressedKeys != null && current != null)
            {
                Keys[] currentPressedKeys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys key in currentPressedKeys)
                {
                    if (!Array.Exists<Keys>(lastPressedKeys, element => element == key))
                    {
                        current.KeyPressed(key);
                    }
                }
                foreach (Keys key in lastPressedKeys)
                {
                    if (!Array.Exists<Keys>(currentPressedKeys, element => element == key))
                    {
                        current.KeyReleased(key);
                    }
                }
            }
            lastPressedKeys = Keyboard.GetState().GetPressedKeys();
        }

        private void CommandCenterFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        current = mainMenuScreen;
                        mainMenuScreen.Start();
                    } break;
                case "UPGRADES":
                    {
                        current = upgradeScreen;
                        upgradeScreen.currentGame = commandCenterScreen.currentGame;
                        upgradeScreen.Start();
                    } break;
                case "LEVEL SELECT":
                    {
                        current = selectStageScreen;
                        selectStageScreen.currentGame = commandCenterScreen.currentGame;
                        selectStageScreen.Start();
                    } break;
            }
            commandCenterScreen.Stop();
            commandCenterScreen.UnloadContent();
        }

        private void CommandCenterStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        mainMenuScreen.LoadContentAsynchronously();
                    } break;
                case "UPGRADES":
                    {
                        upgradeScreen.LoadContentAsynchronously();
                    } break;
                case "LEVEL SELECT":
                    {
                        selectStageScreen.LoadContentAsynchronously();
                    } break;
            }
            commandCenterScreen.TransitionOut();
        }

        private void GameScreenFinishedTransitioningOut(string message)
        {
            current = selectStageScreen;
            selectStageScreen.Start();
            gameScreen.Stop();
            gameScreen.UnloadContent();
        }

        private void GameScreenStartingTransitioningOut(string message)
        {
            selectStageScreen.LoadContentAsynchronously();
            gameScreen.TransitionOut();
        }

        private void LoadGameScreenFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        current = mainMenuScreen;
                        mainMenuScreen.Start();
                    } break;
                default:
                    {
                        current = commandCenterScreen;
                        commandCenterScreen.currentGame = SaveGameManager.GetSavedGame(message + ".sav");
                        commandCenterScreen.Start();
                    } break;
            }
            loadGameScreen.Stop();
            loadGameScreen.UnloadContent();
        }

        private void LoadGameScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        mainMenuScreen.LoadContentAsynchronously();
                    } break;
                default:
                    {
                        commandCenterScreen.LoadContentAsynchronously();
                    } break;
            }
            loadGameScreen.TransitionOut();
        }

        private void MainMenuScreenFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "NEW GAME":
                    {
                        current = newGameScreen;
                        newGameScreen.Start();
                    } break;
                case "LOAD GAME":
                    {
                        current = loadGameScreen;
                        loadGameScreen.Start();
                    } break;
                case "QUIT GAME":
                    {
                        Exit();
                    } break;
            }
            mainMenuScreen.Stop();
            mainMenuScreen.UnloadContent();
        }

        private void MainMenuScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "NEW GAME":
                    {
                        newGameScreen.LoadContentAsynchronously();
                    } break;
                case "LOAD GAME":
                    {
                        loadGameScreen.LoadContentAsynchronously();
                    } break;
            }
            mainMenuScreen.TransitionOut();
        }

        private void NewGameScreenFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        current = mainMenuScreen;
                        mainMenuScreen.Start();
                    } break;
                default:
                    {
                        current = selectCharacterScreen;
                        selectCharacterScreen.currentGame = newGameScreen.currentGame;
                        selectCharacterScreen.Start();
                    } break;
            }
            newGameScreen.Stop();
            newGameScreen.UnloadContent();
        }

        private void NewGameScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        mainMenuScreen.LoadContentAsynchronously();
                    } break;
                default:
                    {
                        selectCharacterScreen.LoadContentAsynchronously();
                    } break;
            }
            newGameScreen.TransitionOut();
        }

        private void SelectCharacterScreenFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        current = newGameScreen;
                        newGameScreen.Start();
                    } break;
                default:
                    {
                        current = selectDifficultyScreen;
                        selectDifficultyScreen.currentGame = selectCharacterScreen.currentGame;
                        selectDifficultyScreen.Start();
                    } break;
            }
            selectCharacterScreen.Stop();
            selectCharacterScreen.UnloadContent();
        }

        private void SelectCharacterScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        newGameScreen.LoadContentAsynchronously();
                    } break;
                default:
                    {
                        selectDifficultyScreen.LoadContentAsynchronously();
                    } break;
            }
            selectCharacterScreen.TransitionOut();
        }

        private void SelectDifficultyScreenFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        current = selectCharacterScreen;
                        selectCharacterScreen.Start();
                    } break;
                default:
                    {
                        current = commandCenterScreen;
                        commandCenterScreen.currentGame = selectDifficultyScreen.currentGame;
                        commandCenterScreen.Start();
                    } break;
            }
            selectDifficultyScreen.Stop();
            selectDifficultyScreen.UnloadContent();
        }

        private void SelectDifficultyScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        selectCharacterScreen.LoadContentAsynchronously();
                    } break;
                default:
                    {
                        SaveGameManager.SaveGame(selectDifficultyScreen.currentGame);
                        commandCenterScreen.LoadContentAsynchronously();
                    } break;
            }
            selectDifficultyScreen.TransitionOut();
        }

        private void SelectStageScreenFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        current = commandCenterScreen;
                        commandCenterScreen.Start();
                    } break;
                case "LEVEL 1":
                    {
                        current = gameScreen;
                        gameScreen.Start();
                    } break;
                case "LEVEL 2":
                    {
                    } break;
                case "LEVEL 3":
                    {
                    } break;
            }
            selectStageScreen.Stop();
            selectStageScreen.UnloadContent();
        }

        private void SelectStageScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        commandCenterScreen.LoadContentAsynchronously();
                    } break;
                case "LEVEL 1":
                    {
                        gameScreen.LoadContentAsynchronously();
                    } break;
                case "LEVEL 2":
                    {
                    } break;
                case "LEVEL 3":
                    {
                    } break;
            }
            selectStageScreen.TransitionOut();
        }

        private void SplashScreenFinishedPlaying(string message)
        {
            splashScreen.TransitionOut();
        }

        private void SplashScreenFinishedTransitioningOut(string message)
        {
            current = mainMenuScreen;
            mainMenuScreen.Start();
            splashScreen.Stop();
            splashScreen.UnloadContent();
        }

        private void Test1()
        {
            Console.Write("Test1");
        }

        private void Test12(string toPrint)
        {
            Console.Write("Test1" + toPrint);
        }

        private void UpgradeScreenFinishedTransitioningOut(string message)
        {
            current = commandCenterScreen;
            commandCenterScreen.currentGame = upgradeScreen.currentGame;
            commandCenterScreen.Start();
            upgradeScreen.Stop();
            upgradeScreen.UnloadContent();
        }

        private void UpgradeScreenStartingTransitioningOut(string message)
        {
            commandCenterScreen.LoadContentAsynchronously();
            upgradeScreen.TransitionOut();
            SaveGameManager.SaveGame(upgradeScreen.currentGame);
        }
    }
}