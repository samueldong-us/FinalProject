using FinalProject.GameResources;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class NewGameScreen : Screen
    {
        public ScreenEvent FinishedTransitioningOut;
        public ScreenEvent StartingTransitioningOut;
        private Texture2D background;
        private bool firstIteration;
        private MenuItemGroup menuItems;
        private MenuItem userGameName;
        private InterpolatedValue scaleIn, scaleOut;
        private Texture2D snapshot;
        private string selected;
        public SaveGame currentGame;
        private enum Error { None, Exists, Empty}
        private Error lastError;

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
            lastError = Error.None;
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
            switch (state)
            {
                case ScreenState.Active:
                    {
                        switch (key)
                        {
                            case Keys.Enter:
                                {
                                    if(SaveGameManager.SaveExists(userGameName.Text + ".sav")){
                                        lastError = Error.Exists;
                                    }else if(userGameName.Text.Trim().Equals("")){
                                        lastError = Error.Empty;
                                    }else{
                                    currentGame = new SaveGame();
                                    currentGame.SaveName = userGameName.Text;
                                    selected = menuItems.GetSelected();
                                    StartingTransitioningOut(selected);
                                    }
                                } break;
                            case Keys.Up:
                                {
                                    menuItems.MoveUp();
                                } break;
                            case Keys.Down:
                                {
                                    menuItems.MoveDown();
                                } break;
                            case Keys.Space:
                                {
                                    AddCharacterTo(" ");
                                } break;
                            case Keys.Back:
                                {
                                    RemoveCharacterFrom();
                                } break;
                            default:
                                {
                                    string KeyPress = "" + key;
                                    if (KeyPress.Length == 1 && KeyPress[0] >= 'A' && KeyPress[0] <= 'Z')
                                    {
                                        AddCharacterTo(KeyPress);
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

        public override void Reset()
        {
            selected = "";
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
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
                case ScreenState.TransitioningOut:
                    {
                        scaleOut.Update(secondsPassed);
                    } break;
            }
        }

        protected override void Set()
        {
            firstIteration = true;
        }

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitle, "NAME PROFILE", new Vector2(380, 210), Fonts.Green);
            switch (lastError)
            {
                case Error.Empty:
                    {
                        GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeBoldCredits, "INVALID NAME", new Vector2(380, 620), Fonts.Red);
                    }break;
                case Error.Exists:
                    {
                        GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeBoldCredits, "NAME ALREADY FOUND", new Vector2(380, 620), Fonts.Red);
                    }break;
            }
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishedTransitioningOut(selected);
        }

        private void AddCharacterTo(string userKeyPress)
        {
            if (Fonts.MenuItems.MeasureString(userGameName.Text + userKeyPress).X < 840)
            {
                userGameName.Text += userKeyPress;
            }
        }

        private void RemoveCharacterFrom()
        {
            if (userGameName.Text.Length > 0)
            {
                userGameName.Text = userGameName.Text.Substring(0,userGameName.Text.Length-1);
            }
        }
    }
}