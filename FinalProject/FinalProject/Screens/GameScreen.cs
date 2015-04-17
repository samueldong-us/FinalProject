using FinalProject.GameResources;
using FinalProject.Messaging;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Screens
{
    internal class GameScreen : Screen
    {
        public static MessageCenter GameMessageCenter;
        public static List<DrawingComponent> NormalLayer;
        public SaveGame currentGame;
        public ScreenEvent FinishedTransitioningOut;
        public ScreenEvent StartingTransitioningOut;
        private Texture2D background;
        private List<Entity> entities;
        private bool firstIteration;
        private MenuItemGroup menuItems;
        private bool paused;
        private InterpolatedValue scaleIn, scaleOut;
        private string selected;
        private Texture2D snapshot;
        private Texture2D test;

        public GameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            GameMessageCenter = new MessageCenter();
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            menuItems = new MenuItemGroup();
            menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LEVEL SELECT"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 450), "UPGRADES"));
            NormalLayer = new List<DrawingComponent>();
            entities = new List<Entity>();
            selected = "";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        if (firstIteration)
                        {
                            firstIteration = false;
                            GraphicsUtilities.BeginDrawingToTexture(spriteBatch, graphicsDevice);
                            DrawScreen(spriteBatch);
                            GraphicsUtilities.EndDrawingToTexture(spriteBatch, graphicsDevice);
                            snapshot = GraphicsUtilities.DuplicateTexture(GraphicsUtilities.GetTexture(), graphicsDevice);
                        }
                        GraphicsUtilities.DrawPixelatedTexture(spriteBatch, snapshot, Vector2.Zero, scaleIn.GetValue(), graphicsDevice);
                    } break;
                case ScreenState.Active:
                    {
                        GraphicsUtilities.BeginDrawingToTexture(spriteBatch, graphicsDevice);
                        DrawScreen(spriteBatch);
                        GraphicsUtilities.EndDrawingToTexture(spriteBatch, graphicsDevice);
                        spriteBatch.Draw(GraphicsUtilities.GetTexture(), new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        GraphicsUtilities.DrawPixelatedTexture(spriteBatch, snapshot, Vector2.Zero, scaleOut.GetValue(), graphicsDevice);
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
                                    StartingTransitioningOut(selected);
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
                                    StartingTransitioningOut("");
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
            base.LoadContent();
        }

        public override void Reset()
        {
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            selected = "";
            entities.Clear();
            NormalLayer.Clear();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TransitionOut()
        {
            snapshot = GraphicsUtilities.DuplicateTexture(GraphicsUtilities.GetTexture(), graphicsDevice);
            base.TransitionOut();
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

        protected override void Set()
        {
            firstIteration = true;
            entities.Add(GeneratePlayer());
        }

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
            foreach (DrawingComponent drawingComponent in NormalLayer)
            {
                drawingComponent.Draw(spriteBatch);
            }
            if (paused)
            {
                menuItems.Draw(spriteBatch);
            }
        }

        private Entity GeneratePlayer()
        {
            Entity player = new Entity();
            PlayerInputComponent inputComponent = new PlayerInputComponent(player.messageCenter);
            OnScreenTransformComponent transformComponent = new OnScreenTransformComponent(player.messageCenter, 110, 140);
            transformComponent.Position = new Vector2(500, 500);
            DrawingComponent drawingComponent = new DrawingComponent(player.messageCenter, test, transformComponent);
            player.AddComponent(inputComponent);
            player.AddComponent(transformComponent);
            player.AddComponent(drawingComponent);
            return player;
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishedTransitioningOut(selected);
        }
    }
}