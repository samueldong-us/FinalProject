using FinalProject.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class Entity
    {
        public MessageCenter messageCenter;
        private List<Component> components;

        public Entity()
        {
            components = new List<Component>();
            messageCenter = new MessageCenter();
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