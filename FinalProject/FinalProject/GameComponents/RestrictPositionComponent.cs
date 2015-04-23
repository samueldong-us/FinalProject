using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class RestrictPositionComponent : Component
    {
        private Rectangle bounds;
        private float horizontalPadding;
        private float verticalPadding;

        public RestrictPositionComponent(Entity entity, float horizontalPadding, float verticalPadding, Rectangle bounds)
            : base(entity)
        {
            this.horizontalPadding = horizontalPadding;
            this.verticalPadding = verticalPadding;
            this.bounds = bounds;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Update(float secondsPassed)
        {
            entity.Position.X = MathHelper.Clamp(entity.Position.X, bounds.Left + horizontalPadding, bounds.Right - horizontalPadding);
            entity.Position.Y = MathHelper.Clamp(entity.Position.Y, bounds.Bottom + verticalPadding, bounds.Top - verticalPadding);
        }
    }
}