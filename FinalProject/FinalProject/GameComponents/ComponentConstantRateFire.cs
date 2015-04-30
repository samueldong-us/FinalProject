namespace FinalProject.GameComponents
{
    internal class ComponentConstantRateFire : Component
    {
        private bool active;

        private float timeBetweenShots;

        private float timePassed;

        public ComponentConstantRateFire(Entity entity, float timeBetweenShots)
            : base(entity)
        {
            this.timeBetweenShots = timeBetweenShots;
            timePassed = 0;
            active = false;
            entity.MessageCenter.AddListener("Start Firing", StartFiring);
            entity.MessageCenter.AddListener("Stop Firing", StopFiring);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener("Start Firing", StartFiring);
            entity.MessageCenter.RemoveListener("Stop Firing", StopFiring);
        }

        public override void Update(float secondsPassed)
        {
            timePassed += secondsPassed;
            if (timePassed > timeBetweenShots && active)
            {
                timePassed = 0;
                entity.MessageCenter.Broadcast("Fire");
            }
        }

        private void StartFiring()
        {
            active = true;
        }

        private void StopFiring()
        {
            active = false;
        }
    }
}