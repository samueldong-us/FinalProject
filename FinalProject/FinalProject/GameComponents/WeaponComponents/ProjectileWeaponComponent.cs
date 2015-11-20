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

        protected ProjectileWeaponComponent(Entity entity, Vector2 offset)
            : base(entity)
        {
            this.offset = offset;
            entity.MessageCenter.AddListener("Fire", Fire);
        }

        public override void Dispose()
        {
            entity.MessageCenter.RemoveListener("Fire", Fire);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
        }

        protected Entity CreateProjectile(float speed, float rotation, Texture2D texture, Rectangle source, List<Triangle> triangles, Color tint, string drawingLayer, string colliderLayer)
        {
            Entity projectile = new Entity();
            projectile.Position = entity.Position + offset;
            projectile.Rotation = rotation;
            new ColliderComponent(projectile, source, triangles, colliderLayer);
            new VelocityAccelerationComponent(projectile, MathUtilities.VectorFromMagnitudeAndAngle(speed, rotation), Vector2.Zero);
            new VelocityBasedRotationComponent(projectile);
            new SignalOnExitComponent(projectile, source, GameScreen.Bounds);
            new RemoveOnExitComponent(projectile);
            new RemoveOnCollideComponent(projectile);
            new TextureRendererComponent(projectile, texture, source, tint, drawingLayer);
            return projectile;
        }

        protected abstract void Fire();
    }
}