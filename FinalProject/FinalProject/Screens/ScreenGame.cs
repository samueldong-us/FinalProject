using FinalProject.GameComponents;
using FinalProject.GameSaving;
using FinalProject.GameWaves;
using FinalProject.Messaging;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject.Screens
{
    internal class ScreenGame : ScreenPixelatedTransition
    {
        public static Rectangle Bounds = new Rectangle(460, 0, 1000, 1062);
        public static SystemCollisions Collisions;
        public static SystemDrawing Drawing;
        public static SystemEntity Entities;
        public static MessageCenter MessageCenter;
        public static SystemScoring Scoring;
        public static Rectangle Visible = new Rectangle(460, 0, 1000, 1080);
        private SaveGame currentGame;
        private InterpolatedValue fadeIn;
        private Texture2D gameHUD;
        private bool gameOver;
        private ItemGroupMenu menuItems;
        private bool paused;
        private Keys[] pressedKeys;
        private bool readyToLeave;
        private Random rng = new Random();
        private ScrollingBackground scrollingBackground;
        private SystemWaves waveManager;

        public ScreenGame(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            InitializeSystems();
            InitializeMessageCenter();
            InitializeMenu();
            fadeIn = new InterpolatedValueLinear(0, .75f, 1);
            fadeIn.InterpolationFinished = (leftOver) => { readyToLeave = true; };
            GameMain.MessageCenter.AddListener<SaveGame, string>("Save Game and Stage Pass to Game", SetCurrentGameAndStage);
        }

        public override void KeyPressed(Keys key)
        {
            if (state == ScreenState.Active)
            {
                if (!paused)
                {
                    MessageCenter.Broadcast<Keys>("Key Pressed", key);
                }
                switch (key)
                {
                    case Keys.Enter:
                        {
                            if (readyToLeave)
                            {
                                if (!gameOver)
                                {
                                    currentGame.Credits += Scoring.GetScore();
                                    if (FactoryUnit.Stage + 1 > currentGame.HighestUnlockedStage && currentGame.HighestUnlockedStage < 3)
                                    {
                                        currentGame.HighestUnlockedStage = FactoryUnit.Stage + 1;
                                    }
                                    SaveGameManager.SaveGame(currentGame);
                                }
                                BeginTransitioningOut();
                                GameMain.Audio.PlayOneTimeSound("Menu Sound");
                            }
                            if (paused)
                            {
                                if (menuItems.GetSelected().Equals("RESUME GAME"))
                                {
                                    Unpause();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                }
                                else
                                {
                                    Unpause();
                                    BeginTransitioningOut();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                }
                            }
                        } break;
                    case Keys.Up:
                        {
                            if (paused)
                            {
                                menuItems.MoveUp();
                                GameMain.Audio.PlayOneTimeSound("Menu Sound");
                            }
                        } break;
                    case Keys.Down:
                        {
                            if (paused)
                            {
                                menuItems.MoveDown();
                                GameMain.Audio.PlayOneTimeSound("Menu Sound");
                            }
                        } break;
                    case Keys.Escape:
                        {
                            if (paused)
                            {
                                Unpause();
                                GameMain.Audio.PlayOneTimeSound("Menu Sound");
                            }
                            else if (!gameOver && !EnemiesGone())
                            {
                                Pause();
                                GameMain.Audio.PlayOneTimeSound("Menu Sound");
                            }
                        } break;
                }
            }
        }

        public override void KeyReleased(Keys key)
        {
            if (state == ScreenState.Active && !paused)
            {
                MessageCenter.Broadcast<Keys>("Key Released", key);
            }
        }

        public override void LoadContent()
        {
            gameHUD = content.Load<Texture2D>("gameHUD");
            GameAssets.LoadContent(content);
            scrollingBackground.LoadContent(content);
            base.LoadContent();
        }

        public override void Start()
        {
            Setup();
            base.Start();
        }

        protected override void ActiveUpdate(float secondsPassed)
        {
            if (!paused)
            {
                scrollingBackground.Update(secondsPassed);
                waveManager.Update(secondsPassed);
                Collisions.Update();
                Entities.Update(secondsPassed);
                MessageCenter.CleanUp();
                if (gameOver || EnemiesGone())
                {
                    fadeIn.Update(secondsPassed);
                }
            }
        }

        protected override void BeginTransitioningOut()
        {
            GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Select Stage", currentGame);
            GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Select Stage");
            base.BeginTransitioningOut();
        }

        protected override void DrawScreen(SpriteBatch spriteBatch)
        {
            scrollingBackground.Draw(spriteBatch);
            Drawing.Draw(spriteBatch);
            spriteBatch.Draw(gameHUD, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            Scoring.Draw(spriteBatch);
            if (gameOver)
            {
                spriteBatch.Draw(UtilitiesGraphics.PlainTexture, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), new Color(0, 0, 0, fadeIn.GetValue()));
                UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Red * fadeIn.GetValue(), new Vector2(460, 500), "DEFEAT");
                UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuItemFont, Fonts.Red * fadeIn.GetValue(), new Vector2(460, 600), "EARNED 0 CREDITS");
            }
            else
            {
                if (EnemiesGone())
                {
                    spriteBatch.Draw(UtilitiesGraphics.PlainTexture, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), new Color(0, 0, 0, fadeIn.GetValue()));
                    UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green * fadeIn.GetValue(), new Vector2(460, 500), "VICTORY");
                    UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuItemFont, Fonts.Green * fadeIn.GetValue(), new Vector2(460, 600), "EARNED " + Scoring.GetScore() + " CREDITS");
                }
            }
            if (paused)
            {
                spriteBatch.Draw(UtilitiesGraphics.PlainTexture, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), new Color(0, 0, 0, .75f));
                menuItems.Draw(spriteBatch);
            }
        }

        protected override void Reset()
        {
            Entities.Dispose();
            Collisions.Dispose();
            Drawing.Dispose();
            MessageCenter.CleanUp();
            Scoring.Reset();
            fadeIn.SetParameter(0);
            gameOver = false;
            readyToLeave = false;
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Stage");
        }

        private bool EnemiesGone()
        {
            bool waveManagerFinished = waveManager.GetNumberOfWaves() == 0;
            bool enemiesGone = Collisions.GetCount("Enemy") == 0;
            bool enemyBulletsGone = Collisions.GetCount("EnemyBullet") == 0;
            return waveManagerFinished && enemiesGone && enemyBulletsGone;
        }

        private void InitializeMenu()
        {
            menuItems = new ItemGroupMenu();
            menuItems.AddItem(new ItemMenu(new Vector2(280, 320), "RESUME GAME"));
            menuItems.AddItem(new ItemMenu(new Vector2(280, 450), "QUIT GAME"));
        }

        private void InitializeMessageCenter()
        {
            MessageCenter = new MessageCenter();
            MessageCenter.AddListener<Entity>("Find Closest Player", Collisions.ClosestPlayer);
            MessageCenter.AddListener<Entity, float>("Find Closest Enemy By Angle", Collisions.ClosestEnemyByAngle);
            MessageCenter.AddListener<int>("Add Worth", Scoring.AddWorth);
        }

        private void InitializeSystems()
        {
            Collisions = new SystemCollisions();
            Drawing = new SystemDrawing();
            Entities = new SystemEntity();
            Scoring = new SystemScoring();
        }

        private void Pause()
        {
            paused = true;
            pressedKeys = KeyboardManager.GetPressedKeys();
        }

        private void SetCurrentGameAndStage(SaveGame saveGame, string stage)
        {
            currentGame = saveGame;
            FactoryUnit.Difficulty = currentGame.difficulty;
            switch (stage)
            {
                case "LEVEL 1":
                    {
                        FactoryUnit.Stage = 1;
                    } break;
                case "LEVEL 2":
                    {
                        FactoryUnit.Stage = 2;
                    } break;
                case "LEVEL 3":
                    {
                        FactoryUnit.Stage = 3;
                    } break;
            }
            scrollingBackground = new ScrollingBackground("Level" + FactoryUnit.Stage + "BG");
        }

        private void Setup()
        {
            switch (FactoryUnit.Stage)
            {
                case 1:
                    {
                        waveManager = new SystemWaves(LevelGenerator.GenerateLevel1());
                    } break;
                case 2:
                    {
                        waveManager = new SystemWaves(LevelGenerator.GenerateLevel2());
                    } break;
                case 3:
                    {
                        waveManager = new SystemWaves(LevelGenerator.GenerateLevel3());
                    } break;
            }
            Scoring.SetMaxScore(waveManager.GetTotalPossibleScore());
            Entity player = FactoryPlayer.CreatePlayer(currentGame);
            player.MessageCenter.AddListener("Health Depleted", () => { gameOver = true; });
            Scoring.SetPlayer(player);
            Entities.AddEntity(player);
        }

        private void Unpause()
        {
            paused = false;
            foreach (Keys key in pressedKeys)
            {
                MessageCenter.Broadcast<Keys>("Key Released", key);
            }
        }
    }
}