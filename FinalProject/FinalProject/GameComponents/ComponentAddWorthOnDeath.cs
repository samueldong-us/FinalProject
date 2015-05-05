using FinalProject.Screens;

namespace FinalProject.GameComponents
{
    internal class ComponentAddWorthOnDeath : Component
    {
        private int worth;

        public ComponentAddWorthOnDeath(Entity entity, int worth)
            : base(entity)
        {
            entity.MessageCenter.AddListener("Health Depleted", AddWorth);
            this.worth = worth;
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener("Health Depleted", AddWorth);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
        }

        private void AddWorth()
        {
            ScreenGame.MessageCenter.Broadcast<int>("Add Worth", worth);
        }
    }
}