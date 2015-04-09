using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace FinalProject.Screens
{
    internal abstract class Screen
    {
        public delegate void ScreenEvent();

        protected enum ScreenState { TransitioningIn, TransitioningOut, Active, Inactive };

        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected ScreenState state;

        protected Screen(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            if (state != ScreenState.Inactive)
            {
                ScreenUpdate((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        protected abstract void ScreenUpdate(float secondsPassed);

        public virtual void LoadContent()
        {
            Set();
        }

        public virtual void Start()
        {
            state = ScreenState.TransitioningIn;
        }

        public virtual void Stop()
        {
            state = ScreenState.Inactive;
        }

        protected abstract void Set();

        public abstract void Reset();

        public void UnloadContent()
        {
            content.Unload();
            Reset();
        }

        public virtual void TransitionOut()
        {
            state = ScreenState.TransitioningOut;
        }

        public void LoadContentAsynchronously()
        {
            new Thread(LoadContent).Start();
        }

        public virtual void KeyPressed(Keys key)
        {
        }

        public virtual void KeyReleased(Keys key)
        {
        }
    }
}