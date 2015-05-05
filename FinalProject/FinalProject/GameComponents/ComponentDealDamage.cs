﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ComponentDealDamage : Component
    {
        private int damage;

        public ComponentDealDamage(Entity entity, int damage)
            : base(entity)
        {
            this.damage = damage;
            entity.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Entity, Entity>("Collided With", DealDamage);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
        }

        private void DealDamage(Entity parameterOne, Entity parameterTwo)
        {
            parameterTwo.MessageCenter.Broadcast<float>("Take Damage", damage);
        }
    }
}