using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal class StarBehaviorComponent : Component
    {
        private const float Speed = 1000;
        private Vector2 center;
        private Vector2 currentPoint;
        private float delay;
        private int numberOfPoints;
        private float radius;
        private bool resting;
        private bool straight;
        private float timeSpent;
        private float timeUntilReach;

        public StarBehaviorComponent(Entity entity, Vector2 center, float delay, int numberOfPoints, float radius)
            : base(entity)
        {
            this.center = center;
            this.delay = delay;
            this.numberOfPoints = numberOfPoints;
            this.radius = radius;
            resting = true;
            straight = true;
            timeSpent = delay;
            float randomAngle = (float)(Math.PI * 2 * GameMain.RNG.NextDouble());
            currentPoint = new Vector2(radius * (float)Math.Cos(randomAngle), radius * (float)Math.Sin(randomAngle)) + center;
        }

        public override void Update(float secondsPassed)
        {
            if (resting)
            {
                timeSpent += secondsPassed;
                if (timeSpent > delay)
                {
                    timeSpent = 0;
                    resting = false;
                    CalculateNextPoint();
                    Vector2 toCurrentPoint = currentPoint - entity.Position;
                    toCurrentPoint.Normalize();
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", toCurrentPoint * Speed);
                }
            }
            else
            {
                timeSpent += secondsPassed;
                if (timeSpent > timeUntilReach)
                {
                    timeSpent = 0;
                    resting = true;
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", Vector2.Zero);
                    entity.MessageCenter.Broadcast("Fire");
                }
            }
        }

        private void CalculateNextPoint()
        {
            Vector2 toCurrent = currentPoint - center;
            float currentAngle = (float)Math.Atan2(toCurrent.Y, toCurrent.X);
            if (straight)
            {
                currentAngle += (float)(Math.PI);
            }
            else
            {
                currentAngle += (float)(Math.PI + Math.PI / numberOfPoints);
            }
            currentPoint = new Vector2(radius * (float)Math.Cos(currentAngle), radius * (float)Math.Sin(currentAngle)) + center;
            timeUntilReach = Vector2.Distance(currentPoint, entity.Position) / Speed;
            straight = !straight;
        }
    }
}