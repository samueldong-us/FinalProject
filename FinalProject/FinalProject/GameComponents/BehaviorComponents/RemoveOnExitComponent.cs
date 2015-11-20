using FinalProject.Screens;

namespace FinalProject.GameComponents
{
    internal class RemoveOnExitComponent : Component
    {
        public RemoveOnExitComponent(Entity entity)
            : base(entity)
        {
            entity.MessageCenter.AddListener<Entity>("Exited Bounds", RequestRemove);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Entity>("Exited Bounds", RequestRemove);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
        }

        private void RequestRemove(Entity entity)
        {
            GameScreen.Entities.RemoveEntity(entity);
        }
    }
}