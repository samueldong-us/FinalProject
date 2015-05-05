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
        public static Rectangle Bounds = new Rectangle(420, 0, 1000, 1062);

        public static SystemCollisions Collisions;

        public static SystemDrawing Drawing;

        public static SystemEntity Entities;

        public static MessageCenter MessageCenter;

        public static Rectangle Visible = new Rectangle(420, 0, 1000, 1080);

        private Texture2D background;

        private SaveGame currentGame;

        private ItemGroupMenu menuItems;

        private bool paused;

        private Keys[] pressedKeys;

        private Random rng = new Random();

        private ScrollingBackground scrollingBackground;

        private SystemWaves waveManager;

        public ScreenGame(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            InitializeSystems();
            InitializeMessageCenter();
            InitializeMenu();
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
                            if (paused)
                            {
                                if (menuItems.GetSelected().Equals("RESUME GAME"))
                                {
                                    Unpause();
                                }
                                else
                                {
                                    Unpause();
                                    BeginTransitioningOut();
                                }
                            }
                        } break;
                    case Keys.Up:
                        {
                            if (paused)
                            {
                                menuItems.MoveUp();
                            }
                        } break;
                    case Keys.Down:
                        {
                            if (paused)
                            {
                                menuItems.MoveDown();
                            }
                        } break;
                    case Keys.Escape:
                        {
                            if (paused)
                            {
                                Unpause();
                            }
                            else
                            {
                                Pause();
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
            background = content.Load<Texture2D>("MenuBackground");
            GameAssets.LoadContent(content);
            base.LoadContent();
        }

        public override void Start()
        {
            scrollingBackground = new ScrollingBackground();
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
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            scrollingBackground.Draw(spriteBatch);
            Drawing.Draw(spriteBatch);
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
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Stage");
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
        }

        private void InitializeSystems()
        {
            Collisions = new SystemCollisions();
            Drawing = new SystemDrawing();
            Entities = new SystemEntity();
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
                        GameAssets.LoadBackground(content, "Level1BG");
                    } break;
                case "LEVEL 2":
                    {
                        FactoryUnit.Stage = 2;
                        GameAssets.LoadBackground(content, "Level2BG");
                    } break;
                case "LEVEL 3":
                    {
                        FactoryUnit.Stage = 3;
                        GameAssets.LoadBackground(content, "Level3BG");
                    } break;
            }
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
            Entities.AddEntity(FactoryPlayer.CreatePlayer(currentGame));
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