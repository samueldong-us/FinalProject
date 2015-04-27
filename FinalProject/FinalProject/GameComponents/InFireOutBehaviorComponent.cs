using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class InFireOutBehaviorComponent : Component
    {
        private const float CeaseFireY = 600;
        private float outSpeed;
        private float outTime;
        private float switchTime;
        private float timePassed;

        public InFireOutBehaviorComponent(Entity entity, Vector2 switchingPoint, float inSpeed, float outSpeed)
            : base(entity)
        {
            Vector2 direction = (switchingPoint - entity.Position);
            switchTime = direction.Length() / inSpeed;
            outTime = (GameScreen.Bounds.Bottom + 200 - switchingPoint.Y) / outSpeed + switchTime;
            timePassed = 0;
            direction.Normalize();
            entity.MessageCenter.Broadcast<Vector2>("Set Velocity", direction * inSpeed);
            this.outSpeed = outSpeed;
        }

        public override void Dispose()
        {
        }

        public override void Update(float secondsPassed)
        {
            timePassed += secondsPassed;
            if (timePassed > switchTime)
            {
                entity.MessageCenter.Broadcast<Vector2>("Set Velocity", new Vector2(0, outSpeed));
                entity.MessageCenter.Broadcast("Start Firing");
                switchTime = outTime + 1;
            }
            if (entity.Position.Y > CeaseFireY)
            {
                entity.MessageCenter.Broadcast("Stop Firing");
            }
            if (timePassed > outTime)
            {
                GameScreen.MessageCenter.Broadcast<Entity>("Remove Entity", entity);
            }
        }
    }
}