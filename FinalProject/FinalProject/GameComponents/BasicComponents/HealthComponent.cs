﻿namespace FinalProject.GameComponents
{
    internal class HealthComponent : Component
    {
        private float health;
        private float maxHealth;

        public HealthComponent(Entity entity, int maxHealth)
            : base(entity)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            entity.MessageCenter.AddListener<float>("Take Damage", TakeDamage);
            entity.MessageCenter.AddListener("Get Health", ReturnHealth);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<float>("Take Damage", TakeDamage);
            entity.MessageCenter.RemoveListener("Get Health", ReturnHealth);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
        }

        private void ReturnHealth()
        {
            entity.MessageCenter.Broadcast<float, float>("Health", health, maxHealth);
        }

        private void TakeDamage(float damage)
        {
            if (health > 0)
            {
                health -= damage;
                if (health <= 0)
                {
                    health = 0;
                    entity.MessageCenter.Broadcast("Health Depleted");
                }
            }
        }
    }
}