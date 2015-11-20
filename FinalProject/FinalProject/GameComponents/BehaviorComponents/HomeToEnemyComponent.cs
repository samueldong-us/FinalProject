using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class HomeToEnemyComponent : Component
    {
        private const float MaxAcceleration = 400;
        private const float MaxAngle = (float)(Math.PI / 6);
        private Vector2 closestPosition;
        private Vector2 velocity;

        public HomeToEnemyComponent(Entity entity)
            : base(entity)
        {
            closestPosition = new Vector2(-1, -1);
            entity.MessageCenter.AddListener<Vector2>("Closest Enemy", SetClosestPosition);
            entity.MessageCenter.AddListener<Vector2>("Velocity", SetVelocity);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Closest Enemy", SetClosestPosition);
            entity.MessageCenter.RemoveListener<Vector2>("Velocity", SetVelocity);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            GameScreen.MessageCenter.Broadcast<Entity, float>("Find Closest Enemy By Angle", entity, MaxAngle);
            entity.MessageCenter.Broadcast("Get Velocity");
            if (closestPosition.Equals(new Vector2(-1, -1)))
            {
                entity.MessageCenter.Broadcast<Vector2>("Set Acceleration", Vector2.Zero);
            }
            else
            {
                Vector2 toClosest = closestPosition - entity.Position;
                toClosest.Normalize();
                toClosest *= velocity.Length();
                Vector2 acceleration = toClosest - velocity;
                acceleration /= secondsPassed;
                if (acceleration.Length() > MaxAcceleration)
                {
                    acceleration.Normalize();
                    acceleration *= MaxAcceleration;
                }
                entity.MessageCenter.Broadcast<Vector2>("Set Acceleration", acceleration);
            }
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }

        private void SetVelocity(Vector2 parameterOne)
        {
            velocity = parameterOne;
        }
    }
}