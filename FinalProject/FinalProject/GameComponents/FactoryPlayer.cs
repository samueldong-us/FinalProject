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
                        CreateLaserShip(saveGame, player);
                    } break;
                case SaveGame.Character.Oason:
                    {
                        CreateLaserShip(saveGame, player);
                    } break;
                case SaveGame.Character.Varlet:
                    {
                        CreateLaserShip(saveGame, player);
                    } break;
            }
            return player;
        }

        private static void CreateLaserShip(SaveGame saveGame, Entity player)
        {
            float movementSpeed = 200 + 20 * saveGame.MovementSpeed;
            float damagePerSecond = (10 + 1 * saveGame.Damage + 1 * saveGame.WeaponStrength) * (1 + saveGame.FireRate / 10.0f);
            int health = 20 + 2 * saveGame.Shields;
            new ComponentPlayerController(player, movementSpeed);
            new ComponentVelocityAcceleration(player, Vector2.Zero, Vector2.Zero);
            new ComponentRestrictPosition(player, 50, 50, ScreenGame.Bounds);
            new ComponentWeaponLaser(player, damagePerSecond);
            new ComponentCollider(player, GameAssets.Unit["Laser Ship"], GameAssets.UnitTriangles["Laser Ship"], "Player");
            new ComponentHealth(player, health);
            new ComponentHealthBarCircular(player, (float)(Math.PI * 4 / 5));
            new ComponentRemoveOnDeath(player);
            new ComponentTextureRenderer(player, GameAssets.UnitTexture, GameAssets.Unit["Laser Ship"], Color.White, "Player");
        }
    }
}