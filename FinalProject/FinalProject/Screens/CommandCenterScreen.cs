using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject.Screens
{
    internal class CommandCenterScreen : PixelateScreen
    {
        private enum Result { Back, Continue }

        private Texture2D background;

        private SaveGame currentGame;

        private MenuItemGroup menuItems;

        private Result result;

        public CommandCenterScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            menuItems = new MenuItemGroup();
            InitializeMenu();
            GameMain.MessageCenter.AddListener<SaveGame>("Save Game Pass to Command Center", saveGame => currentGame = saveGame);
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
            InitializeMenu();
            base.Start();
        }

        protected override void BeginTransitioningOut()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        switch (menuItems.GetSelected())
                        {
                            case "LEVEL SELECT":
                                {
                                    GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Select Stage", currentGame);
                                    GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Select Stage");
                                } break;
                            case "UPGRADES":
                                {
                                    GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Upgrade", currentGame);
                                    GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Upgrade");
                                } break;
                        }
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
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(320, 210), "COMMAND CENTER");
        }

        protected override void Reset()
        {
            currentGame = null;
            menuItems.Reset();
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        switch (menuItems.GetSelected())
                        {
                            case "LEVEL SELECT":
                                {
                                    GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Select Stage");
                                } break;
                            case "UPGRADES":
                                {
                                    GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Upgrade");
                                } break;
                        }
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Main Menu");
                    } break;
            }
        }

        private void InitializeMenu()
        {
            menuItems.AddItem(new MenuItem(new Vector2(280, 320), "LEVEL SELECT"));
            menuItems.AddItem(new MenuItem(new Vector2(280, 450), "UPGRADES"));
        }
    }
}