using FinalProject.Messaging;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class OnScreenTransformComponent : TransformComponent
    {
        private int xPad, yPad;

        public OnScreenTransformComponent(MessageCenter messageCenter, int xPad, int yPad)
            : base(messageCenter)
        {
            this.xPad = xPad;
            this.yPad = yPad;
        }

        public override void Update(float secondsPassed)
        {
            Position.X = (int)MathHelper.Clamp(Position.X + velocity.X * secondsPassed, xPad, Constants.VirtualWidth - xPad);
            Position.Y = (int)MathHelper.Clamp(Position.Y + velocity.Y * secondsPassed, yPad, Constants.VirtualHeight - yPad);
            Console.Write(Position);
        }
    }
}