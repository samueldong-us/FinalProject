using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class VelocityAccelerationComponent : Component
    {
        private Vector2 velocity, acceleration;

        public VelocityAccelerationComponent(Entity entity, Vector2 velocity, Vector2 acceleration)
            : base(entity)
        {
            this.velocity = velocity;
            this.acceleration = acceleration;
            entity.MessageCenter.AddListener<Vector2>("Set Velocity", SetVelocity);
            entity.MessageCenter.AddListener<Vector2>("Set Acceleration", SetAcceleration);
            entity.MessageCenter.AddListener("Get Velocity", GetVelocity);
            entity.MessageCenter.AddListener("Get Acceleration", GetAcceleration);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Set Velocity", SetVelocity);
            entity.MessageCenter.RemoveListener<Vector2>("Set Acceleration", SetAcceleration);
            entity.MessageCenter.RemoveListener("Get Velocity", GetVelocity);
            entity.MessageCenter.RemoveListener("Get Acceleration", GetAcceleration);
        }

        public override void Update(float secondsPassed)
        {
            velocity += acceleration * secondsPassed;
            entity.Position += velocity * secondsPassed;
        }

        private void GetAcceleration()
        {
            entity.MessageCenter.Broadcast<Vector2>("Acceleration", acceleration);
        }

        private void GetVelocity()
        {
            entity.MessageCenter.Broadcast<Vector2>("Velocity", velocity);
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