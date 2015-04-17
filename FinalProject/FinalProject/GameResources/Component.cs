using FinalProject.Messaging;

namespace FinalProject.GameResources
{
    internal abstract class Component
    {
        protected MessageCenter messageCenter;

        public Component(MessageCenter messageCenter)
        {
            this.messageCenter = messageCenter;
        }

        public abstract void Dispose();

        public abstract void Update(float secondsPassed);
    }
}