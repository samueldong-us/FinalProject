using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace FinalProject.Screens
{
    internal abstract class Screen
    {
        public delegate void TransitionOutComplete();

        protected enum ScreenState { TransitioningIn, TransitioningOut, Active, Inactive };

        protected ContentManager content;
        protected ScreenState state;

        protected Screen(ContentManager content)
        {
            this.content = content;
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

        protected abstract void LoadContent();

        public virtual void Start()
        {
            state = ScreenState.TransitioningIn;
        }

        public virtual void Stop()
        {
            state = ScreenState.Inactive;
        }

        protected abstract void Set();

        public virtual void Reset()
        {
            Set();
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public virtual void TransitionOut()
        {
            state = ScreenState.TransitioningOut;
        }

        public void LoadContentAsynchronously()
        {
            new Thread(LoadContent).Start();
        }
    }
}