using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class SelectStageScreen : Screen
    {
        public SaveGame currentGame;
        private Texture2D background;
        private MenuItemGroup menuItems;
        private InterpolatedValue scaleIn, scaleOut;
        private string selected;

        public SelectStageScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            menuItems = new MenuItemGroup();
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

        public override void LoadContent()
        {
            background = content.Load<Texture2D>("MenuBackground");
        }

        public override void Start()
        {
            switch (currentGame.HighestUnlockedStage)
            {
                case 1:
                    {
                        menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LEVEL 1"));
                        menuItems.AddItem(new MenuItem(new Vector2(280, 450), "LEVEL 2") { Disabled = true });
                        menuItems.AddItem(new MenuItem(new Vector2(280, 580), "LEVEL 3") { Disabled = true });
                    } break;
                case 2:
                    {
                        menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LEVEL 1"));
                        menuItems.AddItem(new MenuItem(new Vector2(280, 450), "LEVEL 2"));
                        menuItems.AddItem(new MenuItem(new Vector2(280, 580), "LEVEL 3") { Disabled = true });
                    } break;
                case 3:
                    {
                        menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LEVEL 1"));
                        menuItems.AddItem(new MenuItem(new Vector2(280, 450), "LEVEL 2"));
                        menuItems.AddItem(new MenuItem(new Vector2(280, 580), "LEVEL 3"));
                    } break;
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
            throw new System.NotImplementedException();
        }

        protected override void FinishTransitioningOut()
        {
            throw new System.NotImplementedException();
        }

        protected override void Reset()
        {
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            menuItems.Reset();
            selected = "";
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        scaleIn.Update(secondsPassed);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        scaleOut.Update(secondsPassed);
                    } break;
            }
        }

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitle, "STAGE SELECT", new Vector2(320, 210), Fonts.Green);
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