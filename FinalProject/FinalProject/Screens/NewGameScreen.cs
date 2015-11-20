using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class NewGameScreen : PixelatedTransitionScreen
    {
        private enum Error { None, Exists, Empty }

        private enum Result { Back, Continue }

        private Texture2D background;
        private SaveGame currentGame;
        private Error lastError;
        private ItemGroupMenu menuItems;
        private Result result;
        private ItemMenu userGameName;

        public NewGameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            menuItems = new ItemGroupMenu();
            userGameName = new ItemMenu(new Vector2(280, 320), "");
            menuItems.AddItem(userGameName);
            GameMain.MessageCenter.AddListener<SaveGame>("Save Game Pass to New Game", saveGame => currentGame = saveGame);
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
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                } break;
                            case Keys.Escape:
                                {
                                    result = Result.Back;
                                    BeginTransitioningOut();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
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
            base.BeginTransitioningOut();
        }

        protected override void DrawScreen(SpriteBatch spriteBatch)
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

        protected override void Reset()
        {
            currentGame = null;
            userGameName.Text = "";
            base.Reset();
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

        private void RemoveCharacter()
        {
            if (userGameName.Text.Length > 0)
            {
                userGameName.Text = userGameName.Text.Substring(0, userGameName.Text.Length - 1);
            }
        }
    }
}