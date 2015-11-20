using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class LaserWeaponComponent : Component
    {
        private static int instances = 0;
        private float damagePerSecond;
        private Entity laser;
        private List<Entity> toDamage;

        public LaserWeaponComponent(Entity entity, float damagePerSecond)
            : base(entity)
        {
            instances++;
            toDamage = new List<Entity>();
            this.damagePerSecond = damagePerSecond;
            entity.MessageCenter.AddListener("Start Firing", StartFiring);
            entity.MessageCenter.AddListener("Stop Firing", StopFiring);
            entity.MessageCenter.AddListener("Fire", Fire);
            laser = new Entity();
            laser.Position = entity.Position + new Vector2(0, -500);
            laser.Rotation = (float)(-Math.PI / 2);
            laser.MessageCenter.AddListener<Entity, Entity>("Collided With", AddToList);
            GameScreen.Entities.AddEntity(laser);
        }

        public override void Dispose()
        {
            instances--;
            entity.MessageCenter.RemoveListener("Start Firing", StartFiring);
            entity.MessageCenter.RemoveListener("Stop Firing", StopFiring);
            entity.MessageCenter.RemoveListener("Fire", Fire);
            laser.Dispose();
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            laser.Update(secondsPassed);
            foreach (Entity entity in toDamage)
            {
                entity.MessageCenter.Broadcast<float>("Take Damage", damagePerSecond * secondsPassed);
            }
            toDamage.Clear();
        }

        protected void Fire()
        {
            GameMain.Audio.PlayOneTimeSound("Laser Sound");
        }

        private void AddToList(Entity entity, Entity collidedWith)
        {
            toDamage.Add(collidedWith);
        }

        private void StartFiring()
        {
            new FollowWithOffsetComponent(laser, entity, new Vector2(0, -500));
            new ColliderComponent(laser, GameAssets.Unit["Laser Beam"], GameAssets.UnitTriangles["Laser Beam"], "PlayerBullet");
            new TextureRendererComponent(laser, GameAssets.UnitTexture, GameAssets.Unit["Laser Beam"], Color.White, "PlayerBullet");
        }

        private void StopFiring()
        {
            laser.Dispose();
        }
    }
}