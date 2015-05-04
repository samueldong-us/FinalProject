using FinalProject.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ComponentRemoveOnCollide : Component
    {
        public ComponentRemoveOnCollide(Entity entity)
            : base(entity)
        {
            entity.MessageCenter.AddListener<Entity, Entity>("Collided With", Remove);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Entity, Entity>("Collided With", Remove);
        }

        public override void Update(float secondsPassed)
        {
        }

        private void Remove(Entity parameterOne, Entity parameterTwo)
        {
            ScreenGame.Entities.RemoveEntity(entity);
        }
    }
}