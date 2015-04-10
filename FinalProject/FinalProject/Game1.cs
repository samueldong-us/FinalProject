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
        private Screen current, next;
        private GraphicsDeviceManager graphics;
        private Keys[] lastPressedKeys;
        private LoadGameScreen loadGameScreen;
        private MainMenuScreen mainMenuScreen;
        private SplashScreen splashScreen;
        private SpriteBatch spriteBatch;
        private NewGameScreen newGameScreen;
        private SelectCharacterScreen selectCharacterScreen;
        private SelectDifficultyScreen selectDifficultyScreen;

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
            splashScreen = new SplashScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            splashScreen.SplashScreenFinishedPlaying = SplashScreenFinishedPlaying;
            splashScreen.FinishedTransitioningOut = SplashScreenFinishedTransitioningOut;
            mainMenuScreen = new MainMenuScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            mainMenuScreen.StartingTransitioningOut = MainMenuScreenStartingTransitioningOut;
            mainMenuScreen.FinishedTransitioningOut = MainMenuScreenFinishedTransitioningOut;
            loadGameScreen = new LoadGameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            loadGameScreen.FinishedTransitioningOut = LoadGameScreenFinishedTransitioningOut;
            loadGameScreen.StartingTransitioningOut = LoadGameScreenStartingTransitioningOut;
            commandCenterScreen = new CommandCenterScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            commandCenterScreen.StartingTransitioningOut = CommandCenterStartingTransitioningOut;
            commandCenterScreen.FinishedTransitioningOut = CommandCenterFinishedTransitioningOut;
            newGameScreen = new NewGameScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            newGameScreen.FinishedTransitioningOut = NewGameScreenFinishedTransitioningOut;
            newGameScreen.StartingTransitioningOut = NewGameScreenStartingTransitioningOut;
            selectCharacterScreen = new SelectCharacterScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            selectCharacterScreen.StartingTransitioningOut = SelectCharacterScreenStartingTransitioningOut;
            selectCharacterScreen.FinishedTransitioningOut = SelectCharacterScreenFinishedTransitioningOut;
            selectDifficultyScreen = new SelectDifficultyScreen(GameUtilities.GenerateNewContentManager(Services), GraphicsDevice);
            selectDifficultyScreen.StartingTransitioningOut = SelectDifficultyScreenStartingTransitioningOut;
            selectDifficultyScreen.FinishedTransitioningOut = SelectDifficultyScreenFinishedTransitioningOut;
            //Testing Purposes
            for (int i = 0; i < 23; i++)
            {
                SaveGameManager.SaveGame(new SaveGame() { SaveName = "Test Save " + i });
            }
            //End Testing
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
            }
            commandCenterScreen.TransitionOut();
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
        private void SelectDifficultyScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        selectCharacterScreen.LoadContentAsynchronously();
                    }break;
                default:
                    {
                        SaveGameManager.SaveGame(selectDifficultyScreen.currentGame);
                        commandCenterScreen.LoadContentAsynchronously();
                    }break;
            }
            selectDifficultyScreen.TransitionOut();
        }
        private void SelectDifficultyScreenFinishedTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        current = selectCharacterScreen;
                        selectCharacterScreen.Start();
                    }break;
                default:
                    {
                        current = commandCenterScreen;
                        commandCenterScreen.currentGame = selectDifficultyScreen.currentGame;
                        commandCenterScreen.Start();
                    }break;
            }
            selectDifficultyScreen.Stop();
            selectDifficultyScreen.UnloadContent();
        }
        private void SelectCharacterScreenStartingTransitioningOut(string message)
        {
            switch (message)
            {
                case "":
                    {
                        newGameScreen.LoadContentAsynchronously();
                    }break;
                default:
                    {
                        selectDifficultyScreen.LoadContentAsynchronously();
                    }break;
            }
            selectCharacterScreen.TransitionOut();
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
    }
}