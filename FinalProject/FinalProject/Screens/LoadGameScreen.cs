using FinalProject.GameResources;
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
        public ScreenEvent FinishedTransitioningOut;
        public ScreenEvent StartingTransitioningOut;
        private Texture2D background;
        private int currentPage;
        private List<MenuItemGroup> savedGames;
        private InterpolatedValue scaleIn, scaleOut;
        private string selected;

        public LoadGameScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            savedGames = new List<MenuItemGroup>();
            currentPage = -1;
            selected = "";
        }

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
                                        selected = savedGames[currentPage].GetSelected();
                                        StartingTransitioningOut(selected);
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
                                    StartingTransitioningOut("");
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

        public override void Reset()
        {
            savedGames.Clear();
            currentPage = -1;
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            selected = "";
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
            FinishedTransitioningOut(selected);
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