using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    class DelayedTargetingComponent : Component
    {
        private float timePassed;
        private float delay;
        private float secondDelay;
        private float speed;
        private Vector2 closestPosition;
        private Vector2 lastVelocity;
        private bool happened;
        public DelayedTargetingComponent(Entity entity, float delay, float secondDelay, float speed)
            : base(entity)
        {
            this.delay = delay;
            this.speed = speed;
            this.secondDelay = secondDelay;
            timePassed = 0;
            closestPosition = new Vector2(-1, -1);
            entity.MessageCenter.AddListener<Vector2>("Closest Player", SetClosestPosition);
            entity.MessageCenter.AddListener<Vector2>("Velocity", SetVelocity);
        }

        private void SetVelocity(Vector2 parameterOne)
        {
            lastVelocity = parameterOne;
        }

        private void SetClosestPosition(Vector2 parameterOne)
        {
            closestPosition = parameterOne;
        }
        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Vector2>("Closest Player", SetClosestPosition);
            entity.MessageCenter.AddListener<Vector2>("Velocity", SetVelocity);
        }

        public override void Update(float secondsPassed)
        {
            if (timePassed >= 0)
            {
                if (timePassed < delay)
                {
                    timePassed += secondsPassed;
                }
                else if (timePassed < delay + secondDelay)
                {
                    timePassed += secondsPassed;
                    if (!happened)
                    {
                        entity.MessageCenter.Broadcast("Get Velocity");
                        entity.MessageCenter.Broadcast<Vector2>("Set Velocity", lastVelocity * 0.00001f);
                        happened = true;
                    }
                }
                else
                {
                    timePassed = -1;
                    GameScreen.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
                    Vector2 fromTo = closestPosition - entity.Position;
                    float angle = (float)Math.Atan2(fromTo.Y, fromTo.X);
                    if (closestPosition.Equals(new Vector2(-1, -1)))
                    {
                        angle = (float)(Math.PI / 2);
                    }
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", MathUtilities.VectorFromMagnitude(speed, angle));
                }
            }
        }
    }
}
