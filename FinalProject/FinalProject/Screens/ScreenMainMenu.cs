using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class ScreenMainMenu : ScreenPixelatedTransition
    {
        private Texture2D background;

        private ItemGroupMenu menuItems;

        public ScreenMainMenu(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            menuItems = new ItemGroupMenu();
            InitializeMenu();
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
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                } break;
                            case Keys.Up:
                                {
                                    menuItems.MoveUp();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                } break;
                            case Keys.Down:
                                {
                                    menuItems.MoveDown();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
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
                case "SETTINGS":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Settings");
                    } break;
                case "CREDITS":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Show Credits");
                    } break;
            }
            base.BeginTransitioningOut();
        }

        protected override void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
        }

        protected override void Reset()
        {
            menuItems.Reset();
            base.Reset();
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
                case "SETTINGS":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Settings");
                    } break;
                case "CREDITS":
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Show Credits");
                    } break;
            }
        }

        private void InitializeMenu()
        {
            menuItems.AddItem(new ItemMenu(new Vector2(280, 160), "NEW GAME"));
            menuItems.AddItem(new ItemMenu(new Vector2(280, 320), "LOAD GAME"));
            menuItems.AddItem(new ItemMenu(new Vector2(280, 480), "SETTINGS"));
            menuItems.AddItem(new ItemMenu(new Vector2(280, 640), "CREDITS"));
            menuItems.AddItem(new ItemMenu(new Vector2(280, 800), "QUIT GAME"));
        }
    }
}