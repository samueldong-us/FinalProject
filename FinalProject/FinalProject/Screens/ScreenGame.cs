using FinalProject.GameComponents;
using FinalProject.GameSaving;
using FinalProject.GameWaves;
using FinalProject.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class ScreenGame : ScreenPixelatedTransition
    {
        public static Rectangle Bounds = new Rectangle(420, 0, 1080, 1050);

        public static SystemCollisions Collisions;
        public static SystemDrawing Drawing;
        public static SystemEntity Entities;

        public static MessageCenter MessageCenter;

        private Texture2D background;

        private SaveGame currentGame;

        private ItemGroupMenu menuItems;

        private bool paused;

        private Random rng = new Random();

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
                MessageCenter.Broadcast<Keys>("Key Pressed", key);
                switch (key)
                {
                    case Keys.Enter:
                        {
                        } break;
                    case Keys.Up:
                        {
                            menuItems.MoveUp();
                        } break;
                    case Keys.Down:
                        {
                            menuItems.MoveDown();
                        } break;
                    case Keys.Escape:
                        {
                            BeginTransitioningOut();
                        } break;
                }
            }
        }

        public override void KeyReleased(Keys key)
        {
            if (state == ScreenState.Active)
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
            TestSetup();
            base.Start();
        }

        protected override void ActiveUpdate(float secondsPassed)
        {
            if (!paused)
            {
                waveManager.Update(secondsPassed);
                Collisions.Update();
                Entities.Update(secondsPassed);
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
            Drawing.Draw(spriteBatch);
            if (paused)
            {
                menuItems.Draw(spriteBatch);
            }
        }

        protected override void Reset()
        {
            Entities.Dispose();
            Collisions.Dispose();
            Drawing.Dispose();
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Stage");
        }

        private void InitializeMenu()
        {
            menuItems = new ItemGroupMenu();
            menuItems.AddItem(new ItemMenu(new Vector2(280, 320), "LEVEL SELECT"));
            menuItems.AddItem(new ItemMenu(new Vector2(280, 450), "UPGRADES"));
        }

        private void InitializeMessageCenter()
        {
            MessageCenter = new MessageCenter();
            MessageCenter.AddListener<Entity>("Find Closest Player", Collisions.ClosestPlayer);
        }

        private void InitializeSystems()
        {
            Collisions = new SystemCollisions();
            Drawing = new SystemDrawing();
            Entities = new SystemEntity();
        }

        private void SetCurrentGameAndStage(SaveGame saveGame, string stage)
        {
            currentGame = saveGame;
            FactoryUnit.Difficulty = currentGame.difficulty;
            switch (stage)
            {
                case "LEVEL 1": { FactoryUnit.Stage = 1; } break;
                case "LEVEL 2": { FactoryUnit.Stage = 2; } break;
                case "LEVEL 3": { FactoryUnit.Stage = 3; } break;
            }
        }

        private void TestSetup()
        {
            List<Wave> waves = new List<Wave>();
            SpawnInformation test2 = new SpawnInformation(2);
            test2.AddInformation("Unit Name", "Walking Fish01");
            test2.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            test2.AddInformation("Rotate Based On Velocity", false);
            test2.AddInformation("Behavior Name", "Catmull Rom");
            test2.AddInformation("Weapon Name", "Circular Fire");
            List<Vector2> path = new List<Vector2>();
            path.Add(new Vector2(320, 200));
            path.Add(new Vector2(1010, 400));
            path.Add(new Vector2(1010, 600));
            path.Add(new Vector2(910, 600));
            path.Add(new Vector2(910, 400));
            path.Add(new Vector2(1600, 200));
            test2.AddInformation("Path", path);
            test2.AddInformation("Start Firing Percentage", 0.1f);
            test2.AddInformation("Stop Firing Percentage", 0.9f);
            for (int i = 0; i < 4; i++)
            {
                Wave wave = new Wave();
                wave.AddSpawnInformation(test2);
                waves.Add(wave);
            }
            waveManager = new SystemWaves(waves);
            Entities.AddEntity(FactoryPlayer.CreatePlayer(currentGame));
        }
    }
}