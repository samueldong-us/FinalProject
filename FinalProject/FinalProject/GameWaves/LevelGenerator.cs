using FinalProject.GameComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal static class LevelGenerator
    {
        private const float LengthOfWave = 8;

        public static List<Wave> GenerateLevel1()
        {
            Dictionary<string, int> listOfUnits = new Dictionary<string, int>();
            listOfUnits["Jellyfish"] = 6;
            listOfUnits["Walking Fish01"] = 20;
            listOfUnits["Walking Fish02"] = 20;
            Dictionary<string, int> worthOfUnits = new Dictionary<string, int>();
            worthOfUnits["Jellyfish"] = 4;
            worthOfUnits["Walking Fish01"] = 1;
            worthOfUnits["Walking Fish02"] = 2;

            SpawnInformation jellyfishDefault = new SpawnInformation(0);
            jellyfishDefault.AddInformation("Unit Name", "Jellyfish");
            jellyfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            jellyfishDefault.AddInformation("Rotate Based On Velocity", false);

            SpawnInformation walkingfishDefault = new SpawnInformation(0);
            walkingfishDefault.AddInformation("Unit Name", "Walking Fish01");
            walkingfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            walkingfishDefault.AddInformation("Rotate Based On Velocity", true);

            SpawnInformation walkingfish02Default = new SpawnInformation(0);
            walkingfish02Default.AddInformation("Unit Name", "Walking Fish02");
            walkingfish02Default.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            walkingfish02Default.AddInformation("Rotate Based On Velocity", true);

            Dictionary<string, SpawnInformation> spawnInformation = new Dictionary<string, SpawnInformation>();
            spawnInformation["Jellyfish"] = jellyfishDefault;
            spawnInformation["Walking Fish01"] = walkingfishDefault;
            spawnInformation["Walking Fish02"] = walkingfish02Default;

            Dictionary<string, List<string>> possibleWeapons = new Dictionary<string, List<string>>();
            possibleWeapons["Jellyfish"] = new List<string> { "Wall Fire" };
            possibleWeapons["Walking Fish01"] = new List<string> { "Bullet" };
            possibleWeapons["Walking Fish02"] = new List<string> { "Fan" };

            Dictionary<string, List<string>> possibleBehaviors = new Dictionary<string, List<string>>();
            possibleBehaviors["Jellyfish"] = new List<string> { "In Fire Out" };
            possibleBehaviors["Walking Fish01"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Walking Fish02"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };

            return CreateLevel(listOfUnits, worthOfUnits, spawnInformation, possibleWeapons, possibleBehaviors, 8);
        }

        public static List<Wave> GenerateLevel2()
        {
            Dictionary<string, int> listOfUnits = new Dictionary<string, int>();
            listOfUnits["Jellyfish"] = 0;
            listOfUnits["Walking Fish01"] = 0;
            listOfUnits["Walking Fish02"] = 0;
            listOfUnits["Squid"] = 20;
            listOfUnits["Turtle"] = 20;
            listOfUnits["Sea Slug"] = 20;
            Dictionary<string, int> worthOfUnits = new Dictionary<string, int>();
            worthOfUnits["Jellyfish"] = 3;
            worthOfUnits["Walking Fish01"] = 1;
            worthOfUnits["Walking Fish02"] = 2;
            worthOfUnits["Squid"] = 4;
            worthOfUnits["Turtle"] = 5;
            worthOfUnits["Sea Slug"] = 3;

            SpawnInformation jellyfishDefault = new SpawnInformation(0);
            jellyfishDefault.AddInformation("Unit Name", "Jellyfish");
            jellyfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            jellyfishDefault.AddInformation("Rotate Based On Velocity", false);
            SpawnInformation walkingfishDefault = new SpawnInformation(0);
            walkingfishDefault.AddInformation("Unit Name", "Walking Fish01");
            walkingfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            walkingfishDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation walkingfish02Default = new SpawnInformation(0);
            walkingfish02Default.AddInformation("Unit Name", "Walking Fish02");
            walkingfish02Default.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            walkingfish02Default.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation squidDefault = new SpawnInformation(0);
            squidDefault.AddInformation("Unit Name", "Squid");
            squidDefault.AddInformation("Starting Rotation", -(float)(Math.PI / 2));
            squidDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation turtleDefault = new SpawnInformation(0);
            turtleDefault.AddInformation("Unit Name", "Turtle");
            turtleDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            turtleDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation seaslugDefault = new SpawnInformation(0);
            seaslugDefault.AddInformation("Unit Name", "Sea Slug");
            seaslugDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            seaslugDefault.AddInformation("Rotate Based On Velocity", true);
            Dictionary<string, SpawnInformation> spawnInformation = new Dictionary<string, SpawnInformation>();
            spawnInformation["Jellyfish"] = jellyfishDefault;
            spawnInformation["Walking Fish01"] = walkingfishDefault;
            spawnInformation["Walking Fish02"] = walkingfish02Default;
            spawnInformation["Squid"] = squidDefault;
            spawnInformation["Turtle"] = turtleDefault;
            spawnInformation["Sea Slug"] = seaslugDefault;
            Dictionary<string, List<string>> possibleWeapons = new Dictionary<string, List<string>>();
            possibleWeapons["Jellyfish"] = new List<string> { "Circular Fire", "Circular Bounce" };
            possibleWeapons["Walking Fish01"] = new List<string> { "Bullet" };
            possibleWeapons["Walking Fish02"] = new List<string> { "Fan" };
            possibleWeapons["Squid"] = new List<string> { "Bullet Stream" };
            possibleWeapons["Turtle"] = new List<string> { "Bullet Stream" };
            possibleWeapons["Sea Slug"] = new List<string> { "Bullet Stream", };
            Dictionary<string, List<string>> possibleBehaviors = new Dictionary<string, List<string>>();
            possibleBehaviors["Jellyfish"] = new List<string> { "In Fire Out" };
            possibleBehaviors["Walking Fish01"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Walking Fish02"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Squid"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Turtle"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Sea Slug"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };

            return CreateLevel(listOfUnits, worthOfUnits, spawnInformation, possibleWeapons, possibleBehaviors, 8);
        }

        public static List<Wave> GenerateLevel3()
        {
            Dictionary<string, int> listOfUnits = new Dictionary<string, int>();
            listOfUnits["Jellyfish"] = 3;
            listOfUnits["Walking Fish02"] = 1;
            listOfUnits["Squid"] = 4;
            listOfUnits["Turtle"] = 5;
            listOfUnits["Flying Fish"] = 1;
            listOfUnits["Super Jelly"] = 1;
            listOfUnits["Lobster"] = 1;
            listOfUnits["Sea Slug"] = 3;
            listOfUnits["Starfish"] = 1;
            Dictionary<string, int> worthOfUnits = new Dictionary<string, int>();
            worthOfUnits["Jellyfish"] = 3;
            worthOfUnits["Walking Fish02"] = 1;
            worthOfUnits["Squid"] = 4;
            worthOfUnits["Turtle"] = 5;
            worthOfUnits["Flying Fish"] = 1;
            worthOfUnits["Super Jelly"] = 1;
            worthOfUnits["Lobster"] = 1;
            worthOfUnits["Starfish"] = 3;
            worthOfUnits["Sea Slug"] = 1;

            SpawnInformation jellyfishDefault = new SpawnInformation(0);
            jellyfishDefault.AddInformation("Unit Name", "Jellyfish");
            jellyfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            jellyfishDefault.AddInformation("Rotate Based On Velocity", false);
            SpawnInformation walkingfish02Default = new SpawnInformation(0);
            walkingfish02Default.AddInformation("Unit Name", "Walking Fish02");
            walkingfish02Default.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            walkingfish02Default.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation squidDefault = new SpawnInformation(0);
            squidDefault.AddInformation("Unit Name", "Squid");
            squidDefault.AddInformation("Starting Rotation", -(float)(Math.PI / 2));
            squidDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation turtleDefault = new SpawnInformation(0);
            turtleDefault.AddInformation("Unit Name", "Turtle");
            turtleDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            turtleDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation flyingfishDefault = new SpawnInformation(0);
            flyingfishDefault.AddInformation("Unit Name", "Flying Fish");
            flyingfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            flyingfishDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation superjellyDefault = new SpawnInformation(0);
            superjellyDefault.AddInformation("Unit Name", "Super Jelly");
            superjellyDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            superjellyDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation lobsterDefault = new SpawnInformation(0);
            lobsterDefault.AddInformation("Unit Name", "Lobster");
            lobsterDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            lobsterDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation starfishDefault = new SpawnInformation(0);
            starfishDefault.AddInformation("Unit Name", "Starfish");
            starfishDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            starfishDefault.AddInformation("Rotate Based On Velocity", true);
            SpawnInformation seaslugDefault = new SpawnInformation(0);
            seaslugDefault.AddInformation("Unit Name", "Sea Slug");
            seaslugDefault.AddInformation("Starting Rotation", (float)(Math.PI / 2));
            seaslugDefault.AddInformation("Rotate Based On Velocity", true);
            Dictionary<string, SpawnInformation> spawnInformation = new Dictionary<string, SpawnInformation>();
            spawnInformation["Jellyfish"] = jellyfishDefault;
            spawnInformation["Walking Fish02"] = walkingfish02Default;
            spawnInformation["Squid"] = squidDefault;
            spawnInformation["Turtle"] = turtleDefault;
            spawnInformation["Flying Fish"] = flyingfishDefault;
            spawnInformation["Super Jelly"] = superjellyDefault;
            spawnInformation["Lobster"] = lobsterDefault;
            spawnInformation["Starfish"] = starfishDefault;
            spawnInformation["Sea Slug"] = seaslugDefault;
            Dictionary<string, List<string>> possibleWeapons = new Dictionary<string, List<string>>();
            possibleWeapons["Jellyfish"] = new List<string> { "Circular Bounce" };
            possibleWeapons["Walking Fish02"] = new List<string> { "Fan" };
            possibleWeapons["Squid"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            possibleWeapons["Turtle"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            possibleWeapons["Flying Fish"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            possibleWeapons["Super Jelly"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            possibleWeapons["Lobster"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            possibleWeapons["Starfish"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            possibleWeapons["Sea Slug"] = new List<string> { "Split Shot", "Bullet", "Bullet Stream", "Fan", "Fan Stream", };
            Dictionary<string, List<string>> possibleBehaviors = new Dictionary<string, List<string>>();
            possibleBehaviors["Jellyfish"] = new List<string> { "In Fire Out" };
            possibleBehaviors["Walking Fish02"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Squid"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Turtle"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Flying Fish"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Super Jelly"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Lobster"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Starfish"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };
            possibleBehaviors["Sea Slug"] = new List<string> { "Loop Back", "Sigmoid", "Loop Straight" };

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