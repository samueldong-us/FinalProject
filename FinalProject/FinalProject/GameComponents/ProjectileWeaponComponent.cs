using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal abstract class ProjectileWeaponComponent : Component
    {
        protected Vector2 offset;

        protected List<Entity> projectiles;

        protected List<Entity> toRemove;

        protected ProjectileWeaponComponent(Entity entity, Vector2 offset)
            : base(entity)
        {
            this.offset = offset;
            projectiles = new List<Entity>();
            toRemove = new List<Entity>();
            entity.MessageCenter.AddListener("Fire", Fire);
            entity.MessageCenter.AddListener("Clean Up", CleanUp);
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
        }

        protected void CleanUp()
        {
            foreach (Entity projectile in toRemove)
            {
                projectiles.Remove(projectile);
                projectile.Dispose();
            }
            toRemove.Clear();
        }

        protected Entity CreateProjectile(float speed, float rotation, Texture2D texture, Rectangle source, List<Triangle> triangles, Color tint, List<Drawable> drawingLayer, List<ColliderComponent> colliderList)
        {
            Entity projectile = new Entity();
            projectile.Position = entity.Position + offset;
            new ColliderComponent(projectile, source, triangles, colliderList).DebugDraw();
            new VelocityAccelerationComponent(projectile, MathUtilities.VectorFromMagnitudeAndAngle(speed, rotation), Vector2.Zero);
            new VelocityBasedRotationComponent(projectile);
            new SignalOnExitComponent(projectile, source, GameScreen.Bounds);
            new TextureRendererComponent(projectile, texture, source, tint, drawingLayer);
            return projectile;
        }

        protected abstract void Fire();

        protected void RemoveProjectile(Entity projectile)
        {
            toRemove.Add(projectile);
        }
    }
}