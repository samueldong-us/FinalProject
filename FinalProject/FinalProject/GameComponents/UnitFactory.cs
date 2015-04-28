using FinalProject.GameSaving;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal static class UnitFactory
    {
        public static SaveGame.Difficulty Difficulty;
        public static int Stage;

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
            new VelocityAccelerationComponent(jellyfish, Vector2.Zero, Vector2.Zero);
            new InFireOutBehaviorComponent(jellyfish, shootPosition, 200, 50);
            new ConstantRateFireComponent(jellyfish, fireRate);
            new CircularFireProjectileWeaponComponent(jellyfish, numberOfBullets, damage, 0);
            new ColliderComponent(jellyfish, GameAssets.Unit["Jelly"], GameAssets.UnitTriangles["Jelly"], GameScreen.CollidersEnemies);
            new HealthComponent(jellyfish, health);
            new HealthBarComponent(jellyfish, new Rectangle(0, 0, 100, 7), new Vector2(0, -50));
            new RemoveOnDeathComponent(jellyfish);
            new TextureRendererComponent(jellyfish, GameAssets.UnitTexture, GameAssets.Unit["Jelly"], Color.White, GameScreen.LayerEnemies);
            return jellyfish;
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
    }
}