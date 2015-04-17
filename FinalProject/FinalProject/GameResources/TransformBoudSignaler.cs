using FinalProject.Messaging;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class TransformBoundSignaler : TransformComponent
    {
        private Rectangle boundingBox;
        private Entity currentGameObject;
        private MessageCenter messageOut;

        public TransformBoundSignaler(MessageCenter messageCenter, MessageCenter messageOut, int leftBound, int rightBound, int upperBound, int lowerBound, Entity gameWorldObject)
            : base(messageCenter)
        {
            boundingBox = new Rectangle(leftBound, upperBound, rightBound-leftBound, lowerBound-upperBound);
            currentGameObject = gameWorldObject;
            this.messageOut = messageOut;
        }

        public override void Update(float secondsPassed)
        {
            base.Update(secondsPassed);
            if (!boundingBox.Contains((int) Position.X, (int) Position.Y)) {
                messageOut.Broadcast<Entity>("out of bounds", currentGameObject);
            }
        }
    }
}