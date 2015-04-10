using FinalProject.GameResources;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class UpgradeScreen : Screen
    {
        public SaveGame currentGame;
        public ScreenEvent FinishedTransitioningOut;
        public ScreenEvent StartingTransitioningOut;
        private Texture2D background;
        private bool firstIteration;
        private InterpolatedValue scaleIn, scaleOut;
        private Texture2D snapshot;
        private UpgradeItemGroup upgrades;

        public UpgradeScreen(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new ExponentialInterpolatedValue(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new ExponentialInterpolatedValue(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
            upgrades = new UpgradeItemGroup();
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
                                    int cost = upgrades.GetSelectedCost();
                                    if (cost < currentGame.Credits)
                                    {
                                        currentGame.Credits -= cost;
                                        upgrades.UpgradeSelected();
                                    }
                                } break;
                            case Keys.Up:
                                {
                                    upgrades.MoveUp();
                                } break;
                            case Keys.Down:
                                {
                                    upgrades.MoveDown();
                                } break;
                            case Keys.Escape:
                                {
                                    UpdateUpgrades();
                                    StartingTransitioningOut("");
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
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            upgrades.Reset();
        }

        public override void Start()
        {
            GetUpgrades();
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
            upgrades.Draw(spriteBatch);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitle, "UPGRADES", new Vector2(320, 210), Fonts.Green);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeBoldCredits, "CREDITS:", new Vector2(1155, 245), Fonts.Red);
            GraphicsUtilities.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeLightCredits, "" + currentGame.Credits, new Vector2(1440, 245), Fonts.Red);
        }

        private void GetUpgrades()
        {
            upgrades.AddItem(new UpgradeItem(new Vector2(280, 320), "SHIELDS", currentGame.Shields));
            upgrades.AddItem(new UpgradeItem(new Vector2(280, 450), "MOVE SPEED", currentGame.MovementSpeed));
            upgrades.AddItem(new UpgradeItem(new Vector2(280, 580), "DAMAGE", currentGame.Damage));
            upgrades.AddItem(new UpgradeItem(new Vector2(280, 710), "FIRE RATE", currentGame.FireRate));
            upgrades.AddItem(new UpgradeItem(new Vector2(280, 840), "WEAPON STR", currentGame.WeaponStrength));
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishedTransitioningOut("");
        }

        private void UpdateUpgrades()
        {
            Dictionary<string, int> upgradeLevels = upgrades.GetLevels();
            currentGame.Shields = upgradeLevels["SHIELDS"];
            currentGame.MovementSpeed = upgradeLevels["MOVE SPEED"];
            currentGame.Damage = upgradeLevels["DAMAGE"];
            currentGame.FireRate = upgradeLevels["FIRE RATE"];
            currentGame.WeaponStrength = upgradeLevels["WEAPON STR"];
        }
    }
}