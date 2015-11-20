using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class VelocityBasedRotationComponent : Component
    {
        private Vector2 velocity;

        public VelocityBasedRotationComponent(Entity entity)
            : base(entity)
        {
            entity.MessageCenter.AddListener<Vector2>("Velocity", UpdateVelocity);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            entity.MessageCenter.Broadcast("Get Velocity");
            entity.Rotation = (float)Math.Atan2(velocity.Y, velocity.X);
        }

        private void UpdateVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }
    }
}