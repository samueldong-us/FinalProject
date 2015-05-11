using FinalProject.Screens;

namespace FinalProject.GameComponents
{
    internal class ComponentSwingFireControl : Component
    {
        private bool active;
        private float delay;
        private bool firing;
        private float lengthOfFire;
        private float timeBetweenShots;
        private float timePassed;
        private float totalTimePassed;

        public ComponentSwingFireControl(Entity entity, float timeBetweenShots, float lengthOfFire, float delay)
            : base(entity)
        {
            this.timeBetweenShots = timeBetweenShots;
            timePassed = 0;
            active = false;
            this.lengthOfFire = lengthOfFire;
            this.delay = delay;
            firing = false;
            entity.MessageCenter.AddListener("Start Firing", StartFiring);
            entity.MessageCenter.AddListener("Stop Firing", StopFiring);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener("Start Firing", StartFiring);
            entity.MessageCenter.RemoveListener("Stop Firing", StopFiring);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            if (active)
            {
                if (firing)
                {
                    totalTimePassed += secondsPassed;
                    timePassed += secondsPassed;
                    if (timePassed > timeBetweenShots)
                    {
                        timePassed = 0;
                        entity.MessageCenter.Broadcast("Fire");
                    }
                    if (totalTimePassed > lengthOfFire)
                    {
                        totalTimePassed = 0;
                        firing = false;
                    }
                }
                else
                {
                    totalTimePassed += secondsPassed;
                    if (totalTimePassed > delay)
                    {
                        ScreenGame.MessageCenter.Broadcast<Entity>("Find Closest Player", entity);
                        totalTimePassed = 0;
                        firing = true;
                    }
                }
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