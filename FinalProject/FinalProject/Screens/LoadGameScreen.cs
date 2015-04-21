using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class LoadGameScreen : Screen
    {
        private Texture2D background;
        private int currentPage;
        private Result result;
        private List<MenuItemGroup> savedGames;
        private InterpolatedValue scaleIn, scaleOut;

        public LoadGameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            savedGames = new List<MenuItemGroup>();
            currentPage = -1;
        }

        private enum Result { Back, Continue }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        GraphicsUtilities.BeginDrawingPixelated(spriteBatch, Vector2.Zero, Constants.VirtualWidth, Constants.VirtualHeight, scaleIn.GetValue(), graphicsDevice);
                        DrawScreen(spriteBatch);
                        GraphicsUtilities.EndDrawingPixelated(spriteBatch, Constants.VirtualWidth, Constants.VirtualHeight, Vector2.Zero, scaleIn.GetValue(), graphicsDevice);
                    } break;
                case ScreenState.Active:
                    {
                        DrawScreen(spriteBatch);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        GraphicsUtilities.BeginDrawingPixelated(spriteBatch, Vector2.Zero, Constants.VirtualWidth, Constants.VirtualHeight, scaleOut.GetValue(), graphicsDevice);
                        DrawScreen(spriteBatch);
                        GraphicsUtilities.EndDrawingPixelated(spriteBatch, Constants.VirtualWidth, Constants.VirtualHeight, Vector2.Zero, scaleOut.GetValue(), graphicsDevice);
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
        }

        protected override void BeginTransitioningOut()
        {
            switch (result)
            {
                case Result.Continue:
                    {
                        SaveGame currentGame = SaveGameManager.GetSavedGame(savedGames[currentPage].GetSelected() + ".sav");
                        GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Command Center", currentGame);
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Command Center");
                    } break;
                case Result.Back:
                    {
                        GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Main Menu");
                    } break;
            }
            TransitionOut();
        }

        protected override void FinishTransitioningOut()
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

        protected override void Reset()
        {
            savedGames.Clear();
            currentPage = -1;
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
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

        private void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.VirtualWidth, Constants.VirtualHeight), Color.White);
            if (currentPage != -1)
            {
                savedGames[currentPage].Draw(spriteBatch);
            }
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitle, "SELECT PROFILE", new Vector2(380, 210), Fonts.Green);
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishTransitioningOut();
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