using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class LoadGameScreen : PixelateScreen
    {
        private enum Result { Back, Continue }

        private Texture2D background;

        private int currentPage;

        private Result result;

        private List<MenuItemGroup> savedGames;

        public LoadGameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            savedGames = new List<MenuItemGroup>();
            currentPage = -1;
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
                                    if (currentPage != -1)
                                    {
                                        result = Result.Continue;
                                        BeginTransitioningOut();
                                    }
                                } break;
                            case Keys.Up:
                                {
                                    if (currentPage != -1)
                                    {
                                        savedGames[currentPage].MoveUp();
                                    }
                                } break;
                            case Keys.Down:
                                {
                                    if (currentPage != -1)
                                    {
                                        savedGames[currentPage].MoveDown();
                                    }
                                } break;
                            case Keys.Left:
                                {
                                    if (currentPage > 0)
                                    {
                                        currentPage--;
                                    }
                                } break;
                            case Keys.Right:
                                {
                                    if (currentPage < savedGames.Count - 1 && currentPage != -1)
                                    {
                                        currentPage++;
                                    }
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
            SetupSavedGames();
            base.LoadContent();
        }

        protected override void BeginTransitioningOut()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        SaveGame currentGame = SaveGameManager.GetSavedGame(savedGames[currentPage].GetSelected());
                        GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Command Center", currentGame);
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Command Center");
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
            if (currentPage != -1)
            {
                savedGames[currentPage].Draw(spriteBatch);
            }
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(380, 210), "SELECT PROFILE");
        }

        protected override void Reset()
        {
            savedGames.Clear();
            currentPage = -1;
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Command Center");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Main Menu");
                    } break;
            }
        }

        private void SetupSavedGames()
        {
            List<string> savedNames = SaveGameManager.GetAllSaves();
            int pages = savedNames.Count / 4;
            if (savedNames.Count % 4 != 0)
            {
                pages++;
            }
            if (pages > 0)
            {
                currentPage = 0;
            }
            for (int i = 0; i < pages; i++)
            {
                savedGames.Add(new MenuItemGroup());
                for (int j = 0; j < 4; j++)
                {
                    if (i * 4 + j < savedNames.Count)
                    {
                        string currentName = savedNames[i * 4 + j];
                        savedGames[i].AddItem(new MenuItem(new Vector2(280, 320 + j * 130), currentName));
                    }
                }
            }
        }
    }
}