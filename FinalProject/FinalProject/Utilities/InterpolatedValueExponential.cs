using System;

namespace FinalProject.Utilities
{
    internal class InterpolatedValueExponential : InterpolatedValue
    {
        private float beginningValue, endingValue;
        private float offset;

        public InterpolatedValueExponential(float beginningValue, float endingValue, float timeToFinish)
            : base(timeToFinish)
        {
            float smallestValue = Math.Min(beginningValue, endingValue);
            offset = 1 - smallestValue;
            this.beginningValue = beginningValue + offset;
            this.endingValue = endingValue + offset;
        }

        public override float GetValue()
        {
            return beginningValue * (float)Math.Pow(Math.E, Math.Log(endingValue / beginningValue) * parameter) - offset;
        }
    }
}