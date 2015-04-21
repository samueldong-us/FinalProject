using FinalProject.Messaging;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class Entity
    {
        public MessageCenter MessageCenter;
        private List<Component> components;

        public Entity()
        {
            components = new List<Component>();
            MessageCenter = new MessageCenter();
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public void Dispose()
        {
            foreach (Component component in components)
            {
                component.Dispose();
            }
        }

        public void Update(float secondsPassed)
        {
            foreach (Component component in components)
            {
                component.Update(secondsPassed);
            }
        }
    }
}