using FinalProject.Screens;
using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class ComponentBehaviorInFireOut : Component
    {
        private int ceaseFireY;
        private float outSpeed;
        private float outTime;
        private float switchTime;
        private float timePassed;

        public ComponentBehaviorInFireOut(Entity entity, Vector2 switchingPoint, float inSpeed, float outSpeed, int ceaseFireY)

            : base(entity)
        {
            Vector2 direction = (switchingPoint - entity.Position);
            switchTime = direction.Length() / inSpeed;
            outTime = (ScreenGame.Bounds.Bottom + 200 - switchingPoint.Y) / outSpeed + switchTime;
            timePassed = 0;
            direction.Normalize();
            entity.MessageCenter.Broadcast<Vector2>("Set Velocity", direction * inSpeed);
            this.outSpeed = outSpeed;
            this.ceaseFireY = ceaseFireY;
        }

        public override void Dispose()
        {
            base.Dispose();
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
            if (entity.Position.Y > ceaseFireY)
            {
                entity.MessageCenter.Broadcast("Stop Firing");
            }
            if (timePassed > outTime)
            {
                ScreenGame.Entities.RemoveEntity(entity);
            }
        }
    }
}