using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class NewGameScreen : Screen
    {
        private enum Error { None, Exists, Empty }

        private enum Result { Back, Continue }

        private Texture2D background;

        private SaveGame currentGame;

        private Error lastError;

        private MenuItemGroup menuItems;

        private Result result;

        private InterpolatedValue scaleIn, scaleOut;

        private MenuItem userGameName;

        public NewGameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            menuItems = new MenuItemGroup();
            userGameName = new MenuItem(new Vector2(280, 320), "");
            menuItems.AddItem(userGameName);
            GameMain.MessageCenter.AddListener<SaveGame>("Save Game Pass to New Game", SetCurrentGame);
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
                                    if (SaveGameManager.SaveExists(userGameName.Text))
                                    {
                                        lastError = Error.Exists;
                                    }
                                    else if (userGameName.Text.Trim().Equals(""))
                                    {
                                        lastError = Error.Empty;
                                    }
                                    else
                                    {
                                        lastError = Error.None;
                                        currentGame = new SaveGame();
                                        currentGame.SaveName = userGameName.Text;
                                        result = Result.Continue;
                                        BeginTransitioningOut();
                                    }
                                } break;
                            case Keys.Escape:
                                {
                                    result = Result.Back;
                                    BeginTransitioningOut();
                                } break;
                            case Keys.Space:
                                {
                                    AddCharacter(" ");
                                } break;
                            case Keys.Back:
                                {
                                    RemoveCharacter();
                                } break;
                            default:
                                {
                                    string KeyPress = "" + key;
                                    if (KeyPress.Length == 1 && KeyPress[0] >= 'A' && KeyPress[0] <= 'Z')
                                    {
                                        AddCharacter(KeyPress);
                                    }
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
            if (currentGame != null)
            {
                userGameName.Text = currentGame.SaveName;
            }
            base.Start();
        }

        protected override void BeginTransitioningOut()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Select Character", currentGame);
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Select Character");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Main Menu");
                    } break;
            }
            base.TransitionOut();
        }

        protected override void Reset()
        {
            currentGame = null;
            userGameName.Text = "";
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
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
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Character");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Main Menu");
                    } break;
            }
        }

        private void AddCharacter(string userKeyPress)
        {
            if (Fonts.MenuItemFont.MeasureString(userGameName.Text + userKeyPress).X < 840)
            {
                userGameName.Text += userKeyPress;
            }
        }

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(380, 210), "NAME PROFILE");
            switch (lastError)
            {
                case Error.Empty:
                    {
                        GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeCreditTextFont, Fonts.Red, new Vector2(380, 620), "INVALID NAME");
                    } break;
                case Error.Exists:
                    {
                        GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeCreditTextFont, Fonts.Red, new Vector2(380, 620), "NAME ALREADY FOUND");
                    } break;
            }
        }

        private void RemoveCharacter()
        {
            if (userGameName.Text.Length > 0)
            {
                userGameName.Text = userGameName.Text.Substring(0, userGameName.Text.Length - 1);
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
    }
}