using FinalProject.GameSaving;
using FinalProject.GameWaves;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;

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
                        return CreateJellyFish(spawnPosition, shootPosition);
                    }
                case "Walking Fish01":
                    {
                        Vector2 spawnPosition = spawnInformation.GetInformation<Vector2>("Spawn Position");
                        Vector2 shootPosition = spawnInformation.GetInformation<Vector2>("Shoot Position");
                        return CreateWalkingFish01(spawnPosition, shootPosition);
                    }
            }
            return null;
        }

        public static Entity CreateJellyFish(Vector2 spawnPosition, Vector2 shootPosition)
        {
            int health;
            int numberOfBullets;
            int damage;
            float fireRate;
            GetJellyFishValues(out health, out numberOfBullets, out damage, out fireRate);
            Entity jellyfish = new Entity();
            jellyfish.Position = spawnPosition;
            jellyfish.Rotation = (float)(Math.PI / 2);
            new ComponentVelocityAcceleration(jellyfish, Vector2.Zero, Vector2.Zero);
            new ComponentInFireOutBehavior(jellyfish, shootPosition, 200, 50);
            new ComponentConstantRateFire(jellyfish, fireRate);
            new ComponentProjectileWeaponCircularFire(jellyfish, numberOfBullets, damage, (float)(Math.PI * Math.PI));
            new ComponentCollider(jellyfish, GameAssets.Unit["Jelly"], GameAssets.UnitTriangles["Jelly"], "Enemy");
            new ComponentHealth(jellyfish, health);
            new ComponentHealthBar(jellyfish, new Rectangle(0, 0, 100, 7), new Vector2(0, -50));
            new ComponentRemoveOnDeath(jellyfish);
            new ComponentTextureRenderer(jellyfish, GameAssets.UnitTexture, GameAssets.Unit["Jelly"], Color.White, "Enemy");
            return jellyfish;
        }

        public static Entity CreateWalkingFish01(Vector2 spawnPosition, Vector2 shootPosition)
        {
            int health;
            int damage;
            float fireRate;
            GetWalkingFish01Values(out health, out damage, out fireRate);
            Entity walkingFish01 = new Entity();
            walkingFish01.Position = spawnPosition;
            walkingFish01.Rotation = (float)(Math.PI / 2);
            new ComponentVelocityAcceleration(walkingFish01, Vector2.Zero, Vector2.Zero);
            new ComponentInFireOutBehavior(walkingFish01, shootPosition, 200, 50);
            new ComponentConstantRateFire(walkingFish01, fireRate);
            new ComponentProjectileWeaponSingleShot(walkingFish01, damage, fireRate);
            new ComponentCollider(walkingFish01, GameAssets.Unit["Walking Fish01"], GameAssets.UnitTriangles["Walking Fish01"], "Enemy");
            new ComponentHealth(walkingFish01, health);
            new ComponentHealthBar(walkingFish01, new Rectangle(0, 0, 100, 7), new Vector2(0, -50));
            new ComponentRemoveOnDeath(walkingFish01);
            new ComponentTextureRenderer(walkingFish01, GameAssets.UnitTexture, GameAssets.Unit["Walking Fish01"], Color.White, "Enemy");
            return walkingFish01;
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

        private static void GetWalkingFish01Values(out int health, out int damage, out float fireRate)
        {
            health = 1;
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