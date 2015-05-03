using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class ComponentProjectileWeaponSpreadShot : ComponentProjectileWeapon
    {
        private const int Speed = 1000;

        private int damage;

        private float rotation;

        public ComponentProjectileWeaponSpreadShot(Entity entity, int damage, float rotation, Vector2 offset)
            : base(entity, offset)
        {
            this.damage = damage;
            this.rotation = rotation;
        }

        protected override void Fire()
        {
            Entity bullet = CreateProjectile(Speed, rotation, GameAssets.BulletTexture, GameAssets.Bullet[0], GameAssets.BulletTriangles[0], Color.Red, "PlayerBullet", "PlayerBullet");
            bullet.MessageCenter.AddListener<Entity>("Exited Bounds", RemoveProjectile);
            bullet.MessageCenter.AddListener<Entity, Entity>("Collided With", DealDamage);
            projectiles.Add(bullet);
        }

        private void DealDamage(Entity projectile, Entity collidedEntity)
        {
            collidedEntity.MessageCenter.Broadcast<float>("Take Damage", damage);
            RemoveProjectile(projectile);
        }
    }
}