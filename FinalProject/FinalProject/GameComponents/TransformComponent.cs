using FinalProject.Messaging;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class TransformComponent : Component
    {
        public Vector2 Position;
        public float Scale;
        public float Theta;
        public Vector2 Velocity;

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
            this.Velocity = velocity;
        }

        public override void Update(float secondsPassed)
        {
            Position += secondsPassed * Velocity;
        }
    }
}