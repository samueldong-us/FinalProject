using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject.Screens
{
    internal class ScreenSettings : ScreenPixelatedTransition
    {
        private const float VolumeChangeSpeed = 10;
        private Texture2D background;

        private float master;

        private ItemGroupMenu menuItems;

        private ItemMenu volume;

        public ScreenSettings(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            menuItems = new ItemGroupMenu();
        }

        public override void KeyPressed(Keys key)
        {
            switch (state)
            {
                case ScreenState.Active:
                    {
                        switch (key)
                        {
                            case Keys.Left:
                                {
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
                                } break;
                            case Keys.Right:
                                {
                                    GameMain.Audio.PlayOneTimeSound("Menu Sound");
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
            master = (int)(GameMain.Audio.GetVolume() * 100);
            volume = new ItemMenu(new Vector2(280, 320), "VOLUME: " + (int)master);
            menuItems.AddItem(volume);
            base.Start();
        }

        protected override void ActiveUpdate(float secondsPassed)
        {
            float lastMaster = master;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                master = MathHelper.Clamp(master - VolumeChangeSpeed * secondsPassed, 0, 100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                master = MathHelper.Clamp(master + VolumeChangeSpeed * secondsPassed, 0, 100);
            }
            if (master != lastMaster)
            {
                volume.Text = "VOLUME: " + (int)master;
                GameMain.Audio.SetVolume((int)master / 100f);
            }
            base.ActiveUpdate(secondsPassed);
        }

        protected override void BeginTransitioningOut()
        {
            GameMain.MessageCenter.Broadcast<string>("Start Loading Content", "Main Menu");
            base.BeginTransitioningOut();
        }

        protected override void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameMain.VirtualWidth, GameMain.VirtualHeight), Color.White);
            menuItems.Draw(spriteBatch);
            UtilitiesGraphics.DrawStringVerticallyCentered(spriteBatch, Fonts.MenuTitleFont, Fonts.Green, new Vector2(320, 210), "SETTINGS");
        }

        protected override void Reset()
        {
            menuItems.Reset();
            base.Reset();
        }

        protected override void SwitchScreens()
        {
            GameMain.MessageCenter.Broadcast<string>("Switch Screens", "Main Menu");
        }
    }
}