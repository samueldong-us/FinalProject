﻿using FinalProject.Screens;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class BounceComponent : Component
    {
        private bool bounced;
        private Vector2 velocity;

        public BounceComponent(Entity entity)
            : base(entity)
        {
            bounced = false;
            entity.MessageCenter.AddListener<Vector2>("Velocity", GetVelocity);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Velocity", GetVelocity);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            if (!bounced)
            {
                if (entity.Position.Y > GameScreen.Bounds.Bottom - 50)
                {
                    entity.MessageCenter.Broadcast("Get Velocity");
                    Vector2 newVelocity = new Vector2(velocity.X, -velocity.Y);
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                    bounced = true;
                }
                if (entity.Position.Y < GameScreen.Bounds.Top + 50)
                {
                    entity.MessageCenter.Broadcast("Get Velocity");
                    Vector2 newVelocity = new Vector2(velocity.X, -velocity.Y);
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                    bounced = true;
                }
                if (entity.Position.X < GameScreen.Bounds.Left + 50)
                {
                    entity.MessageCenter.Broadcast("Get Velocity");
                    Vector2 newVelocity = new Vector2(-velocity.X, velocity.Y);
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                    bounced = true;
                }
                if (entity.Position.X > GameScreen.Bounds.Right - 50)
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