using FinalProject.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class RemoveOnDeathComponent : Component
    {
        public RemoveOnDeathComponent(Entity entity)
            : base(entity)
        {
            entity.MessageCenter.AddListener("Health Depleted", RequestRemove);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener("Health Depleted", RequestRemove);
        }

        public override void Update(float secondsPassed)
        {
        }

        private void RequestRemove()
        {
            GameScreen.MessageCenter.Broadcast<Entity>("Remove Entity", entity);
        }
    }
}