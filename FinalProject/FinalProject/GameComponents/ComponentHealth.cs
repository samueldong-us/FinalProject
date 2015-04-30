namespace FinalProject.GameComponents
{
    internal class ComponentHealth : Component
    {
        private float health;

        private float maxHealth;

        public ComponentHealth(Entity entity, int maxHealth)
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
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                entity.MessageCenter.Broadcast("Health Depleted");
            }
        }
    }
}