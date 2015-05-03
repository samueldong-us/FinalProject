using FinalProject.GameSaving;
using FinalProject.GameWaves;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal static class UnitFactory
    {
        public static SaveGame.Difficulty Difficulty;

        public static int Stage;

        public static Entity CreateFromSpawnInformation(SpawnInformation spawnInformation)
        {
            switch (spawnInformation.GetInformation<string>("Unit Type"))
            {
                case "Jellyfish":
                    {
                        Vector2 spawnPosition = spawnInformation.GetInformation<Vector2>("Spawn Position");
                        Vector2 shootPosition = spawnInformation.GetInformation<Vector2>("Shoot Position");
                        List<Vector2> path = spawnInformation.GetInformation<List<Vector2>>("Path");
                        return CreateJellyFish(spawnPosition, shootPosition, path);
                    }
                case "Walking Fish01":
                    {
                        Vector2 spawnPosition = spawnInformation.GetInformation<Vector2>("Spawn Position");
                        Vector2 shootPosition = spawnInformation.GetInformation<Vector2>("Shoot Position");
                        return CreateWalkingFish(spawnPosition, shootPosition);
                    }
            }
            return null;
        }

        public static Entity CreateJellyFish(Vector2 spawnPosition, Vector2 shootPosition, List<Vector2> path)
        {
            int health;
            int numberOfBullets;
            int damage;
            float fireRate;
            GetJellyFishValues(out health, out numberOfBullets, out damage, out fireRate);
            Entity jellyfish = new Entity();
            jellyfish.Position = path[0];
            jellyfish.Rotation = (float)(Math.PI / 2);
            new ComponentVelocityAcceleration(jellyfish, Vector2.Zero, Vector2.Zero);
            new ComponentBehaviorCatmullRom(jellyfish, path, 150, .1f, .9f);
            new ComponentConstantRateFire(jellyfish, fireRate);
            new ComponentProjectileWeaponCircularBounce(jellyfish, numberOfBullets, damage, (float)Math.PI / 6);
            new ComponentCollider(jellyfish, GameAssets.Unit["Jelly"], GameAssets.UnitTriangles["Jelly"], "Enemy");
            new ComponentHealth(jellyfish, health);
            new ComponentHealthBar(jellyfish, new Rectangle(0, 0, 100, 7), new Vector2(0, -50));
            new ComponentRemoveOnDeath(jellyfish);
            new ComponentTextureRenderer(jellyfish, GameAssets.UnitTexture, GameAssets.Unit["Jelly"], Color.White, "Enemy");
            return jellyfish;
        }

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

        public static Entity CreateWalkingFish(Vector2 spawnPosition, Vector2 shootPosition)
        {
            int health;
            int numberOfBullets;
            int damage;
            float fireRate;
            GetWalkingFishValues(out health, out numberOfBullets, out damage, out fireRate);
            Entity walkingfish = new Entity();
            walkingfish.Position = spawnPosition;
            walkingfish.Rotation = (float)(Math.PI / 2);
            new ComponentVelocityAcceleration(walkingfish, Vector2.Zero, Vector2.Zero);
            new ComponentBehaviorInFireOut(walkingfish, shootPosition, 200, 50, 700);
            new ComponentConstantRateFire(walkingfish, 2f);
            new ComponentProjectileWeaponCircularBounce(walkingfish, numberOfBullets, damage, (float)Math.PI / 6);
            new ComponentCollider(walkingfish, GameAssets.Unit["Walking Fish01"], GameAssets.UnitTriangles["Walking Fish01"], "Enemy");
            new ComponentHealth(walkingfish, health);
            new ComponentHealthBar(walkingfish, new Rectangle(0, 0, 100, 7), new Vector2(0, -50));
            new ComponentRemoveOnDeath(walkingfish);
            new ComponentTextureRenderer(walkingfish, GameAssets.UnitTexture, GameAssets.Unit["Walking Fish01"], Color.White, "Enemy");
            return walkingfish;
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

        private static void GetJellyFishValues(out int health, out int numberOfBullets, out int damage, out float fireRate)
        {
            health = 1;
            numberOfBullets = 1;
            damage = 1;
            fireRate = 1;
            switch (Difficulty)
            {
                case SaveGame.Difficulty.Easy:
                    {
                        switch (Stage)
                        {
                            case 1:
                                {
                                    health = 30;
                                    numberOfBullets = 25;
                                    damage = 1;
                                    fireRate = 3;
                                } break;
                            case 2:
                                {
                                } break;
                            case 3:
                                {
                                } break;
                        }
                    } break;
                case SaveGame.Difficulty.Normal:
                    {
                        switch (Stage)
                        {
                            case 1:
                                {
                                    health = 45;
                                    numberOfBullets = 35;
                                    damage = 2;
                                    fireRate = 2;
                                } break;
                            case 2:
                                {
                                } break;
                            case 3:
                                {
                                } break;
                        }
                    } break;
                case SaveGame.Difficulty.Hard:
                    {
                        switch (Stage)
                        {
                            case 1:
                                {
                                    health = 60;
                                    numberOfBullets = 50;
                                    damage = 3;
                                    fireRate = 1;
                                } break;
                            case 2:
                                {
                                } break;
                            case 3:
                                {
                                } break;
                        }
                    } break;
            }
        }

        private static void GetWalkingFishValues(out int health, out int numberOfBullets, out int damage, out float fireRate)
        {
            health = 1;
            numberOfBullets = 1;
            damage = 1;
            fireRate = 1;
            switch (Difficulty)
            {
                case SaveGame.Difficulty.Easy:
                    {
                        switch (Stage)
                        {
                            case 1:
                                {
                                    health = 30;
                                    numberOfBullets = 3;
                                    damage = 1;
                                    fireRate = 3;
                                } break;
                            case 2:
                                {
                                } break;
                            case 3:
                                {
                                } break;
                        }
                    } break;
                case SaveGame.Difficulty.Normal:
                    {
                        switch (Stage)
                        {
                            case 1:
                                {
                                    health = 45;
                                    numberOfBullets = 5;
                                    damage = 2;
                                    fireRate = 2;
                                } break;
                            case 2:
                                {
                                } break;
                            case 3:
                                {
                                } break;
                        }
                    } break;
                case SaveGame.Difficulty.Hard:
                    {
                        switch (Stage)
                        {
                            case 1:
                                {
                                    health = 60;
                                    numberOfBullets = 7;
                                    damage = 3;
                                    fireRate = 1;
                                } break;
                            case 2:
                                {
                                } break;
                            case 3:
                                {
                                } break;
                        }
                    } break;
            }
        }
    }
}