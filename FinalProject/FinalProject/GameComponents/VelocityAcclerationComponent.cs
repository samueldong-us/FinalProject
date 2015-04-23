using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class VelocityAcclerationComponent : Component
    {
        private Vector2 velocity, acceleration;

        public VelocityAcclerationComponent(Entity entity, Vector2 velocity, Vector2 acceleration)
            : base(entity)
        {
            this.velocity = velocity;
            this.acceleration = acceleration;
            entity.MessageCenter.AddListener<Vector2>("Set Velocity", SetVelocity);
            entity.MessageCenter.AddListener<Vector2>("Set Acceleration", SetAcceleration);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Set Velocity", SetVelocity);
            entity.MessageCenter.RemoveListener<Vector2>("Set Acceleration", SetAcceleration);
        }

        public override void Update(float secondsPassed)
        {
            velocity += acceleration * secondsPassed;
            entity.Position += velocity * secondsPassed;
        }

        private void SetAcceleration(Vector2 acceleration)
        {
            this.acceleration = acceleration;
        }

        private void SetVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }
    }
}