using FinalProject.Messaging;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class EnemyHomingComponent : Component
    {
        private float bulletSpeed;
        private Vector2 closestPosition;
        private float currentAngle;
        private float maxAngularSpeed;
        private TransformComponent transform;

        public EnemyHomingComponent(MessageCenter messageCenter, float maxAngularSpeed, Vector2 bulletVelocity, TransformComponent transform)
            : base(messageCenter)
        {
            messageCenter.AddListener<Vector2>("Closest Position", SetClosestPosition);
            this.transform = transform;
            this.maxAngularSpeed = maxAngularSpeed;
            this.currentAngle = (float)Math.Atan2(bulletVelocity.Y, bulletVelocity.X);
            if (currentAngle < 0)
            {
                currentAngle += (float)(Math.PI * 2);
            }
            bulletSpeed = bulletVelocity.Length();
        }

        public override void Dispose()
        {
            messageCenter.RemoveListener<Vector2>("Closest Position", SetClosestPosition);
        }

        public override void Update(float secondsPassed)
        {
            GameScreen.GameMessageCenter.Broadcast<Vector2, MessageCenter>("Get Closest Enemy", transform.Position, messageCenter);
            if (GameScreen.Bounds.Contains((int)closestPosition.X, (int)closestPosition.Y))
            {
                float targetAngle = (float)Math.Atan2((closestPosition - transform.Position).Y, (closestPosition - transform.Position).X);
                if (targetAngle < 0)
                {
                    targetAngle += (float)(Math.PI * 2);
                }
                float positiveChange = targetAngle - currentAngle;
                float negativeChange = currentAngle - targetAngle;
                float timeToReach = Vector2.Distance(closestPosition, transform.Position) / bulletSpeed;
                if (positiveChange < 0)
                {
                    positiveChange += (float)(Math.PI * 2);
                }
                if (negativeChange < 0)
                {
                    negativeChange += (float)(Math.PI * 2);
                }
                if (positiveChange < negativeChange)
                {
                    if (positiveChange < (float)(Math.PI / 4))
                    {
                        currentAngle += MathHelper.Clamp(positiveChange / timeToReach, 0, maxAngularSpeed) * secondsPassed;
                    }
                }
                else
                {
                    if (negativeChange < (float)(Math.PI / 4))
                    {
                        currentAngle -= MathHelper.Clamp(negativeChange / timeToReach, 0, maxAngularSpeed) * secondsPassed;
                    }
                }
                if (currentAngle < 0)
                {
                    currentAngle += (float)(Math.PI * 2);
                }
                if (currentAngle > (float)(Math.PI * 2))
                {
                    currentAngle -= (float)(Math.PI * 2);
                }
            }
            transform.Velocity = new Vector2((float)(bulletSpeed * Math.Cos(currentAngle)), (float)(bulletSpeed * Math.Sin(currentAngle)));
            transform.Theta = MathHelper.ToDegrees(currentAngle);
        }

        private void SetClosestPosition(Vector2 closestPosition)
        {
            this.closestPosition = closestPosition;
        }
    }
}