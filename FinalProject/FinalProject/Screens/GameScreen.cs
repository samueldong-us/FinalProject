using FinalProject.GameComponents;
using FinalProject.GameSaving;
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
    internal class GameScreen : Screen
    {
        public static Rectangle Bounds = new Rectangle(420, 0, 1080, Constants.VirtualHeight);
        public static List<ColliderComponent> CollidersEnemies;
        public static List<ColliderComponent> CollidersEnemyBullets;
        public static List<ColliderComponent> CollidersPlayer;
        public static List<ColliderComponent> CollidersPlayerBullets;
        public static List<Drawable> LayerBullets;
        public static List<Drawable> LayerDebug;
        public static List<Drawable> LayerEnemies;
        public static List<Drawable> LayerHealthBars;
        public static List<Drawable> LayerPlayer;
        public static MessageCenter MessageCenter;
        private Texture2D background;
        private Texture2D bullet;
        private SaveGame currentGame;
        private List<Entity> entities;
        private MenuItemGroup menuItems;
        private bool paused;
        private Random rng = new Random();
        private InterpolatedValue scaleIn, scaleOut;
        private Texture2D test;
        private Entity testEntity;
        private Texture2D testHealth;
        private List<Entity> toRemove;

        public GameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            InitializeMessageCenter();
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            InitializeMenu();
            InitializeStaticVariables();
            entities = new List<Entity>();
            toRemove = new List<Entity>();
            GameMain.MessageCenter.AddListener<SaveGame, string>("Save Game and Stage Pass to Game", SetCurrentGameAndStage);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        GraphicsUtilities.BeginDrawingPixelated(spriteBatch, Vector2.Zero, Constants.VirtualWidth, Constants.VirtualHeight, scaleIn.GetValue(), graphicsDevice);
                        DrawScreen(spriteBatch);
                        GraphicsUtilities.EndDrawingPixelated(spriteBatch, Constants.VirtualWidth, Constants.VirtualHeight, Vector2.Zero, scaleIn.GetValue(), graphicsDevice);
                    } break;
                case ScreenState.Active:
                    {
                        DrawScreen(spriteBatch);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        GraphicsUtilities.BeginDrawingPixelated(spriteBatch, Vector2.Zero, Constants.VirtualWidth, Constants.VirtualHeight, scaleOut.GetValue(), graphicsDevice);
                        DrawScreen(spriteBatch);
                        GraphicsUtilities.EndDrawingPixelated(spriteBatch, Constants.VirtualWidth, Constants.VirtualHeight, Vector2.Zero, scaleOut.GetValue(), graphicsDevice);
                    } break;
            }
        }

        public override void KeyPressed(Keys key)
        {
            MessageCenter.Broadcast<Keys>("Key Pressed", key);
            testEntity.MessageCenter.Broadcast("Fire");
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
            test = content.Load<Texture2D>("ship01");
            background = content.Load<Texture2D>("MenuBackground");
            bullet = content.Load<Texture2D>("TestBulletTri");
            testHealth = content.Load<Texture2D>("CircularHealthBarTest");
            GameAssets.LoadContent(content);
        }

        public override void Start()
        {
            testEntity = new Entity();
            testEntity.Position = new Vector2(500, 500);
            new ExampleProjectileWeaponComponent(testEntity, Vector2.Zero);
            entities.Add(testEntity);
            Entity ship = new Entity();
            ship.Position = new Vector2(700, 700);
            ship.Rotation = -(float)(Math.PI / 2);
            new ColliderComponent(ship, GameAssets.Unit["Spread Shot Ship"], GameAssets.UnitTriangles["Spread Shot Ship"], CollidersEnemies).DebugDraw();
            new TextureRendererComponent(ship, GameAssets.UnitTexture, GameAssets.Unit["Spread Shot Ship"], Color.White, LayerPlayer);
            entities.Add(ship);
            base.Start();
        }

        protected override void BeginTransitioningOut()
        {
            GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Select Stage", currentGame);
            GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Select Stage");
            TransitionOut();
        }

        protected override void FinishTransitioningOut()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Stage");
        }

        protected override void Reset()
        {
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            entities.Clear();
            LayerPlayer.Clear();
            LayerBullets.Clear();
            LayerHealthBars.Clear();
            CollidersEnemies.Clear();
            CollidersPlayer.Clear();
            CollidersPlayerBullets.Clear();
            CollidersEnemyBullets.Clear();
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        scaleIn.Update(secondsPassed);
                    } break;
                case ScreenState.Active:
                    {
                        if (!paused)
                        {
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
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        scaleOut.Update(secondsPassed);
                    } break;
            }
        }

        private static void InitializeStaticVariables()
        {
            LayerPlayer = new List<Drawable>();
            LayerEnemies = new List<Drawable>();
            LayerBullets = new List<Drawable>();
            LayerHealthBars = new List<Drawable>();
            LayerDebug = new List<Drawable>();
            CollidersPlayer = new List<ColliderComponent>();
            CollidersEnemies = new List<ColliderComponent>();
            CollidersEnemyBullets = new List<ColliderComponent>();
            CollidersPlayerBullets = new List<ColliderComponent>();
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

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);

            foreach (Drawable drawable in LayerPlayer)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in LayerHealthBars)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in LayerBullets)
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
        }

        private void RemoveEntity(Entity entity)
        {
            toRemove.Add(entity);
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishTransitioningOut();
        }

        private void SetCurrentGameAndStage(SaveGame saveGame, string stage)
        {
            currentGame = saveGame;
        }
    }
}