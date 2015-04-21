using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class ExitSignalingTransformComponent : TransformComponent
    {
        private Entity entity;
        private Rectangle gameBounds;
        private Rectangle objectBounds;

        public ExitSignalingTransformComponent(Entity entity, Rectangle objectBounds, Rectangle gameBounds)
            : base(entity.MessageCenter)
        {
            this.objectBounds = objectBounds;
            this.gameBounds = gameBounds;
            this.entity = entity;
        }

        public override void Update(float secondsPassed)
        {
            base.Update(secondsPassed);
            Rectangle transformedObjectBounds = new Rectangle(objectBounds.X - objectBounds.Width / 2 + (int)Position.X, objectBounds.Y - objectBounds.Height / 2 + (int)Position.Y, objectBounds.Width, objectBounds.Height);
            if (!gameBounds.Intersects(transformedObjectBounds) && !gameBounds.Contains(transformedObjectBounds))
            {
                messageCenter.Broadcast<Entity>("Exited", entity);
            }
        }
    }
}