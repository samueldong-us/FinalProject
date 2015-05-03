using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal abstract class ComponentProjectileWeapon : Component
    {
        protected Vector2 offset;

        protected List<Entity> projectiles;

        protected List<Entity> toRemove;

        protected ComponentProjectileWeapon(Entity entity, Vector2 offset)
            : base(entity)
        {
            this.offset = offset;
            projectiles = new List<Entity>();
            toRemove = new List<Entity>();
            entity.MessageCenter.AddListener("Fire", Fire);
        }

        public override void Dispose()
        {
            foreach (Entity projectile in projectiles)
            {
                projectile.Dispose();
            }
            entity.MessageCenter.RemoveListener("Fire", Fire);
        }

        public override void Update(float secondsPassed)
        {
            foreach (Entity projectile in projectiles)
            {
                projectile.Update(secondsPassed);
            }
            foreach (Entity projectile in toRemove)
            {
                projectiles.Remove(projectile);
                projectile.Dispose();
            }
            toRemove.Clear();
        }

        protected Entity CreateProjectile(float speed, float rotation, Texture2D texture, Rectangle source, List<Triangle> triangles, Color tint, string drawingLayer, string colliderLayer)
        {
            Entity projectile = new Entity();
            projectile.Position = entity.Position + offset;
            new ComponentCollider(projectile, source, triangles, colliderLayer);
            new ComponentVelocityAcceleration(projectile, UtilitiesMath.VectorFromMagnitudeAndAngle(speed, rotation), Vector2.Zero);
            new ComponentVelocityBasedRotation(projectile);
            new ComponentSignalOnExit(projectile, source, ScreenGame.Bounds);
            new ComponentTextureRenderer(projectile, texture, source, tint, drawingLayer);
            return projectile;
        }

        protected abstract void Fire();

        protected void RemoveProjectile(Entity projectile)
        {
            toRemove.Add(projectile);
        }
    }
}