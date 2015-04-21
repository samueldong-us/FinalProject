using FinalProject.Messaging;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class BoundedTransformComponent : TransformComponent
    {
        private Rectangle bounds;
        private int horizontalPadding, verticalPadding;

        public BoundedTransformComponent(MessageCenter messageCenter, int horizontalPadding, int verticalPadding, Rectangle bounds)
            : base(messageCenter)
        {
            this.horizontalPadding = horizontalPadding;
            this.verticalPadding = verticalPadding;
            this.bounds = bounds;
        }

        public override void Update(float secondsPassed)
        {
            Position.X = (int)MathHelper.Clamp(Position.X + Velocity.X * secondsPassed, horizontalPadding + bounds.Left, bounds.Right - horizontalPadding);
            Position.Y = (int)MathHelper.Clamp(Position.Y + Velocity.Y * secondsPassed, verticalPadding + bounds.Top, bounds.Bottom - verticalPadding);
        }
    }
}