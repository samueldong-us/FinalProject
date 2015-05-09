using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject.Screens
{
    internal class ScreenCommandCenter : ScreenPixelatedTransition
    {
        private enum Result { Back, Continue }

        private const float RotationSpeed = (float)(Math.PI / 8);

        private Texture2D background;

        private SaveGame currentGame;

        private Texture2D displayShips;

        private ItemGroupMenu menuItems;

        private Result result;

        private float rotation;

        public ScreenCommandCenter(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            menuItems = new ItemGroupMenu();
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
                            case Keys.Escape:
                                {
                                    result = Result.Back;
                                    BeginTransitioningOut();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                } break;
                        }
                    } break;
            }
        }

        public override void LoadContent()
        {
            background = content.Load<Texture2D>("MenuBackground");
            displayShips = content.Load<Texture2D>("DisplayShips");
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

        protected override void ActiveUpdate(float secondsPassed)
        {
            rotation += RotationSpeed * secondsPassed;
            if (rotation > Math.PI * 2)
            {
                rotation -= (float)(Math.PI * 2);
            }
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
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(320, 210), "COMMAND CENTER");
            switch (currentGame.character)
            {
                case SaveGame.Character.Varlet:
                    {
                        spriteBatch.Draw(displayShips, new Vector2(1400, 500), new Rectangle(460, 0, 230, 230), Color.White, rotation, new Vector2(115, 115), 1, SpriteEffects.None, 0);
                        UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuItemFont, Fonts.Green, new Vector2(1250, 700), "LASER");
                    } break;
                case SaveGame.Character.Oason:
                    {
                        spriteBatch.Draw(displayShips, new Vector2(1400, 500), new Rectangle(230, 0, 230, 230), Color.White, rotation, new Vector2(115, 115), 1, SpriteEffects.None, 0);
                        UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuItemFont, Fonts.Green, new Vector2(1250, 700), "HOMING");
                    } break;
                case SaveGame.Character.Dimmy:
                    {
                        spriteBatch.Draw(displayShips, new Vector2(1400, 500), new Rectangle(0, 0, 230, 230), Color.White, rotation, new Vector2(115, 115), 1, SpriteEffects.None, 0);
                        UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuItemFont, Fonts.Green, new Vector2(1250, 700), "SPREAD");
                    } break;
            }
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
            menuItems.AddItem(new ItemMenu(new Vector2(280, 320), "LEVEL SELECT"));
            menuItems.AddItem(new ItemMenu(new Vector2(280, 450), "UPGRADES"));
        }
    }
}