﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Screens
{
    internal class MainMenuScreen : Screen
    {
        private ScreenEvent FinishedTransitioningOut;

        public MainMenuScreen(ContentManager contentManager, ScreenEvent transitionOutCompleteDelegate)
            : base(contentManager)
        {
            FinishedTransitioningOut = transitionOutCompleteDelegate;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
        }

        public override void Reset()
        {
        }

        protected override void Set()
        {
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