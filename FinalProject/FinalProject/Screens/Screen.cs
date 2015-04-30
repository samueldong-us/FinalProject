using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace FinalProject.Screens
{
    internal abstract class Screen
    {
        public delegate void ScreenEvent(string message);

        protected enum ScreenState { TransitioningIn, TransitioningOut, Active, Inactive };

        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected bool otherScreenReady;

        protected bool readyToSwitch;

        protected ScreenState state;

        protected Screen(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            otherScreenReady = false;
            readyToSwitch = false;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void KeyPressed(Keys key)
        {
        }

        public virtual void KeyReleased(Keys key)
        {
        }

        public virtual void LoadContent()
        {
            FinishedLoading();
        }

        public void LoadContentAsynchronously()
        {
            new Thread(LoadContent).Start();
        }

        public virtual void Start()
        {
            state = ScreenState.TransitioningIn;
        }

        public virtual void Stop()
        {
            state = ScreenState.Inactive;
        }

        public virtual void TransitionOut()
        {
            state = ScreenState.TransitioningOut;
        }

        public void UnloadContent()
        {
            content.Unload();
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            if (state != ScreenState.Inactive)
            {
                ScreenUpdate((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        protected virtual void BeginTransitioningOut()
        {
            GameMain.MessageCenter.AddListener("Finished Loading", OtherScreenFinishedLoading);
            TransitionOut();
        }

        protected void FinishTransitioningOut()
        {
            if (otherScreenReady)
            {
                SwitchScreens();
            }
            else
            {
                readyToSwitch = true;
            }
        }

        protected virtual void Reset()
        {
            otherScreenReady = false;
            readyToSwitch = false;
            GameMain.MessageCenter.RemoveListener("Finished Loading", OtherScreenFinishedLoading);
        }

        protected abstract void ScreenUpdate(float secondsPassed);

        protected abstract void SwitchScreens();

        private void FinishedLoading()
        {
            GameMain.MessageCenter.Broadcast("Finished Loading");
        }

        private void OtherScreenFinishedLoading()
        {
            if (readyToSwitch)
            {
                SwitchScreens();
            }
            else
            {
                otherScreenReady = true;
            }
        }
    }
}