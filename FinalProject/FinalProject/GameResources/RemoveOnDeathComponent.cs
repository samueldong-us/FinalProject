using FinalProject.Messaging;
using FinalProject.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class RemoveOnDeathComponent : Component
    {
        private Entity entity;

        public RemoveOnDeathComponent(Entity entity)
            : base(entity.MessageCenter)
        {
            this.entity = entity;
            entity.MessageCenter.AddListener("Health Depleted", SendRemoveMessage);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener("Health Depleted", SendRemoveMessage);
        }

        public override void Update(float secondsPassed)
        {
        }

        private void SendRemoveMessage()
        {
            GameScreen.GameMessageCenter.Broadcast<Entity>("Remove Entity", entity);
        }
    }
}