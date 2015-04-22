using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class ColliderComponent : Component
    {
        private int boundingRadius;
        private List<ColliderComponent> colliderList;
        private Color[,] texture;
        private List<Triangle> triangles;
        private int width, height;

        public ColliderComponent(Entity entity, int boundingRadius, int width, int height, List<Triangle> triangles, TransformComponent transform, List<ColliderComponent> colliderList)
            : base(entity.MessageCenter)
        {
            this.boundingRadius = boundingRadius;
            this.triangles = triangles;
            this.transform = transform;
            this.colliderList = colliderList;
            colliderList.Add(this);
            this.Entity = entity;
            this.width = width;
            this.height = height;
        }

        public Entity Entity { get; private set; }

        public TransformComponent transform { get; private set; }

        public bool CollidesWith(ColliderComponent other)
        {
            TransformComponent otherTransform = other.transform;
            if (Vector2.DistanceSquared(transform.Position, otherTransform.Position) < (boundingRadius + other.boundingRadius) * (boundingRadius + other.boundingRadius))
            {
                foreach (Triangle triangle in TransformedTriangles())
                {
                    foreach (Triangle otherTriangle in other.TransformedTriangles())
                    {
                        if (triangle.Intersects(otherTriangle))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }

        public override void Dispose()
        {
            colliderList.Remove(this);
        }

        public void NotifyOfCollision(Entity collidedWith)
        {
            messageCenter.Broadcast<Entity, Entity>("Collided With", collidedWith, Entity);
        }

        public List<Triangle> TransformedTriangles()
        {
            List<Triangle> transformed = new List<Triangle>();
            foreach (Triangle triangle in triangles)
            {
                transformed.Add(triangle.Transform(ToScreenMatrix()));
            }
            return transformed;
        }

        public override void Update(float secondsPassed)
        {
        }

        private Matrix ToScreenMatrix()
        {
            return Matrix.CreateTranslation(-width / 2, -height / 2, 0) * Matrix.CreateScale(transform.Scale) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Theta)) * Matrix.CreateTranslation(transform.Position.X, transform.Position.Y, 0);
        }
    }
}