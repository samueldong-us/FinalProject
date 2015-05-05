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

        public static Dictionary<string, int> UnitWorth;

        public static Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, Dictionary<string, object>>>> WeaponValues;

        private static SaveGame.Difficulty[] difficultyValues = { SaveGame.Difficulty.Easy, SaveGame.Difficulty.Normal, SaveGame.Difficulty.Hard };

        private static string[] weaponTypes = { "Circular Fire", "Fan", "Bullet Stream", "Circular Bounce", "Fan Stream", "Split Shot", "Bullet", "Spiral", "Wall Fire" };

        static Values()
        {
            UnitValues = new Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, UnitValues>>>();
            InitializeUnitValues();
            FillUnitValues();
            UnitMovementSpeeds = new Dictionary<string, float>();
            FillUnitMovementSpeeds();
            UnitWorth = new Dictionary<string, int>();
            FillUnitWorth();
            UnitHealthBars = new Dictionary<string, UnitHealthBarInformation>();
            FillUnitHealthBars();
            WeaponValues = new Dictionary<SaveGame.Difficulty, Dictionary<int, Dictionary<string, Dictionary<string, object>>>>();
            InitalizeWeaponValues();
            FillWeaponValues();
        }

        private static void FillBulletStreamValues()
        {
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Bullet Stream"]["Fire Rate"] = .2f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Bullet Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][2]["Bullet Stream"]["Fire Rate"] = .2f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Bullet Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][2]["Bullet Stream"]["Fire Rate"] = .2f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Bullet Stream"]["Damage"] = 2;
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Bullet Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Bullet Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Bullet Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Bullet Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Bullet Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Bullet Stream"]["Damage"] = 2;
        }

        private static void FillBulletValues()
        {
            // Level 1
            WeaponValues[SaveGame.Difficulty.Easy][1]["Bullet"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Bullet"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][1]["Bullet"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Bullet"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][1]["Bullet"]["Fire Rate"] = 1f;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Bullet"]["Damage"] = 2;
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Bullet"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Bullet"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][2]["Bullet"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Bullet"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][2]["Bullet"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Bullet"]["Damage"] = 2;
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Bullet"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Bullet"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Bullet"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Bullet"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Bullet"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Bullet"]["Damage"] = 2;
        }

        private static void FillCircularBounceValues()
        {
            // Level 1
            // Not Used
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Bounce"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Bounce"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Bounce"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Bounce"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Bounce"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Bounce"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Bounce"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Bounce"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Bounce"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);
        }

        private static void FillCircularFireValues()
        {
            // Level 1
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
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Fire"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Fire"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Fire"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);
        }

        private static void FillFanStreamValues()
        {
            // Level 1
            // Not used
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan Stream"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan Stream"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan Stream"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan Stream"]["Damage"] = 2;
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan Stream"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan Stream"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan Stream"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan Stream"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan Stream"]["Damage"] = 2;
        }

        private static void FillFanValues()
        {
            // Level 1
            WeaponValues[SaveGame.Difficulty.Easy][1]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Fan"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][1]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Fan"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][1]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Fan"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Fan"]["Damage"] = 2;
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan"]["Damage"] = 2;
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan"]["Damage"] = 2;
        }

        private static void FillSpiralFireValues()
        {
            // Level 1
            WeaponValues[SaveGame.Difficulty.Easy][1]["Spiral"]["Fire Rate"] = .5f;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Spiral"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Spiral"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);

            WeaponValues[SaveGame.Difficulty.Normal][1]["Spiral"]["Fire Rate"] = .5f;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Spiral"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Spiral"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);

            WeaponValues[SaveGame.Difficulty.Hard][1]["Spiral"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Spiral"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Spiral"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Spiral"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Spiral"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Spiral"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);

            WeaponValues[SaveGame.Difficulty.Normal][2]["Spiral"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Spiral"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Spiral"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);

            WeaponValues[SaveGame.Difficulty.Hard][2]["Spiral"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Spiral"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Spiral"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Number Of Bullets"] = 25;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Number Of Bullets"] = 35;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);
        }

        private static void FillSplitShotValues()
        {
            // Level 1
            // Not used
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Split Shot"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Split Shot"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Split Shot"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][2]["Split Shot"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Split Shot"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Split Shot"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][2]["Split Shot"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Split Shot"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Split Shot"]["Damage"] = 2;
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Split Shot"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Split Shot"]["Number Of Bullets"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Split Shot"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Split Shot"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Split Shot"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Split Shot"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Split Shot"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Split Shot"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Split Shot"]["Damage"] = 2;
        }

        private static void FillUnitHealthBars()
        {
            UnitHealthBars["Jellyfish"] = new UnitHealthBarInformation(new Rectangle(0, 0, 100, 1), new Vector2(0, -50));
        }

        private static void FillUnitMovementSpeeds()
        {
            UnitMovementSpeeds["Jellyfish"] = 50f;
            UnitMovementSpeeds["Walking Fish01"] = 100f;
            UnitMovementSpeeds["Walking Fish02"] = 80f;
            UnitMovementSpeeds["Squid"] = 100f;
            UnitMovementSpeeds["Turtle"] = 100f;
            UnitMovementSpeeds["Sea Slug"] = 100f;
            UnitMovementSpeeds["Starfish"] = 100f;
            UnitMovementSpeeds["Lobster"] = 100f;
            UnitMovementSpeeds["Super Jelly"] = 100f;
            UnitMovementSpeeds["Flying Fish"] = 100f;
        }

        private static void FillUnitValues()
        {
            // Level 1
            UnitValues[SaveGame.Difficulty.Easy][1]["Jellyfish"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Normal][1]["Jellyfish"] = new UnitValues(45);
            UnitValues[SaveGame.Difficulty.Hard][1]["Jellyfish"] = new UnitValues(60);

            UnitValues[SaveGame.Difficulty.Easy][1]["Walking Fish01"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][1]["Walking Fish01"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][1]["Walking Fish01"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][1]["Walking Fish02"] = new UnitValues(15);
            UnitValues[SaveGame.Difficulty.Normal][1]["Walking Fish02"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Hard][1]["Walking Fish02"] = new UnitValues(45);
            // Level 2
            UnitValues[SaveGame.Difficulty.Easy][2]["Jellyfish"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Normal][2]["Jellyfish"] = new UnitValues(45);
            UnitValues[SaveGame.Difficulty.Hard][2]["Jellyfish"] = new UnitValues(60);

            UnitValues[SaveGame.Difficulty.Easy][2]["Walking Fish01"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Walking Fish01"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Walking Fish01"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Walking Fish02"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Walking Fish02"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Walking Fish02"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Sea Slug"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Sea Slug"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Sea Slug"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Squid"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Squid"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Squid"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Turtle"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Turtle"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Turtle"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Starfish"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Starfish"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Starfish"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Lobster"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Lobster"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Lobster"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Flying Fish"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Flying Fish"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Flying Fish"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][2]["Super Jelly"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][2]["Super Jelly"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][2]["Super Jelly"] = new UnitValues(30);
            // Level 3
            UnitValues[SaveGame.Difficulty.Easy][3]["Jellyfish"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Normal][3]["Jellyfish"] = new UnitValues(45);
            UnitValues[SaveGame.Difficulty.Hard][3]["Jellyfish"] = new UnitValues(60);

            UnitValues[SaveGame.Difficulty.Easy][3]["Walking Fish01"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Walking Fish01"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Walking Fish01"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Walking Fish02"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Walking Fish02"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Walking Fish02"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Sea Slug"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Sea Slug"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Sea Slug"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Squid"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Squid"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Squid"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Turtle"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Turtle"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Turtle"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Starfish"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Starfish"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Starfish"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Lobster"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Lobster"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Lobster"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Flying Fish"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Flying Fish"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Flying Fish"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][3]["Super Jelly"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][3]["Super Jelly"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][3]["Super Jelly"] = new UnitValues(30);
        }

        private static void FillWallFireValues()
        {
            // Level 1
            WeaponValues[SaveGame.Difficulty.Easy][1]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Easy][1]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Normal][1]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Normal][1]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Hard][1]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][1]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);
        }

        private static void FillWeaponValues()
        {
            FillBulletValues();
            FillBulletStreamValues();
            FillCircularBounceValues();
            FillCircularFireValues();
            FillFanValues();
            FillFanStreamValues();
            FillSplitShotValues();
            FillSpiralFireValues();
            FillWallFireValues();
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