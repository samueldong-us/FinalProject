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
    internal class GameScreen : PixelateScreen
    {
        public static Rectangle Bounds = new Rectangle(420, 0, 1080, 1050);

        public static List<ColliderComponent> CollidersEnemies;

        public static List<ColliderComponent> CollidersEnemyBullets;

        public static List<ColliderComponent> CollidersPlayer;

        public static List<ColliderComponent> CollidersPlayerBullets;

        public static List<Drawable> LayerDebug;

        public static List<Drawable> LayerEnemies;

        public static List<Drawable> LayerEnemyBullets;

        public static List<Drawable> LayerHealthBars;

        public static List<Drawable> LayerPlayer;

        public static List<Drawable> LayerPlayerBullets;

        public static MessageCenter MessageCenter;

        private Texture2D background;

        private SaveGame currentGame;

        private List<Entity> entities;

        private MenuItemGroup menuItems;

        private bool paused;

        private Random rng = new Random();

        private List<Entity> toRemove;

        private WaveManager waveManager;

        public GameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            InitializeMessageCenter();
            InitializeMenu();
            InitializeStaticVariables();
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
            test2.AddInformation("Unit Type", "Jellyfish");
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
            Entity ship = new Entity();
            ship.Position = new Vector2(700, 700);
            ship.Rotation = -(float)(Math.PI / 2);
            new PlayerControllerComponent(ship, 200);
            /*
            new ConstantRateFireComponent(ship, 0.1f);
            new SpreadShotProjectileWeaponComponent(ship, 1, (float)(-Math.PI / 2), new Vector2(0, -50));
            new SpreadShotProjectileWeaponComponent(ship, 1, (float)(-Math.PI / 2 - Math.PI / 16), new Vector2(-5, -50));
            new SpreadShotProjectileWeaponComponent(ship, 1, (float)(-Math.PI / 2 + Math.PI / 16), new Vector2(5, -50));
             */
            new VelocityAccelerationComponent(ship, Vector2.Zero, Vector2.Zero);
            new RestrictPositionComponent(ship, 50, 50, Bounds);
            new LaserWeaponComponent(ship, 10);
            new ColliderComponent(ship, GameAssets.Unit["Laser Ship"], GameAssets.UnitTriangles["Laser Ship"], CollidersPlayer).DebugDraw();
            new HealthComponent(ship, 20);
            new CircularHealthBarComponent(ship, (float)(Math.PI * 4 / 5));
            new RemoveOnDeathComponent(ship);
            new TextureRendererComponent(ship, GameAssets.UnitTexture, GameAssets.Unit["Laser Ship"], Color.White, LayerPlayer);
            entities.Add(ship);
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
                CheckForCollisions();
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
            foreach (Drawable drawable in LayerPlayerBullets)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in LayerPlayer)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in LayerHealthBars)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in LayerEnemyBullets)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in LayerEnemies)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in LayerDebug)
            {
                drawable.Draw(spriteBatch);
            }
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
            CollidersEnemies.Clear();
            CollidersEnemyBullets.Clear();
            CollidersPlayer.Clear();
            CollidersPlayerBullets.Clear();
            LayerDebug.Clear();
            LayerEnemies.Clear();
            LayerEnemyBullets.Clear();
            LayerHealthBars.Clear();
            LayerPlayer.Clear();
            LayerPlayerBullets.Clear();
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Stage");
        }

        private static void InitializeStaticVariables()
        {
            CollidersEnemies = new List<ColliderComponent>();
            CollidersEnemyBullets = new List<ColliderComponent>();
            CollidersPlayer = new List<ColliderComponent>();
            CollidersPlayerBullets = new List<ColliderComponent>();
            LayerDebug = new List<Drawable>();
            LayerEnemies = new List<Drawable>();
            LayerEnemyBullets = new List<Drawable>();
            LayerHealthBars = new List<Drawable>();
            LayerPlayer = new List<Drawable>();
            LayerPlayerBullets = new List<Drawable>();
        }

        private void CheckForCollisions()
        {
            foreach (ColliderComponent player in CollidersPlayer)
            {
                foreach (ColliderComponent enemyBullet in CollidersEnemyBullets)
                {
                    if (enemyBullet.CollidesWith(player))
                    {
                        enemyBullet.NotifyOfCollision(player.GetEntity());
                        player.NotifyOfCollision(enemyBullet.GetEntity());
                    }
                }
            }
            foreach (ColliderComponent player in CollidersPlayer)
            {
                foreach (ColliderComponent enemy in CollidersEnemies)
                {
                    if (player.CollidesWith(enemy))
                    {
                        enemy.NotifyOfCollision(player.GetEntity());
                        player.NotifyOfCollision(enemy.GetEntity());
                    }
                }
            }
            foreach (ColliderComponent enemy in CollidersEnemies)
            {
                foreach (ColliderComponent playerBullet in CollidersPlayerBullets)
                {
                    if (playerBullet.CollidesWith(enemy))
                    {
                        playerBullet.NotifyOfCollision(enemy.GetEntity());
                        enemy.NotifyOfCollision(playerBullet.GetEntity());
                    }
                }
            }
        }

        private Vector2 ClosestCollider(Entity entity, List<ColliderComponent> colliderList, float maxAngle)
        {
            ColliderComponent closest = null;
            Vector2 closestFromTo = Vector2.Zero;
            Vector2 entityVector = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(entity.Rotation));
            foreach (ColliderComponent collider in colliderList)
            {
                Vector2 fromTo = collider.GetEntity().Position - entity.Position;
                if (MathUtilities.AngleBetween(entityVector, fromTo) < maxAngle)
                {
                    if (closest == null || fromTo.LengthSquared() < closestFromTo.LengthSquared())
                    {
                        closest = collider;
                        closestFromTo = fromTo;
                    }
                }
            }
            return closest == null ? new Vector2(-1, -1) : closest.GetEntity().Position;
        }

        private void ClosestPlayer(Entity parameterOne)
        {
            parameterOne.MessageCenter.Broadcast<Vector2>("Closest Player", ClosestCollider(parameterOne, CollidersPlayer, (float)Math.PI));
        }

        private void InitializeMenu()
        {
            menuItems = new MenuItemGroup();
            menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LEVEL SELECT"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 450), "UPGRADES"));
        }

        private void InitializeMessageCenter()
        {
            MessageCenter = new MessageCenter();
            MessageCenter.AddListener<Entity>("Remove Entity", RemoveEntity);
            MessageCenter.AddListener<Entity>("Find Closest Player", ClosestPlayer);
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