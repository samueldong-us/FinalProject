using FinalProject.Messaging;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class ColliderComponent : Component
    {
        private int boundingRadius;
        private List<ColliderComponent> colliderList;
        private Color[,] texture;
        private TransformComponent transform;

        public ColliderComponent(Entity entity, int boundingRadius, Color[,] texture, TransformComponent transform, List<ColliderComponent> colliderList)
            : base(entity.MessageCenter)
        {
            this.boundingRadius = boundingRadius;
            this.texture = texture;
            this.transform = transform;
            this.colliderList = colliderList;
            colliderList.Add(this);
            this.Entity = entity;
        }

        public Entity Entity { get; private set; }

        public bool CollidesWith(ColliderComponent other)
        {
            TransformComponent otherTransform = other.transform;
            if (Vector2.DistanceSquared(transform.Position, otherTransform.Position) < (boundingRadius + other.boundingRadius) * (boundingRadius + other.boundingRadius))
            {
                Matrix thisToOther = ToScreenMatrix() * Matrix.Invert(other.ToScreenMatrix());
                for (int x = 0; x < texture.GetLength(0); x++)
                {
                    for (int y = 0; y < texture.GetLength(1); y++)
                    {
                        if (texture[x, y].A > 0)
                        {
                            Vector2 thisPoint = new Vector2(x, y);
                            Vector2 thisPointInOther = Vector2.Transform(thisPoint, thisToOther);
                            if (new Rectangle(0, 0, other.texture.GetLength(0), other.texture.GetLength(1)).Contains((int)thisPointInOther.X, (int)thisPointInOther.Y))
                            {
                                if (other.texture[(int)thisPointInOther.X, (int)thisPointInOther.Y].A > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public override void Dispose()
        {
            colliderList.Remove(this);
        }

        public void NotifyOfCollision(Entity collidedWith)
        {
            messageCenter.Broadcast<Entity>("Collided With", collidedWith);
        }

        public override void Update(float secondsPassed)
        {
        }

        private Matrix ToScreenMatrix()
        {
            return Matrix.CreateTranslation(-texture.GetLength(0) / 2, -texture.GetLength(1) / 2, 0) * Matrix.CreateScale(transform.Scale) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Theta)) * Matrix.CreateTranslation(transform.Position.X, transform.Position.Y, 0);
        }
    }
}