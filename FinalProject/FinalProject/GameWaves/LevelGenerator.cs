using FinalProject.GameComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal static class LevelGenerator
    {
        private const float LengthOfWave = 5;

        public static List<Wave> GenerateLevel1()
        {
            Dictionary<string, int> listOfUnits = new Dictionary<string, int>();
            listOfUnits["Jellyfish"] = 6;
            listOfUnits["Walking Fish01"] = 50;
            Dictionary<string, int> worthOfUnits = new Dictionary<string, int>();
            worthOfUnits["Jellyfish"] = 6;
            worthOfUnits["Walking Fish01"] = 1;
            SpawnInformation jellyfishDefault = new SpawnInformation(0);
            jellyfishDefault.AddInformation("Unit Name", "Jellyfish");
            jellyfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            jellyfishDefault.AddInformation("Rotate Based On Velocity", false);
            SpawnInformation walkingfishDefault = new SpawnInformation(0);
            walkingfishDefault.AddInformation("Unit Name", "Walking Fish01");
            walkingfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            walkingfishDefault.AddInformation("Rotate Based On Velocity", true);
            Dictionary<string, SpawnInformation> spawnInformation = new Dictionary<string, SpawnInformation>();
            spawnInformation["Jellyfish"] = jellyfishDefault;
            spawnInformation["Walking Fish01"] = walkingfishDefault;
            Dictionary<string, List<string>> possibleWeapons = new Dictionary<string, List<string>>();
            possibleWeapons["Jellyfish"] = new List<string> { "Circular Fire", "Circular Bounce" };
            possibleWeapons["Walking Fish01"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            Dictionary<string, List<string>> possibleBehaviors = new Dictionary<string, List<string>>();
            possibleBehaviors["Jellyfish"] = new List<string> { "In Fire Out" };
            possibleBehaviors["Walking Fish01"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            return CreateLevel(listOfUnits, worthOfUnits, spawnInformation, possibleWeapons, possibleBehaviors, 8);
        }

        private static void AddBehaviorToSpawn(string behavior, SpawnInformation spawnInformation)
        {
            switch (behavior)
            {
                case "In Fire Out":
                    {
                        Vector4 spawnAndSwitch = BehaviorInformationGenerator.RandomSpawnAndSwitchPosition();
                        spawnInformation.AddInformation("Behavior Name", "In Fire Out");
                        spawnInformation.AddInformation("Spawn Position", new Vector2(spawnAndSwitch.X, spawnAndSwitch.Y));
                        spawnInformation.AddInformation("Switch Position", new Vector2(spawnAndSwitch.Z, spawnAndSwitch.W));
                        spawnInformation.AddInformation("Cease Fire Y", 700);
                    } break;
                case "Loop Back":
                    {
                        spawnInformation.AddInformation("Behavior Name", "Catmull Rom");
                        spawnInformation.AddInformation("Path", BehaviorInformationGenerator.GenerateLoopBack());
                        spawnInformation.AddInformation("Start Firing Percentage", .1f);
                        spawnInformation.AddInformation("Stop Firing Percentage", .9f);
                    } break;
                case "Sigmoid":
                    {
                        spawnInformation.AddInformation("Behavior Name", "Catmull Rom");
                        spawnInformation.AddInformation("Path", BehaviorInformationGenerator.GenerateSigmoid());
                        spawnInformation.AddInformation("Start Firing Percentage", .1f);
                        spawnInformation.AddInformation("Stop Firing Percentage", .9f);
                    } break;
                case "Loop Straight":
                    {
                        spawnInformation.AddInformation("Behavior Name", "Catmull Rom");
                        spawnInformation.AddInformation("Path", BehaviorInformationGenerator.GenerateStraightWithLoop());
                        spawnInformation.AddInformation("Start Firing Percentage", .1f);
                        spawnInformation.AddInformation("Stop Firing Percentage", .9f);
                    } break;
            }
        }

        private static void AddWeaponToSpawn(string weapon, SpawnInformation spawnInformation)
        {
            spawnInformation.AddInformation("Weapon Name", weapon);
        }

        private static List<Wave> CreateLevel(Dictionary<string, int> listOfUnits, Dictionary<string, int> worthOfUnits, Dictionary<string, SpawnInformation> spawnInformation, Dictionary<string, List<string>> possibleWeapons, Dictionary<string, List<string>> possibleBehaviors, int numberOfWaves)
        {
            List<Wave> waves = new List<Wave>();
            for (int i = 0; i < numberOfWaves; i++)
            {
                waves.Add(new Wave());
            }
            int total = 0;
            foreach (string unit in listOfUnits.Keys)
            {
                total += listOfUnits[unit] * worthOfUnits[unit];
            }
            List<int> distribution = GenerateWaveDistribution(total, numberOfWaves);
            List<string> sortedByWorth = new List<string>();
            while (sortedByWorth.Count < worthOfUnits.Keys.Count)
            {
                string highestWorth = "";
                int worth = 0;
                foreach (string unit in worthOfUnits.Keys)
                {
                    if (!sortedByWorth.Contains(unit) && worthOfUnits[unit] > worth)
                    {
                        highestWorth = unit;
                        worth = worthOfUnits[unit];
                    }
                }
                sortedByWorth.Add(highestWorth);
            }
            foreach (string unit in sortedByWorth)
            {
                for (int i = 0; i < listOfUnits[unit]; i++)
                {
                    List<int> possibleWaves = new List<int>();
                    int distributionRange = 0;
                    for (int j = 0; j < distribution.Count; j++)
                    {
                        if (worthOfUnits[unit] <= distribution[j])
                        {
                            possibleWaves.Add(j);
                            distributionRange += distribution[j];
                        }
                    }
                    int generatedNumber = GameMain.RNG.Next(distributionRange);
                    foreach (int possibleWave in possibleWaves)
                    {
                        if (generatedNumber < distribution[possibleWave])
                        {
                            distribution[possibleWave] -= worthOfUnits[unit];
                            SpawnInformation clone = spawnInformation[unit].Clone();
                            clone.SpawnTime = (float)GameMain.RNG.NextDouble() * LengthOfWave;
                            int weaponIndex = GameMain.RNG.Next(possibleWeapons[unit].Count);
                            int behaviorIndex = GameMain.RNG.Next(possibleBehaviors[unit].Count);
                            AddBehaviorToSpawn(possibleBehaviors[unit][behaviorIndex], clone);
                            AddWeaponToSpawn(possibleWeapons[unit][weaponIndex], clone);
                            waves[possibleWave].AddSpawnInformation(clone);
                            break;
                        }
                        else
                        {
                            generatedNumber -= distribution[possibleWave];
                        }
                    }
                }
            }
            return waves;
        }

        private static List<int> GenerateWaveDistribution(int total, int number)
        {
            float min = total / (number * 3 / 2f);
            float change = min / number;
            List<int> distribution = new List<int>();
            int leftOver = total;
            float current = min;
            for (int i = 0; i < number; i++, current += change)
            {
                distribution.Add((int)Math.Round(current));
                leftOver -= (int)Math.Round(current);
            }
            distribution[number - 1] += leftOver;
            for (int i = 0; i < (int)min; i++)
            {
                int firstIndex = GameMain.RNG.Next(number);
                int secondIndex = GameMain.RNG.Next(number);
                distribution[firstIndex]++;
                distribution[secondIndex]--;
            }
            return distribution;
        }
    }
}