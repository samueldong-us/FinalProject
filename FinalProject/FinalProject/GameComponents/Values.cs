using FinalProject.GameSaving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal static class Values
    {
        public static Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, UnitValues>>> UnitValues;
        public static Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, Dictionary<string, object>>>> WeaponValues;
        private static SaveGame.Difficulty[] difficultyValues = { SaveGame.Difficulty.Easy, SaveGame.Difficulty.Normal, SaveGame.Difficulty.Hard };
        private static string[] weaponTypes = { "Circular Fire", "Fan" };

        static Values()
        {
            UnitValues = new Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, UnitValues>>>();
            InitializeUnitValues();
            FillUnitValues();
            WeaponValues = new Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, Dictionary<string, object>>>>();
            InitalizeWeaponValues();
            FillWeaponValues();
        }

        private static void FillUnitValues()
        {
            UnitValues[SaveGame.Difficulty.Easy][1]["Jellyfish"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Normal][1]["Jellyfish"] = new UnitValues(45);
            UnitValues[SaveGame.Difficulty.Hard][1]["Jellyfish"] = new UnitValues(60);
        }

        private static void FillWeaponValues()
        {
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Fire Rate"] = 1f;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);
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