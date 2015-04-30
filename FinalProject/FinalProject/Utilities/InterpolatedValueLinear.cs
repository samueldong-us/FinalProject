using Microsoft.Xna.Framework;

namespace FinalProject.Utilities
{
    internal class InterpolatedValueLinear : InterpolatedValue
    {
        private float beginningValue, endingValue;

        public InterpolatedValueLinear(float beginningValue, float endingValue, float timeToFinish)
            : base(timeToFinish)
        {
            this.beginningValue = beginningValue;
            this.endingValue = endingValue;
        }

        public override float GetValue()
        {
            return MathHelper.Lerp(beginningValue, endingValue, parameter);
        }
    }
}