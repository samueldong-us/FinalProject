using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class SignalOnExitComponent : Component
    {
        private Rectangle bounds;

        private Rectangle source;

        public SignalOnExitComponent(Entity entity, Rectangle source, Rectangle bounds)
            : base(entity)
        {
            this.bounds = bounds;
            this.source = source;
        }

        public override void Dispose()
        {
        }

        public override void Update(float secondsPassed)
        {
            if (!WithinBounds())
            {
                entity.MessageCenter.Broadcast<Entity>("Exited Bounds", entity);
            }
        }

        private bool WithinBounds()
        {
            Vector2 A = Vector2.Transform(new Vector2(source.Left, source.Bottom), entity.ToScreenMatrix(source));
            Vector2 B = Vector2.Transform(new Vector2(source.Right, source.Bottom), entity.ToScreenMatrix(source));
            Vector2 C = Vector2.Transform(new Vector2(source.Left, source.Top), entity.ToScreenMatrix(source));
            Vector2 D = Vector2.Transform(new Vector2(source.Right, source.Top), entity.ToScreenMatrix(source));
            return bounds.Contains((int)A.X, (int)A.Y) || bounds.Contains((int)B.X, (int)B.Y) || bounds.Contains((int)C.X, (int)C.Y) || bounds.Contains((int)D.X, (int)D.Y);
        }
    }
}