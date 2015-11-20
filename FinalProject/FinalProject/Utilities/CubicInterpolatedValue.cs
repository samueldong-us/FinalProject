using Microsoft.Xna.Framework;

namespace FinalProject.Utilities
{
    internal class CubicInterpolatedValue : InterpolatedValue
    {
        private float beginningValue, endingValue;

        public CubicInterpolatedValue(float beginningValue, float endingValue, float timeToFinish)
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