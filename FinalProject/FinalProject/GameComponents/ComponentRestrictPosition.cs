using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class ComponentRestrictPosition : Component
    {
        private Rectangle bounds;

        private float horizontalPadding;

        private float verticalPadding;

        public ComponentRestrictPosition(Entity entity, float horizontalPadding, float verticalPadding, Rectangle bounds)
            : base(entity)
        {
            this.horizontalPadding = horizontalPadding;
            this.verticalPadding = verticalPadding;
            this.bounds = bounds;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            entity.Position.X = MathHelper.Clamp(entity.Position.X, bounds.Left + horizontalPadding, bounds.Right - horizontalPadding);
            entity.Position.Y = MathHelper.Clamp(entity.Position.Y, bounds.Top + verticalPadding, bounds.Bottom - verticalPadding);
        }
    }
}