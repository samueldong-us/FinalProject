using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class SystemCollisions
    {
        private Dictionary<string, List<ComponentCollider>> colliderLayers;
        private string[] Layers = { "Enemy", "EnemyBullet", "Player", "PlayerBullet" };

        public SystemCollisions()
        {
            colliderLayers = new Dictionary<string, List<ComponentCollider>>();
            foreach (string layer in Layers)
            {
                colliderLayers[layer] = new List<ComponentCollider>();
            }
        }

        public void AddCollider(string layer, ComponentCollider collider)
        {
            colliderLayers[layer].Add(collider);
        }

        public void ClosestPlayer(Entity parameterOne)
        {
            parameterOne.MessageCenter.Broadcast<Vector2>("Closest Player", ClosestCollider(parameterOne, colliderLayers["Player"], (float)Math.PI));
        }

        public void Dispose()
        {
            foreach (string layer in colliderLayers.Keys)
            {
                colliderLayers[layer].Clear();
            }
        }

        public int GetCount(string layer)
        {
            return colliderLayers[layer].Count;
        }

        public void RemoveCollider(string layer, ComponentCollider collider)
        {
            colliderLayers[layer].Remove(collider);
        }

        public void Update()
        {
            foreach (ComponentCollider player in colliderLayers["Player"])
            {
                foreach (ComponentCollider enemyBullet in colliderLayers["EnemyBullet"])
                {
                    if (enemyBullet.CollidesWith(player))
                    {
                        enemyBullet.NotifyOfCollision(player.GetEntity());
                        player.NotifyOfCollision(enemyBullet.GetEntity());
                    }
                }
            }
            foreach (ComponentCollider player in colliderLayers["Player"])
            {
                foreach (ComponentCollider enemy in colliderLayers["Enemy"])
                {
                    if (player.CollidesWith(enemy))
                    {
                        enemy.NotifyOfCollision(player.GetEntity());
                        player.NotifyOfCollision(enemy.GetEntity());
                    }
                }
            }
            foreach (ComponentCollider enemy in colliderLayers["Enemy"])
            {
                foreach (ComponentCollider playerBullet in colliderLayers["PlayerBullet"])
                {
                    if (playerBullet.CollidesWith(enemy))
                    {
                        playerBullet.NotifyOfCollision(enemy.GetEntity());
                        enemy.NotifyOfCollision(playerBullet.GetEntity());
                    }
                }
            }
        }

        private Vector2 ClosestCollider(Entity entity, List<ComponentCollider> colliderList, float maxAngle)
        {
            ComponentCollider closest = null;
            Vector2 closestFromTo = Vector2.Zero;
            Vector2 entityVector = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(entity.Rotation));
            foreach (ComponentCollider collider in colliderList)
            {
                Vector2 fromTo = collider.GetEntity().Position - entity.Position;
                if (UtilitiesMath.AngleBetween(entityVector, fromTo) < maxAngle)
                {
                    if (closest == null || fromTo.LengthSquared() < closestFromTo.LengthSquared())
                    {
                        closest = collider;
                        closestFromTo = fromTo;
                    }
                }
            }
            return closest == null ? new Vector2(-1, -1) : closest.GetEntity().Position;
        }
    }
}