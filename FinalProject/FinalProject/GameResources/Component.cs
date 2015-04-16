using FinalProject.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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