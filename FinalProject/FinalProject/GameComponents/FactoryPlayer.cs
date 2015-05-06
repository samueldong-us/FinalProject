using FinalProject.GameSaving;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

namespace FinalProject.GameComponents
{
    internal static class FactoryPlayer
    {
        public static Entity CreatePlayer(SaveGame saveGame)
        {
            Entity player = new Entity();
            player.Position = new Vector2(700, 700);
            player.Rotation = (float)(-Math.PI / 2);
            switch (saveGame.character)
            {
                case SaveGame.Character.Dimmy:
                    {
                        CreateSpreadShotShip(saveGame, player);
                    } break;
                case SaveGame.Character.Oason:
                    {
                        CreateHomingShip(saveGame, player);
                    } break;
                case SaveGame.Character.Varlet:
                    {
                        CreateLaserShip(saveGame, player);
                    } break;
            }
            return player;
        }

        private static void CreateHomingShip(SaveGame saveGame, Entity player)
        {
            float movementSpeed = 200 + 20 * saveGame.MovementSpeed;
            int damage = 1 + saveGame.Damage / 4;
            int health = 20 + 2 * saveGame.Shields;
            float firerate = .5f - .25f * saveGame.FireRate / 10;
            new ComponentPlayerController(player, movementSpeed);
            new ComponentVelocityAcceleration(player, Vector2.Zero, Vector2.Zero);
            new ComponentRestrictPosition(player, 50, 50, ScreenGame.Bounds);
            new ComponentConstantRateFire(player, firerate);
            new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2), new Vector2(0, -20));
            new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2 + Math.PI / 32), new Vector2(8, -20));
            new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2 - Math.PI / 32), new Vector2(-8, -20));
            if (saveGame.WeaponStrength > 3)
            {
                new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2), new Vector2(12, -10));
                new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2), new Vector2(-12, -10));
            }
            if (saveGame.WeaponStrength > 6)
            {
                new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2 + Math.PI / 10), new Vector2(21, -15));
                new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2 - Math.PI / 10), new Vector2(-21, -15));
            }
            if (saveGame.WeaponStrength == 10)
            {
                new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2), new Vector2(28, -15));
                new ComponentProjectileWeaponHoming(player, damage, (float)(-Math.PI / 2), new Vector2(-28, -15));
            }
            new ComponentCollider(player, GameAssets.Unit["Homing Ship"], GameAssets.UnitTriangles["Homing Ship"], "Player");
            new ComponentHealth(player, health);
            new ComponentRemoveOnDeath(player);
            new ComponentTextureRenderer(player, GameAssets.UnitTexture, GameAssets.Unit["Homing Ship"], Color.White, "Player");
        }

        private static void CreateLaserShip(SaveGame saveGame, Entity player)
        {
            float movementSpeed = 200 + 20 * saveGame.MovementSpeed;
            float damagePerSecond = (10 + 2f * saveGame.Damage + 2f * saveGame.WeaponStrength) * (1.5f + saveGame.FireRate / 10.0f);
            int health = 20 + 2 * saveGame.Shields;
            new ComponentPlayerController(player, movementSpeed);
            new ComponentVelocityAcceleration(player, Vector2.Zero, Vector2.Zero);
            new ComponentRestrictPosition(player, 50, 50, ScreenGame.Bounds);
            new ComponentWeaponLaser(player, damagePerSecond);
            new ComponentCollider(player, GameAssets.Unit["Laser Ship"], GameAssets.UnitTriangles["Laser Ship"], "Player");
            new ComponentHealth(player, health);
            new ComponentRemoveOnDeath(player);
            new ComponentTextureRenderer(player, GameAssets.UnitTexture, GameAssets.Unit["Laser Ship"], Color.White, "Player");
        }

        private static void CreateSpreadShotShip(SaveGame saveGame, Entity player)
        {
            float movementSpeed = 200 + 20 * saveGame.MovementSpeed;
            int damage = 1 + saveGame.Damage / 4;
            int health = 20 + 2 * saveGame.Shields;
            float firerate = .2f - .1f * saveGame.FireRate / 10;
            new ComponentPlayerController(player, movementSpeed);
            new ComponentVelocityAcceleration(player, Vector2.Zero, Vector2.Zero);
            new ComponentRestrictPosition(player, 50, 50, ScreenGame.Bounds);
            new ComponentConstantRateFire(player, firerate);
            new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2), new Vector2(0, -35));
            new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 + Math.PI / 32), new Vector2(10, 0));
            new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 - Math.PI / 32), new Vector2(-10, 0));
            if (saveGame.WeaponStrength > 3)
            {
                new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 + Math.PI / 20), new Vector2(15, -10));
                new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 - Math.PI / 20), new Vector2(-15, -10));
            }
            if (saveGame.WeaponStrength > 6)
            {
                new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 + Math.PI / 10), new Vector2(22, -10));
                new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 - Math.PI / 10), new Vector2(-22, -10));
            }
            if (saveGame.WeaponStrength == 10)
            {
                new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 + Math.PI / 5), new Vector2(30, -20));
                new ComponentProjectileWeaponSpreadShot(player, damage, (float)(-Math.PI / 2 - Math.PI / 5), new Vector2(-30, -20));
            }
            new ComponentCollider(player, GameAssets.Unit["Spread Shot Ship"], GameAssets.UnitTriangles["Spread Shot Ship"], "Player");
            new ComponentHealth(player, health);
            new ComponentRemoveOnDeath(player);
            new ComponentTextureRenderer(player, GameAssets.UnitTexture, GameAssets.Unit["Spread Shot Ship"], Color.White, "Player");
        }
    }
}