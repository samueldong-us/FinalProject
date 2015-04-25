using FinalProject.Messaging;

namespace FinalProject.GameComponents
{
    internal class HealthComponent : Component
    {
        private int health;
        private int maxHealth;

        public HealthComponent(Entity entity, int maxHealth)
            : base(entity)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            entity.MessageCenter.AddListener<int>("Take Damage", TakeDamage);
            entity.MessageCenter.AddListener("Get Health", ReturnHealth);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<int>("Take Damage", TakeDamage);
            entity.MessageCenter.RemoveListener("Get Health", ReturnHealth);
        }

        public override void Update(float secondsPassed)
        {
        }

        private void ReturnHealth()
        {
            entity.MessageCenter.Broadcast<int, int>("Health", health, maxHealth);
        }

        private void TakeDamage(int damage)
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