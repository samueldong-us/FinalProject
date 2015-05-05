using FinalProject.Screens;

namespace FinalProject.GameComponents
{
    internal class ComponentRemoveOnCollide : Component
    {
        public ComponentRemoveOnCollide(Entity entity)
            : base(entity)
        {
            entity.MessageCenter.AddListener<Entity, Entity>("Collided With", Remove);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener<Entity, Entity>("Collided With", Remove);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
        }

        private void Remove(Entity parameterOne, Entity parameterTwo)
        {
            ScreenGame.Entities.RemoveEntity(entity);
        }
    }
}