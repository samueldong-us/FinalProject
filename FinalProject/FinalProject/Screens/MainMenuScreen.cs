using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class MainMenuScreen : Screen
    {
        private Texture2D background;

        private MenuItemGroup menuItems;

        private InterpolatedValue scaleIn, scaleOut;

        public MainMenuScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            menuItems = new MenuItemGroup();
            InitializeMenu();
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
            InitializeMenu();
            base.Start();
        }

        protected override void BeginTransitioningOut()
        {
            switch (menuItems.GetSelected())
            {
                case "NEW GAME":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "New Game");
                    } break;
                case "LOAD GAME":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Load Game");
                    } break;
                case "QUIT GAME":
                    {
                        otherScreenReady = true;
                    } break;
            }
            base.BeginTransitioningOut();
        }

        protected override void Reset()
        {
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
            switch (menuItems.GetSelected())
            {
                case "NEW GAME":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "New Game");
                    } break;
                case "LOAD GAME":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Load Game");
                    } break;
                case "QUIT GAME":
                    {
                        GameMain.MessageCenter.Broadcast("Quit");
                    } break;
            }
        }

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
        }

        private void InitializeMenu()
        {
            menuItems.AddItem(new MenuItem(new Vector2(280, 160), "NEW GAME"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LOAD GAME"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 480), "SETTINGS") { Disabled = true });
            menuItems.AddItem(new MenuItem(new Vector2(280, 640), "CREDITS") { Disabled = true });
            menuItems.AddItem(new MenuItem(new Vector2(280, 800), "QUIT GAME"));
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishTransitioningOut();
        }
    }
}