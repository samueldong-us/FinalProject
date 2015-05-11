using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class ComponentDelayedSetVelocity : Component
    {
        private float delay;
        private Vector2 newVelocity;
        private float timePassed;

        public ComponentDelayedSetVelocity(Entity entity, float delay, Vector2 velocity)
            : base(entity)
        {
            newVelocity = velocity;
            this.delay = delay;
            timePassed = 0;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            if (timePassed >= 0)
            {
                if (timePassed < delay)
                {
                    timePassed += secondsPassed;
                }
                else
                {
                    timePassed = -1;
                    entity.MessageCenter.Broadcast<Vector2>("Set Velocity", newVelocity);
                }
            }
        }
    }
}