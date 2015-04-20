using FinalProject.Messaging;

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
            messageCenter.RemoveListener<int>("Take Damage", TakeDamage);
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
                messageCenter.Broadcast("Health Depleted");
            }
        }
    }
}