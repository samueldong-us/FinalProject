using System;

namespace FinalProject.Utilities
{
    internal abstract class InterpolatedValue
    {
        public delegate void InterpolationEvent(float parameter);

        public InterpolationEvent InterpolationFinished;
        protected float timeScale;
        protected float parameter;

        protected InterpolatedValue(float timeToFinish)
        {
            timeScale = 1 / timeToFinish;
            parameter = 0;
        }

        public void Update(float secondsPassed)
        {
            parameter += secondsPassed * timeScale;
            if (parameter > 1)
            {
                if (InterpolationFinished != null)
                {
                    InterpolationFinished(parameter);
                }
                parameter = 1;
            }
        }

        public void SetParameter(float parameter)
        {
            if (parameter >= 0 && parameter <= 1)
            {
                this.parameter = parameter;
            }
            else
            {
                throw new Exception("The parameter must be between 0 and 1");
            }
        }

        public abstract float GetValue();
    }
}