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

        protected ComponentProjectileWeapon(Entity entity, Vector2 offset)
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
            new ComponentCollider(projectile, source, triangles, colliderLayer);
            new ComponentVelocityAcceleration(projectile, UtilitiesMath.VectorFromMagnitudeAndAngle(speed, rotation), Vector2.Zero);
            new ComponentVelocityBasedRotation(projectile);
            new ComponentSignalOnExit(projectile, source, ScreenGame.Bounds);
            new ComponentRemoveOnExit(projectile);
            new ComponentRemoveOnCollide(projectile);
            new ComponentTextureRenderer(projectile, texture, source, tint, drawingLayer);
            return projectile;
        }

        protected abstract void Fire();
    }
}