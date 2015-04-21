﻿using FinalProject.GameComponents;
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
        public static Rectangle Bounds = new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight);
        public static List<Drawable> BulletLayer;
        public static List<ColliderComponent> EnemyBulletColliders;
        public static List<ColliderComponent> EnemyColliders;
        public static MessageCenter GameMessageCenter;
        public static List<Drawable> HealthLayer;
        public static List<Drawable> NormalLayer;
        public static List<ColliderComponent> PlayerBulletColliders;
        public static List<ColliderComponent> PlayerCollider;
        public SaveGame currentGame;
        private Texture2D background;
        private Texture2D bullet;
        private List<Entity> entities;
        private MenuItemGroup menuItems;
        private bool paused;
        private InterpolatedValue scaleIn, scaleOut;
        private string selected;
        private Texture2D test;
        private Texture2D testHealth;
        private List<Entity> toRemove;

        public GameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            GameMessageCenter = new MessageCenter();
            GameMessageCenter.AddListener<Entity>("Remove Entity", RemoveEntity);
            GameMessageCenter.AddListener<Vector2, MessageCenter>("Get Closest Enemy", ClosestEnemy);
            GameMessageCenter.AddListener<Vector2, MessageCenter>("Get Closest Player", ClosestPlayer);
            toRemove = new List<Entity>();
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            menuItems = new MenuItemGroup();
            menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LEVEL SELECT"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 450), "UPGRADES"));
            NormalLayer = new List<Drawable>();
            BulletLayer = new List<Drawable>();
            HealthLayer = new List<Drawable>();
            PlayerCollider = new List<ColliderComponent>();
            EnemyColliders = new List<ColliderComponent>();
            EnemyBulletColliders = new List<ColliderComponent>();
            PlayerBulletColliders = new List<ColliderComponent>();
            entities = new List<Entity>();
            selected = "";
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
            GameMessageCenter.Broadcast<Keys>("Key Pressed", key);
            switch (state)
            {
                case ScreenState.Active:
                    {
                        switch (key)
                        {
                            case Keys.Enter:
                                {
                                    selected = menuItems.GetSelected();
                                    RequestingToTransitionOut(selected);
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
                                    RequestingToTransitionOut("");
                                } break;
                        }
                    } break;
            }
        }

        public override void KeyReleased(Keys key)
        {
            GameMessageCenter.Broadcast<Keys>("Key Released", key);
        }

        public override void LoadContent()
        {
            test = content.Load<Texture2D>("Test");
            background = content.Load<Texture2D>("MenuBackground");
            bullet = content.Load<Texture2D>("TestBullet");
            testHealth = content.Load<Texture2D>("CircularHealthBarTest");
        }

        public override void Start()
        {
            entities.Add(GeneratePlayer());
            for (int i = 0; i < 10; i++)
            {
                entities.Add(TestGenerateEnemy());
            }
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TransitionOut()
        {
            base.TransitionOut();
        }

        protected override void BeginTransitioningOut()
        {
            throw new NotImplementedException();
        }

        protected override void FinishTransitioningOut()
        {
            throw new NotImplementedException();
        }

        protected override void Reset()
        {
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            selected = "";
            entities.Clear();
            NormalLayer.Clear();
            BulletLayer.Clear();
            HealthLayer.Clear();
            EnemyColliders.Clear();
            PlayerCollider.Clear();
            PlayerBulletColliders.Clear();
            EnemyBulletColliders.Clear();
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

        private void CheckForCollisions()
        {
            foreach (ColliderComponent player in PlayerCollider)
            {
                foreach (ColliderComponent enemyBullet in EnemyBulletColliders)
                {
                    if (enemyBullet.CollidesWith(player))
                    {
                        enemyBullet.NotifyOfCollision(player.Entity);
                        player.NotifyOfCollision(enemyBullet.Entity);
                    }
                }
            }
            foreach (ColliderComponent player in PlayerCollider)
            {
                foreach (ColliderComponent enemy in EnemyColliders)
                {
                    if (player.CollidesWith(enemy))
                    {
                        enemy.NotifyOfCollision(player.Entity);
                        player.NotifyOfCollision(enemy.Entity);
                    }
                }
            }
            foreach (ColliderComponent enemy in EnemyColliders)
            {
                foreach (ColliderComponent playerBullet in PlayerBulletColliders)
                {
                    if (playerBullet.CollidesWith(enemy))
                    {
                        playerBullet.NotifyOfCollision(enemy.Entity);
                        enemy.NotifyOfCollision(playerBullet.Entity);
                    }
                }
            }
        }

        private Vector2 ClosestCollider(Vector2 position, List<ColliderComponent> colliders)
        {
            if (colliders.Count == 0)
            {
                return new Vector2(-1, -1);
            }
            else
            {
                ColliderComponent closest = colliders[0];
                foreach (ColliderComponent collider in colliders)
                {
                    if (Vector2.DistanceSquared(collider.transform.Position, position) < Vector2.DistanceSquared(closest.transform.Position, position))
                    {
                        closest = collider;
                    }
                }
                return closest.transform.Position;
            }
        }

        private void ClosestEnemy(Vector2 position, MessageCenter messageCenter)
        {
            messageCenter.Broadcast<Vector2>("Closest Position", ClosestCollider(position, EnemyColliders));
        }

        private void ClosestPlayer(Vector2 position, MessageCenter messageCenter)
        {
            messageCenter.Broadcast<Vector2>("Closest Position", ClosestCollider(position, PlayerCollider));
        }

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);

            foreach (Drawable drawable in NormalLayer)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in HealthLayer)
            {
                drawable.Draw(spriteBatch);
            }
            foreach (Drawable drawable in BulletLayer)
            {
                drawable.Draw(spriteBatch);
            }
            if (paused)
            {
                menuItems.Draw(spriteBatch);
            }
        }

        private Entity GeneratePlayer()
        {
            Entity player = new Entity();
            PlayerInputComponent playerInput = new PlayerInputComponent(player.MessageCenter);
            BoundedTransformComponent playerTransform = new BoundedTransformComponent(player.MessageCenter, 110, 140, Bounds);
            playerTransform.Position = new Vector2(500, 500);
            HomingWeaponComponent playerWeapon = new HomingWeaponComponent(player.MessageCenter, playerTransform, bullet, 0.1f, new Vector2(0, -1000), new Vector2(-50, -50), (float)(Math.PI * 4));
            HomingWeaponComponent playerWeapon2 = new HomingWeaponComponent(player.MessageCenter, playerTransform, bullet, 0.1f, new Vector2(0, -1000), new Vector2(50, -50), (float)(Math.PI * 4));
            RenderComponent playerRender = new RenderComponent(player.MessageCenter, test, playerTransform, GameScreen.NormalLayer);
            player.AddComponent(playerWeapon);
            player.AddComponent(playerWeapon2);
            player.AddComponent(playerInput);
            player.AddComponent(playerTransform);
            player.AddComponent(playerRender);
            return player;
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
            FinishedTransitioningOut(selected);
        }

        private Entity TestGenerateEnemy()
        {
            Random rng = new Random();
            Entity enemy = new Entity();
            BoundedTransformComponent enemyTransform = new BoundedTransformComponent(enemy.MessageCenter, 110, 140, Bounds);
            enemyTransform.Position = new Vector2(rng.Next(200, 1720), rng.Next(200, 880));
            enemyTransform.Theta = rng.Next(360);
            enemyTransform.Scale = .5f;
            HealthComponent enemyHealth = new HealthComponent(enemy.MessageCenter, 50);
            TestCircularHealthBar enemyHealthBar = new TestCircularHealthBar(enemy.MessageCenter, 100, 15, new Vector2(0, -150), enemyHealth, enemyTransform, HealthLayer, testHealth);
            RemoveOnDeathComponent enemyRemoveOnDeath = new RemoveOnDeathComponent(enemy);
            ColliderComponent enemyCollider = new ColliderComponent(enemy, 150, GraphicsUtilities.GetColorsFromTexture(test), enemyTransform, EnemyColliders);
            RenderComponent enemyRender = new RenderComponent(enemy.MessageCenter, test, enemyTransform, GameScreen.NormalLayer);
            enemy.AddComponent(enemyHealth);
            enemy.AddComponent(enemyHealthBar);
            enemy.AddComponent(enemyRemoveOnDeath);
            enemy.AddComponent(enemyCollider);
            enemy.AddComponent(enemyTransform);
            enemy.AddComponent(enemyRender);
            return enemy;
        }
    }
}