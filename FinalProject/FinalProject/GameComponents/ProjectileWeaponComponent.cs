using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            }
            toRemove.Clear();
        }

        protected Entity CreateProjectile(float speed, float rotation, Texture2D texture, Rectangle source, List<Triangle> triangles, List<Drawable> drawingLayer, List<ColliderComponent> colliderList)
        {
            Entity projectile = new Entity();
            new ColliderComponent(projectile, source, triangles, colliderList);
            new VelocityAcclerationComponent(projectile, VectorFromMagnitude(speed, rotation), Vector2.Zero);
            new SignalOnExitComponent(projectile, source, GameScreen.Bounds);
            new TextureRendererComponent(projectile, texture, source, drawingLayer);
            return projectile;
        }

        protected abstract void Fire();

        protected Vector2 VectorFromMagnitude(float magnitude, float angle)
        {
            return new Vector2((float)(magnitude * Math.Cos(angle)), (float)(magnitude * Math.Sin(angle)));
        }
    }
}