using FinalProject.Screens;

namespace FinalProject.GameComponents
{
    internal class RemoveOnDeathComponent : Component
    {
        public RemoveOnDeathComponent(Entity entity)
            : base(entity)
        {
            entity.MessageCenter.AddListener("Health Depleted", RequestRemove);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener("Health Depleted", RequestRemove);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
        }

        private void RequestRemove()
        {
            GameScreen.Entities.RemoveEntity(entity);
        }
    }
}