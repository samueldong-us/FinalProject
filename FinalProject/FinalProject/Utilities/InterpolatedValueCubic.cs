using Microsoft.Xna.Framework;

namespace FinalProject.Utilities
{
    internal class InterpolatedValueCubic : InterpolatedValue
    {
        private float beginningValue, endingValue;

        public InterpolatedValueCubic(float beginningValue, float endingValue, float timeToFinish)
            : base(timeToFinish)
        {
            this.beginningValue = beginningValue;
            this.endingValue = endingValue;
        }

        public override float GetValue()
        {
            return MathHelper.SmoothStep(beginningValue, endingValue, parameter);
        }
    }
}