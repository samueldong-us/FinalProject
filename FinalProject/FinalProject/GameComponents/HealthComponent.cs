using FinalProject.Messaging;

namespace FinalProject.GameComponents
{
    internal class HealthComponent : Component
    {
        public int Health;
        public int MaxHealth;

        public HealthComponent(Entity entity, int maxHealth)
            : base(entity)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            entity.MessageCenter.AddListener<int>("Take Damage", TakeDamage);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<int>("Take Damage", TakeDamage);
        }

        public override void Update(float secondsPassed)
        {
        }

        private void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                entity.MessageCenter.Broadcast("Health Depleted");
            }
        }
    }
}