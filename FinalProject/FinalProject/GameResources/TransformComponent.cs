using FinalProject.Messaging;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class TransformComponent : Component
    {
        public Vector2 Position;
        public float Scale;
        public float Theta;
        protected Vector2 velocity;

        public TransformComponent(MessageCenter messageCenter)
            : base(messageCenter)
        {
            Position = Vector2.Zero;
            Scale = 1;
            Theta = 0;
            messageCenter.AddListener<Vector2>("Set Velocity", SetVelocity);
        }

        public override void Dispose()
        {
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public override void Update(float secondsPassed)
        {
            Position += secondsPassed * velocity;
        }
    }
}