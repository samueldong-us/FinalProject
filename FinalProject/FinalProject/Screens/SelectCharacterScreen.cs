using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject.Screens
{
    internal class SelectCharacterScreen : Screen
    {
        private enum Result { Back, Continue }

        private Texture2D background;

        private SaveGame currentGame;

        private MenuItemGroup menuItems;

        private Result result;

        private InterpolatedValue scaleIn, scaleOut;

        public SelectCharacterScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            menuItems = new MenuItemGroup();
            InitializeMenu();
            GameMain.MessageCenter.AddListener<SaveGame>("Save Game Pass to Select Character", SetCurrentGame);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        GraphicsUtilities.BeginDrawingPixelated(spriteBatch, graphicsDevice, scaleIn.GetValue());
                        DrawScreen(spriteBatch);
                        GraphicsUtilities.EndDrawingPixelated(spriteBatch, graphicsDevice, scaleIn.GetValue());
                    } break;
                case ScreenState.Active:
                    {
                        DrawScreen(spriteBatch);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        GraphicsUtilities.BeginDrawingPixelated(spriteBatch, graphicsDevice, scaleOut.GetValue());
                        DrawScreen(spriteBatch);
                        GraphicsUtilities.EndDrawingPixelated(spriteBatch, graphicsDevice, scaleOut.GetValue());
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
                                    switch (menuItems.GetSelected())
                                    {
                                        case "VARLET":
                                            {
                                                currentGame.character = SaveGame.Character.Varlet;
                                            } break;
                                        case "OASON":
                                            {
                                                currentGame.character = SaveGame.Character.Oason;
                                            } break;
                                        case "DIMMY":
                                            {
                                                currentGame.character = SaveGame.Character.Dimmy;
                                            } break;
                                    }
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
            base.Start();
            InitializeMenu();
        }

        protected override void BeginTransitioningOut()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Select Difficulty", currentGame);
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Select Difficulty");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to New Game", currentGame);
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "New Game");
                    } break;
            }
            base.BeginTransitioningOut();
        }

        protected override void Reset()
        {
            currentGame = null;
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            menuItems.Reset();
            base.Reset();
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

        protected override void SwitchScreens()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Difficulty");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "New Game");
                    } break;
            }
        }

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(320, 210), "SELECT CHARACTER");
        }

        private void InitializeMenu()
        {
            menuItems.AddItem(new MenuItem(new Vector2(280, 320), "OASON"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 450), "VARLET"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 580), "DIMMY"));
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
    }
}