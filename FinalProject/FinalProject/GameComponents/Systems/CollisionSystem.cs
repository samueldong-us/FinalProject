using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class CollisionSystem
    {
        private Dictionary<string, List<ColliderComponent>> colliderLayers;
        private string[] Layers = { "Enemy", "EnemyBullet", "Player", "PlayerBullet" };

        public CollisionSystem()
        {
            colliderLayers = new Dictionary<string, List<ColliderComponent>>();
            foreach (string layer in Layers)
            {
                colliderLayers[layer] = new List<ColliderComponent>();
            }
        }

        public void AddCollider(string layer, ColliderComponent collider)
        {
            colliderLayers[layer].Add(collider);
        }

        public void ClosestEnemyByAngle(Entity entity, float maxAngle)
        {
            ColliderComponent closest = null;
            float closestAngle = (float)(Math.PI);
            Vector2 entityVector = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(entity.Rotation));
            foreach (ColliderComponent collider in colliderLayers["Enemy"])
            {
                if (GameScreen.Bounds.Contains((int)collider.GetEntity().Position.X, (int)collider.GetEntity().Position.Y))
                {
                    Vector2 fromTo = collider.GetEntity().Position - entity.Position;
                    float angleBetween = MathUtilities.AngleBetween(entityVector, fromTo);
                    if (angleBetween < maxAngle && angleBetween < closestAngle)
                    {
                        closest = collider;
                        closestAngle = angleBetween;
                    }
                }
            }
            Vector2 result = closest == null ? new Vector2(-1, -1) : closest.GetEntity().Position;
            entity.MessageCenter.Broadcast<Vector2>("Closest Enemy", result);
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

        public void RemoveCollider(string layer, ColliderComponent collider)
        {
            colliderLayers[layer].Remove(collider);
        }

        public void Update()
        {
            foreach (ColliderComponent player in colliderLayers["Player"])
            {
                foreach (ColliderComponent enemyBullet in colliderLayers["EnemyBullet"])
                {
                    if (enemyBullet.CollidesWith(player))
                    {
                        enemyBullet.NotifyOfCollision(player.GetEntity());
                        player.NotifyOfCollision(enemyBullet.GetEntity());
                    }
                }
            }
            foreach (ColliderComponent player in colliderLayers["Player"])
            {
                foreach (ColliderComponent enemy in colliderLayers["Enemy"])
                {
                    if (player.CollidesWith(enemy))
                    {
                        enemy.NotifyOfCollision(player.GetEntity());
                        player.NotifyOfCollision(enemy.GetEntity());
                    }
                }
            }
            foreach (ColliderComponent enemy in colliderLayers["Enemy"])
            {
                foreach (ColliderComponent playerBullet in colliderLayers["PlayerBullet"])
                {
                    if (playerBullet.CollidesWith(enemy))
                    {
                        playerBullet.NotifyOfCollision(enemy.GetEntity());
                        enemy.NotifyOfCollision(playerBullet.GetEntity());
                    }
                }
            }
        }

        private Vector2 ClosestCollider(Entity entity, List<ColliderComponent> colliderList, float maxAngle)
        {
            ColliderComponent closest = null;
            Vector2 closestFromTo = Vector2.Zero;
            Vector2 entityVector = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(entity.Rotation));
            foreach (ColliderComponent collider in colliderList)
            {
                Vector2 fromTo = collider.GetEntity().Position - entity.Position;
                if (MathUtilities.AngleBetween(entityVector, fromTo) < maxAngle)
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