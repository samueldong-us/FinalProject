namespace FinalProject.GameComponents
{
    internal abstract class Component
    {
        protected Entity entity;

        public Component(Entity entity)
        {
            entity.AddComponent(this);
            this.entity = entity;
        }

        public abstract void Dispose();

        public abstract void Update(float secondsPassed);
    }
}