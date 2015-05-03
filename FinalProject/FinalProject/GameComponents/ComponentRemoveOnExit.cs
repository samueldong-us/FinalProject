using FinalProject.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ComponentRemoveOnExit : Component
    {
        public ComponentRemoveOnExit(Entity entity)
            : base(entity)
        {
            entity.MessageCenter.AddListener<Entity>("Exited Bounds", RequestRemove);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Entity>("Exited Bounds", RequestRemove);
        }

        public override void Update(float secondsPassed)
        {
        }

        private void RequestRemove(Entity entity)
        {
            ScreenGame.Entities.RemoveEntity(entity);
        }
    }
}