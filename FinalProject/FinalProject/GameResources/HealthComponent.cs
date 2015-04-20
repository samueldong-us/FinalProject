using FinalProject.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class HealthComponent : Component
    {
        public int Health;

        public HealthComponent(MessageCenter messageCenter, int maxHealth)
            : base(messageCenter)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            messageCenter.AddListener<int>("Take Damage", TakeDamage);
        }

        public int MaxHealth { get; private set; }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Update(float secondsPassed)
        {
            throw new NotImplementedException();
        }

        private void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
                messageCenter.Broadcast("Health Depleted");
            }
        }
    }
}