using FinalProject.Screens;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class ComponentBounce : Component
    {
        private bool bounced;

        private Vector2 velocity;

        public ComponentBounce(Entity entity)
            : base(entity)
        {
            bounced = false;
            entity.MessageCenter.AddListener<Vector2>("Velocity", GetVelocity);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Velocity", GetVelocity);
        }

        public override void Update(float secondsPassed)
        {
            if (!bounced)
            {
                if (entity.Position.Y > ScreenGame.Bounds.Bottom - 50)
                {
                    entity.MessageCenter.Broadcast("Get Velocity");
                    Vector2 newVelocity = new Vector2(velocity.X, -velocity.Y);
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                    bounced = true;
                }
                if (entity.Position.Y < ScreenGame.Bounds.Top + 50)
                {
                    entity.MessageCenter.Broadcast("Get Velocity");
                    Vector2 newVelocity = new Vector2(velocity.X, -velocity.Y);
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                    bounced = true;
                }
                if (entity.Position.X < ScreenGame.Bounds.Left + 50)
                {
                    entity.MessageCenter.Broadcast("Get Velocity");
                    Vector2 newVelocity = new Vector2(-velocity.X, velocity.Y);
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                    bounced = true;
                }
                if (entity.Position.X > ScreenGame.Bounds.Right - 50)
                {
                    entity.MessageCenter.Broadcast("Get Velocity");
                    Vector2 newVelocity = new Vector2(-velocity.X, velocity.Y);
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                    bounced = true;
                }
            }
        }

        private void GetVelocity(Vector2 parameterOne)
        {
            velocity = parameterOne;
        }
    }
}