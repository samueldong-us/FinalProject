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
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class ScreenGame : ScreenPixelatedTransition
    {
        public static Rectangle Bounds = new Rectangle(420, 0, 1080, 1050);

        public static SystemCollisions Collisions;
        public static SystemDrawing Drawing;

        public static MessageCenter MessageCenter;

        private Texture2D background;

        private SaveGame currentGame;

        private List<Entity> entities;

        private ItemGroupMenu menuItems;

        private bool paused;

        private Random rng = new Random();

        private List<Entity> toRemove;

        private WaveManager waveManager;

        public ScreenGame(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            InitializeSystems();
            InitializeMessageCenter();
            InitializeMenu();
            entities = new List<Entity>();
            toRemove = new List<Entity>();
            GameMain.MessageCenter.AddListener<SaveGame, string>("Save Game and Stage Pass to Game", SetCurrentGameAndStage);
        }

        public override void KeyPressed(Keys key)
        {
            MessageCenter.Broadcast<Keys>("Key Pressed", key);
            switch (state)
            {
                case ScreenState.Active:
                    {
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
                    } break;
            }
        }

        public override void KeyReleased(Keys key)
        {
            MessageCenter.Broadcast<Keys>("Key Released", key);
        }

        public override void LoadContent()
        {
            background = content.Load<Texture2D>("MenuBackground");
            GameAssets.LoadContent(content);
            base.LoadContent();
        }

        public override void Start()
        {
            List<Wave> waves = new List<Wave>();
            SpawnInformation test1 = new SpawnInformation(0);
            test1.AddInformation("Unit Type", "Jellyfish");
            test1.AddInformation("Spawn Position", new Vector2(500, -200));
            test1.AddInformation("Shoot Position", new Vector2(700, 200));
            SpawnInformation test2 = new SpawnInformation(2);
            test2.AddInformation("Unit Type", "Walking Fish01");
            test2.AddInformation("Spawn Position", new Vector2(1420, -200));
            test2.AddInformation("Shoot Position", new Vector2(1220, 200));
            for (int i = 0; i < 4; i++)
            {
                Wave wave = new Wave();
                wave.AddSpawnInformation(test1);
                wave.AddSpawnInformation(test2);
                waves.Add(wave);
            }
            waveManager = new WaveManager(waves);
            entities.Add(UnitFactory.CreatePlayer(currentGame));
            base.Start();
        }

        protected override void ActiveUpdate(float secondsPassed)
        {
            if (!paused)
            {
                waveManager.Update(secondsPassed);
                List<Entity> toSpawn = waveManager.GetEntitiesToSpawn();
                if (toSpawn != null)
                {
                    foreach (Entity entity in toSpawn)
                    {
                        entities.Add(entity);
                    }
                }
                Collisions.Update();
                foreach (Entity entity in toRemove)
                {
                    entities.Remove(entity);
                    entity.Dispose();
                }
                toRemove.Clear();
                foreach (Entity entity in entities)
                {
                    entity.MessageCenter.Broadcast("Clean Up");
                }
                foreach (Entity entity in entities)
                {
                    entity.Update(secondsPassed);
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
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            Drawing.Draw(spriteBatch);
            if (paused)
            {
                menuItems.Draw(spriteBatch);
            }
        }

        protected override void Reset()
        {
            foreach (Entity entity in entities)
            {
                entity.Dispose();
            }
            entities.Clear();
            Collisions.Dispose(); Drawing.Dispose();
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
            MessageCenter.AddListener<Entity>("Remove Entity", RemoveEntity);
            MessageCenter.AddListener<Entity>("Find Closest Player", Collisions.ClosestPlayer);
        }

        private void InitializeSystems()
        {
            Collisions = new SystemCollisions();
            Drawing = new SystemDrawing();
        }

        private void RemoveEntity(Entity entity)
        {
            toRemove.Add(entity);
        }

        private void SetCurrentGameAndStage(SaveGame saveGame, string stage)
        {
            currentGame = saveGame;
            UnitFactory.Difficulty = currentGame.difficulty;
            switch (stage)
            {
                case "LEVEL 1": { UnitFactory.Stage = 1; } break;
                case "LEVEL 2": { UnitFactory.Stage = 2; } break;
                case "LEVEL 3": { UnitFactory.Stage = 3; } break;
            }
        }
    }
}