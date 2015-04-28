using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject.Screens
{
    internal class SelectStageScreen : Screen
    {
        private Texture2D background;

        private SaveGame currentGame;

        private MenuItemGroup menuItems;

        private bool otherScreenReady;

        private bool readyToSwitch;

        private Result result;

        private InterpolatedValue scaleIn, scaleOut;

        public SelectStageScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            readyToSwitch = false;
            otherScreenReady = false;
            menuItems = new MenuItemGroup();
            GameMain.MessageCenter.AddListener<SaveGame>("Save Game Pass to Select Stage", SetCurrentGame);
        }

        private enum Result { Back, Continue }

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
                                    result = Result.Continue;
                                    BeginTransitioningOut();
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
                                    result = Result.Back;
                                    BeginTransitioningOut();
                                } break;
                        }
                    } break;
            }
        }

        public override void LoadContent()
        {
            background = content.Load<Texture2D>("MenuBackground");
            base.LoadContent();
        }

        public override void Start()
        {
            if (currentGame == null)
            {
                throw new Exception("A Save Game Must Be Passed In");
            }
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

        protected override void BeginTransitioningOut()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        GameMain.MessageCenter.Broadcast<SaveGame, string>("Save Game and Stage Pass to Game", currentGame, menuItems.GetSelected());
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Game");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Command Center", currentGame);
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Command Center");
                    } break;
            }
            GameMain.MessageCenter.AddListener("Finished Loading", OtherScreenFinishedLoading);
            TransitionOut();
        }

        protected override void FinishedLoading()
        {
            GameMain.MessageCenter.Broadcast("Finished Loading");
        }

        protected override void FinishTransitioningOut()
        {
            if (otherScreenReady)
            {
                SwitchScreens();
            }
            else
            {
                readyToSwitch = true;
            }
        }

        protected override void Reset()
        {
            currentGame = null;
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            menuItems.Reset();
            GameMain.MessageCenter.RemoveListener("Finished Loading", OtherScreenFinishedLoading);
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

        private void OtherScreenFinishedLoading()
        {
            if (readyToSwitch)
            {
                SwitchScreens();
            }
            else
            {
                otherScreenReady = true;
            }
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishTransitioningOut();
        }

        private void SetCurrentGame(SaveGame saveGame)
        {
            currentGame = saveGame;
        }

        private void SwitchScreens()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Game");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Command Center");
                    } break;
            }
        }
    }
}