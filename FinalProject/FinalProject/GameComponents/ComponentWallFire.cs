using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ComponentWallFire : Component
    {
        private bool active;

        private float timeBetweenShots;
        private float timePassed;
        private int wallWidth;

        public ComponentWallFire(Entity entity, float fireRate, int wallWidth)
            : base(entity)
        {
            this.timeBetweenShots = fireRate;
            this.wallWidth = wallWidth;
            timePassed = 0;
            active = false;
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
            timePassed += secondsPassed;
            if (timePassed > timeBetweenShots && active)
            {
                timePassed = 0;
                FireWall();
            }
        }

        private void FireWall()
        {
            for (int i = 0; i < wallWidth; i++)
            {
                entity.MessageCenter.Broadcast("Fire");
            }
            entity.MessageCenter.Broadcast<float>("Rotate", (float)(GameMain.RNG.NextDouble() * Math.PI * 2));
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