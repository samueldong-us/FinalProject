using FinalProject.GameSaving;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class ScreenUpgrade : ScreenPixelatedTransition
    {
        private Texture2D background;

        private SaveGame currentGame;

        private ItemGroupUpgrade upgrades;

        public ScreenUpgrade(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            upgrades = new ItemGroupUpgrade();
            GameMain.MessageCenter.AddListener<SaveGame>("Save Game Pass to Upgrade", saveGame => currentGame = saveGame);
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
                                    if (cost <= currentGame.Credits)
                                    {
                                        currentGame.Credits -= cost;
                                        upgrades.UpgradeSelected();
                                        GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                    }
                                } break;
                            case Keys.Up:
                                {
                                    upgrades.MoveUp();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                } break;
                            case Keys.Down:
                                {
                                    upgrades.MoveDown();
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                } break;
                            case Keys.Escape:
                                {
                                    UpdateUpgrades();
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
            base.LoadContent();
        }

        public override void Start()
        {
            if (currentGame == null)
            {
                throw new Exception("A Save Game Must Be Passed In");
            }
            GetUpgrades();
            base.Start();
        }

        protected override void BeginTransitioningOut()
        {
            SaveGameManager.SaveGame(currentGame);
            GameMain.MessageCenter.Broadcast<SaveGame>("Save Game Pass to Command Center", currentGame);
            GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Command Center");
            base.BeginTransitioningOut();
        }

        protected override void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            upgrades.Draw(spriteBatch);
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(320, 210), "UPGRADES");
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeCreditTextFont, Fonts.Red, new Vector2(1155, 245), "CREDITS:");
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.UpgradeCreditsFont, Fonts.Red, new Vector2(1440, 245), "" + currentGame.Credits);
        }

        protected override void Reset()
        {
            currentGame = null;
            upgrades.Reset(); base.Reset();
        }

        protected override void SwitchScreens()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Command Center");
        }

        private void GetUpgrades()
        {
            upgrades.AddItem(new ItemUpgrade(new Vector2(280, 320), "SHIELDS", currentGame.Shields));
            upgrades.AddItem(new ItemUpgrade(new Vector2(280, 450), "MOVE SPEED", currentGame.MovementSpeed));
            upgrades.AddItem(new ItemUpgrade(new Vector2(280, 580), "DAMAGE", currentGame.Damage));
            upgrades.AddItem(new ItemUpgrade(new Vector2(280, 710), "FIRE RATE", currentGame.FireRate));
            upgrades.AddItem(new ItemUpgrade(new Vector2(280, 840), "WEAPON STR", currentGame.WeaponStrength));
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