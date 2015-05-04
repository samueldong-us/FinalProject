using FinalProject.GameSaving;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal static class Values
    {
        public static Dictionary<string, UnitHealthBarInformation> UnitHealthBars;

        public static Dictionary<string, float> UnitMovementSpeeds;

        public static Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, UnitValues>>> UnitValues;

        public static Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, Dictionary<string, object>>>> WeaponValues;

        private static SaveGame.Difficulty[] difficultyValues = { SaveGame.Difficulty.Easy, SaveGame.Difficulty.Normal, SaveGame.Difficulty.Hard };

        private static string[] weaponTypes = { "Circular Fire", "Fan" };

        static Values()
        {
            UnitValues = new Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, UnitValues>>>();
            InitializeUnitValues();
            FillUnitValues();
            UnitMovementSpeeds = new Dictionary<string, float>();
            FillUnitMovementSpeeds();
            UnitHealthBars = new Dictionary<string, UnitHealthBarInformation>();
            FillUnitHealthBars();
            WeaponValues = new Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, Dictionary<string, object>>>>();
            InitalizeWeaponValues();
            FillWeaponValues();
        }

        private static void FillUnitHealthBars()
        {
            UnitHealthBars["Jellyfish"] = new UnitHealthBarInformation(new Rectangle(0, 0, 100, 10), new Vector2(0, -75));
        }

        private static void FillUnitMovementSpeeds()
        {
            UnitMovementSpeeds["Jellyfish"] = 50f;
            UnitMovementSpeeds["Walking Fish01"] = 100f;
        }

        private static void FillUnitValues()
        {
            UnitValues[SaveGame.Difficulty.Easy][1]["Jellyfish"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Normal][1]["Jellyfish"] = new UnitValues(45);
            UnitValues[SaveGame.Difficulty.Hard][1]["Jellyfish"] = new UnitValues(60);

            UnitValues[SaveGame.Difficulty.Easy][1]["Walking Fish01"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][1]["Walking Fish01"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][1]["Walking Fish01"] = new UnitValues(30);
        }

        private static void FillWeaponValues()
        {
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Easy][1]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Fan"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][1]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Fan"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][1]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Fan"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Fan"]["Damage"] = 2;
        }

        private static void InitalizeWeaponValues()
        {
            foreach (SaveGame.Difficulty difficulty in difficultyValues)
            {
                WeaponValues[difficulty] = new Dictionary<int, Dictionary<string, Dictionary<string, object>>>();
                for (int stage = 1; stage <= 3; stage++)
                {
                    WeaponValues[difficulty][stage] = new Dictionary<string, Dictionary<string, object>>();
                    foreach (string weaponType in weaponTypes)
                    {
                        WeaponValues[difficulty][stage][weaponType] = new Dictionary<string, object>();
                    }
                }
            }
        }

        private static void InitializeUnitValues()
        {
            foreach (SaveGame.Difficulty difficulty in difficultyValues)
            {
                UnitValues[difficulty] = new Dictionary<int, Dictionary<string, UnitValues>>();
                for (int stage = 1; stage <= 3; stage++)
                {
                    UnitValues[difficulty][stage] = new Dictionary<string, UnitValues>();
                }
            }
        }
    }
}