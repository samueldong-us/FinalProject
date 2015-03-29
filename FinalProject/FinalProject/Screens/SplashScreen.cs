using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Screens
{
    class SplashScreen : Screen
    {
        private TransitionOutComplete FinishedTransitioningOut;
        public SplashScreen(ContentManager contentManager, TransitionOutComplete transitionOutCompleteDelegate) : base(contentManager)
        {
            FinishedTransitioningOut = transitionOutCompleteDelegate;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        protected override void LoadContent()
        {
            throw new NotImplementedException();
        }
        protected override void ScreenUpdate(float secondsPassed)
        {
            throw new NotImplementedException();
        }
        public override void Reset()
        {
            base.Reset();
        }
        protected override void Set()
        {
            throw new NotImplementedException();
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
    }
}
