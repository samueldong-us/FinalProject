using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace FinalProject.Screens
{
    internal abstract class Screen
    {
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected ScreenState state;

        protected Screen(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
        }

        public delegate void ScreenEvent(string message);

        protected enum ScreenState { TransitioningIn, TransitioningOut, Active, Inactive };

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

        protected abstract void BeginTransitioningOut();

        protected abstract void FinishedLoading();

        protected abstract void FinishTransitioningOut();

        protected abstract void Reset();

        protected abstract void ScreenUpdate(float secondsPassed);
    }
}