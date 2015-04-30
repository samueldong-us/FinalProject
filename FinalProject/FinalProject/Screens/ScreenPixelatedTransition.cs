using FinalProject.Utilities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject.Screens
{
    internal abstract class ScreenPixelatedTransition : Screen
    {
        private InterpolatedValue scaleIn, scaleOut;

        public ScreenPixelatedTransition(ContentManager contentManager, GraphicsDevice graphicsDevice)
            : base(contentManager, graphicsDevice)
        {
            scaleIn = new InterpolatedValueExponential(.002f, .25f, .5f);
            scaleIn.InterpolationFinished = ScaleInFinished;
            scaleOut = new InterpolatedValueExponential(.25f, .002f, .5f);
            scaleOut.InterpolationFinished = ScaleOutFinished;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        UtilitiesGraphics.BeginDrawingPixelated(spriteBatch, graphicsDevice, scaleIn.GetValue());
                        DrawScreen(spriteBatch);
                        UtilitiesGraphics.EndDrawingPixelated(spriteBatch, graphicsDevice, scaleIn.GetValue());
                    } break;
                case ScreenState.Active:
                    {
                        DrawScreen(spriteBatch);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        UtilitiesGraphics.BeginDrawingPixelated(spriteBatch, graphicsDevice, scaleOut.GetValue());
                        DrawScreen(spriteBatch);
                        UtilitiesGraphics.EndDrawingPixelated(spriteBatch, graphicsDevice, scaleOut.GetValue());
                    } break;
            }
        }

        protected virtual void ActiveUpdate(float secondsPassed)
        {
        }

        protected abstract void DrawScreen(SpriteBatch spriteBatch);

        protected override void Reset()
        {
            scaleIn.SetParameter(0);
            scaleOut.SetParameter(0);
            base.Reset();
        }

        protected override void ScreenUpdate(float secondsPassed)
        {
            switch (state)
            {
                case ScreenState.TransitioningIn:
                    {
                        scaleIn.Update(secondsPassed);
                    } break;
                case ScreenState.Active:
                    {
                        ActiveUpdate(secondsPassed);
                    } break;
                case ScreenState.TransitioningOut:
                    {
                        scaleOut.Update(secondsPassed);
                    } break;
            }
        }

        private void ScaleInFinished(float parameter)
        {
            state = ScreenState.Active;
        }

        private void ScaleOutFinished(float parameter)
        {
            FinishTransitioningOut();
        }
    }
}