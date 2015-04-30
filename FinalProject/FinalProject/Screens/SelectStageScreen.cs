using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject.Screens
{
    internal class SelectStageScreen : PixelateScreen
    {
        private enum Result { Back, Continue }

        private Texture2D background;

        private SaveGame currentGame;

        private MenuItemGroup menuItems;

        private Result result;

        public SelectStageScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            menuItems = new MenuItemGroup();
            GameMain.MessageCenter.AddListener<SaveGame>("Save Game Pass to Select Stage", saveGame => currentGame = saveGame);
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
            base.BeginTransitioningOut();
        }

        protected override void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(320, 210), "STAGE SELECT");
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