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
        private static string[] weaponTypes = { "Circular Fire", "Fan", "Bullet Stream", "Circular Bounce", "Fan Stream", "Split Shot", "Bullet", "Spiral", "Wall Fire", "Swing" };

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
            WeaponValues[SaveGame.Difficulty.Easy][3]["Bullet Stream"]["Fire Rate"] = .2f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Bullet Stream"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Bullet Stream"]["Fire Rate"] = .15f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Bullet Stream"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Bullet Stream"]["Fire Rate"] = .1f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Bullet Stream"]["Damage"] = 3;
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
            WeaponValues[SaveGame.Difficulty.Easy][2]["Bullet"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Bullet"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Normal][2]["Bullet"]["Fire Rate"] = 1f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Bullet"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Hard][2]["Bullet"]["Fire Rate"] = 1f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Bullet"]["Damage"] = 3;
            // Level 3
            // Not used
        }

        private static void FillCircularBounceValues()
        {
            // Level 1
            // Not Used
            // Level 2
            // Not used
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Fire Rate"] = 2.7f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Number Of Bullets"] = 40;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Fire Rate"] = 2.4f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Number Of Bullets"] = 60;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Damage"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Bounce"]["Rotational Delta"] = (float)(Math.PI * Math.PI);
        }

        private static void FillCircularFireValues()
        {
            // Level 1
            WeaponValues[SaveGame.Difficulty.Easy][1]["Circular Fire"]["Fire Rate"] = 4f;
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
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Fire"]["Number Of Bullets"] = 40;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Number Of Bullets"] = 40;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Fire Rate"] = 2.5f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Number Of Bullets"] = 40;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Number Of Bullets"] = 50;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Circular Fire"]["Rotational Delta"] = (float)(Math.PI * Math.PI);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Fire"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Fire"]["Number Of Bullets"] = 60;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Circular Fire"]["Damage"] = 4;
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
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan Stream"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan Stream"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan Stream"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan Stream"]["Number Of Bullets"] = 4;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan Stream"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan Stream"]["Fire Rate"] = 2.5f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan Stream"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan Stream"]["Damage"] = 3;
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
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan"]["Number Of Bullets"] = 4;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Fan"]["Damage"] = 1;

            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Fan"]["Damage"] = 2;
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan"]["Number Of Bullets"] = 4;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Fan"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Fan"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan"]["Fire Rate"] = 1.8f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Fan"]["Damage"] = 3;
        }

        private static void FillSpiralFireValues()
        {
            // Level 1
            // Not used
            // Level 2
            // Not used
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Fire Rate"] = .2f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Number Of Bullets"] = 4;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 16);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Fire Rate"] = .15f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Number Of Bullets"] = 6;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 24);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Fire Rate"] = .1f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Number Of Bullets"] = 10;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Damage"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Spiral"]["Rotational Delta"] = (float)(Math.PI / 32);
        }

        private static void FillSplitShotValues()
        {
            // Level 1
            // Not used
            // Level 2
            // Not used
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Split Shot"]["Fire Rate"] = 1.8f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Split Shot"]["Damage"] = 2;

            WeaponValues[SaveGame.Difficulty.Normal][3]["Split Shot"]["Fire Rate"] = 1.5f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Split Shot"]["Damage"] = 3;

            WeaponValues[SaveGame.Difficulty.Hard][3]["Split Shot"]["Fire Rate"] = 1f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Split Shot"]["Damage"] = 4;
        }

        private static void FillSwingValues()
        {
            WeaponValues[SaveGame.Difficulty.Easy][3]["Swing"]["Fire Rate"] = .3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Swing"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Swing"]["Time Firing"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Swing"]["Delay"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Swing"]["Delta Theta"] = (float)(Math.PI / 4);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Swing"]["Fire Rate"] = .2f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Swing"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Swing"]["Time Firing"] = 2f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Swing"]["Delay"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Swing"]["Delta Theta"] = (float)(Math.PI / 6);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Swing"]["Fire Rate"] = .1f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Swing"]["Damage"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Swing"]["Time Firing"] = 1f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Swing"]["Delay"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Swing"]["Delta Theta"] = (float)(Math.PI / 12);
        }

        private static void FillUnitHealthBars()
        {
            UnitHealthBars["Jellyfish"] = new UnitHealthBarInformation(new Rectangle(0, 0, 75, 1), new Vector2(0, -50));
            UnitHealthBars["Walking Fish01"] = new UnitHealthBarInformation(new Rectangle(0, 0, 50, 1), new Vector2(0, -50));
            UnitHealthBars["Walking Fish02"] = new UnitHealthBarInformation(new Rectangle(0, 0, 50, 1), new Vector2(0, -60));
            UnitHealthBars["Sea Slug"] = new UnitHealthBarInformation(new Rectangle(0, 0, 75, 1), new Vector2(0, -75));
            UnitHealthBars["Squid"] = new UnitHealthBarInformation(new Rectangle(0, 0, 50, 1), new Vector2(0, -90));
            UnitHealthBars["Turtle"] = new UnitHealthBarInformation(new Rectangle(0, 0, 150, 1), new Vector2(0, -75));
            UnitHealthBars["Lobster"] = new UnitHealthBarInformation(new Rectangle(0, 0, 150, 1), new Vector2(0, -90));
            UnitHealthBars["Starfish"] = new UnitHealthBarInformation(new Rectangle(0, 0, 150, 1), new Vector2(0, -90));
            UnitHealthBars["Flying Fish"] = new UnitHealthBarInformation(new Rectangle(0, 0, 150, 1), new Vector2(0, -50));
            UnitHealthBars["Super Jelly"] = new UnitHealthBarInformation(new Rectangle(0, 0, 150, 1), new Vector2(0, -75));
        }

        private static void FillUnitMovementSpeeds()
        {
            UnitMovementSpeeds["Walking Fish01"] = 120f;
            UnitMovementSpeeds["Walking Fish02"] = 80f;
            UnitMovementSpeeds["Sea Slug"] = 100f;
            UnitMovementSpeeds["Jellyfish"] = 50f;
            UnitMovementSpeeds["Squid"] = 60f;
            UnitMovementSpeeds["Turtle"] = 35f;
            UnitMovementSpeeds["Starfish"] = 80f;
            UnitMovementSpeeds["Lobster"] = 60f;
            UnitMovementSpeeds["Flying Fish"] = 40f;
            UnitMovementSpeeds["Super Jelly"] = 40f;
        }

        private static void FillUnitValues()
        {
            // Level 1
            UnitValues[SaveGame.Difficulty.Easy][1]["Walking Fish01"] = new UnitValues(10);
            UnitValues[SaveGame.Difficulty.Normal][1]["Walking Fish01"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Hard][1]["Walking Fish01"] = new UnitValues(30);

            UnitValues[SaveGame.Difficulty.Easy][1]["Walking Fish02"] = new UnitValues(15);
            UnitValues[SaveGame.Difficulty.Normal][1]["Walking Fish02"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Hard][1]["Walking Fish02"] = new UnitValues(45);

            UnitValues[SaveGame.Difficulty.Easy][1]["Jellyfish"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Normal][1]["Jellyfish"] = new UnitValues(45);
            UnitValues[SaveGame.Difficulty.Hard][1]["Jellyfish"] = new UnitValues(60);
            // Level 2
            UnitValues[SaveGame.Difficulty.Easy][2]["Walking Fish01"] = new UnitValues(20);
            UnitValues[SaveGame.Difficulty.Normal][2]["Walking Fish01"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Hard][2]["Walking Fish01"] = new UnitValues(40);

            UnitValues[SaveGame.Difficulty.Easy][2]["Walking Fish02"] = new UnitValues(30);
            UnitValues[SaveGame.Difficulty.Normal][2]["Walking Fish02"] = new UnitValues(45);
            UnitValues[SaveGame.Difficulty.Hard][2]["Walking Fish02"] = new UnitValues(55);

            UnitValues[SaveGame.Difficulty.Easy][2]["Sea Slug"] = new UnitValues(40);
            UnitValues[SaveGame.Difficulty.Normal][2]["Sea Slug"] = new UnitValues(50);
            UnitValues[SaveGame.Difficulty.Hard][2]["Sea Slug"] = new UnitValues(60);

            UnitValues[SaveGame.Difficulty.Easy][2]["Jellyfish"] = new UnitValues(50);
            UnitValues[SaveGame.Difficulty.Normal][2]["Jellyfish"] = new UnitValues(65);
            UnitValues[SaveGame.Difficulty.Hard][2]["Jellyfish"] = new UnitValues(80);

            UnitValues[SaveGame.Difficulty.Easy][2]["Squid"] = new UnitValues(70);
            UnitValues[SaveGame.Difficulty.Normal][2]["Squid"] = new UnitValues(80);
            UnitValues[SaveGame.Difficulty.Hard][2]["Squid"] = new UnitValues(100);

            UnitValues[SaveGame.Difficulty.Easy][2]["Turtle"] = new UnitValues(90);
            UnitValues[SaveGame.Difficulty.Normal][2]["Turtle"] = new UnitValues(110);
            UnitValues[SaveGame.Difficulty.Hard][2]["Turtle"] = new UnitValues(130);
            // Level 3
            UnitValues[SaveGame.Difficulty.Easy][3]["Walking Fish02"] = new UnitValues(40);
            UnitValues[SaveGame.Difficulty.Normal][3]["Walking Fish02"] = new UnitValues(60);
            UnitValues[SaveGame.Difficulty.Hard][3]["Walking Fish02"] = new UnitValues(80);

            UnitValues[SaveGame.Difficulty.Easy][3]["Sea Slug"] = new UnitValues(60);
            UnitValues[SaveGame.Difficulty.Normal][3]["Sea Slug"] = new UnitValues(80);
            UnitValues[SaveGame.Difficulty.Hard][3]["Sea Slug"] = new UnitValues(100);

            UnitValues[SaveGame.Difficulty.Easy][3]["Jellyfish"] = new UnitValues(80);
            UnitValues[SaveGame.Difficulty.Normal][3]["Jellyfish"] = new UnitValues(100);
            UnitValues[SaveGame.Difficulty.Hard][3]["Jellyfish"] = new UnitValues(120);

            UnitValues[SaveGame.Difficulty.Easy][3]["Squid"] = new UnitValues(90);
            UnitValues[SaveGame.Difficulty.Normal][3]["Squid"] = new UnitValues(105);
            UnitValues[SaveGame.Difficulty.Hard][3]["Squid"] = new UnitValues(140);

            UnitValues[SaveGame.Difficulty.Easy][3]["Turtle"] = new UnitValues(125);
            UnitValues[SaveGame.Difficulty.Normal][3]["Turtle"] = new UnitValues(160);
            UnitValues[SaveGame.Difficulty.Hard][3]["Turtle"] = new UnitValues(200);

            UnitValues[SaveGame.Difficulty.Easy][3]["Starfish"] = new UnitValues(105);
            UnitValues[SaveGame.Difficulty.Normal][3]["Starfish"] = new UnitValues(120);
            UnitValues[SaveGame.Difficulty.Hard][3]["Starfish"] = new UnitValues(170);

            UnitValues[SaveGame.Difficulty.Easy][3]["Lobster"] = new UnitValues(100);
            UnitValues[SaveGame.Difficulty.Normal][3]["Lobster"] = new UnitValues(110);
            UnitValues[SaveGame.Difficulty.Hard][3]["Lobster"] = new UnitValues(150);

            UnitValues[SaveGame.Difficulty.Easy][3]["Flying Fish"] = new UnitValues(105);
            UnitValues[SaveGame.Difficulty.Normal][3]["Flying Fish"] = new UnitValues(120);
            UnitValues[SaveGame.Difficulty.Hard][3]["Flying Fish"] = new UnitValues(170);

            UnitValues[SaveGame.Difficulty.Easy][3]["Super Jelly"] = new UnitValues(115);
            UnitValues[SaveGame.Difficulty.Normal][3]["Super Jelly"] = new UnitValues(140);
            UnitValues[SaveGame.Difficulty.Hard][3]["Super Jelly"] = new UnitValues(180);
        }

        private static void FillUnitWorth()
        {
            UnitWorth["Walking Fish01"] = 1;
            UnitWorth["Walking Fish02"] = 3;
            UnitWorth["Sea Slug"] = 32;
            UnitWorth["Jellyfish"] = 9;
            UnitWorth["Squid"] = 64;
            UnitWorth["Turtle"] = 83;
            UnitWorth["Starfish"] = 88;
            UnitWorth["Super Jelly"] = 86;
            UnitWorth["Flying Fish"] = 70;
            UnitWorth["Lobster"] = 70;
        }

        private static void FillWallFireValues()
        {
            // Level 1
            // Not used
            // Level 2
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Number Of Bullets"] = 5;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Damage"] = 1;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Wall Width"] = 4;
            WeaponValues[SaveGame.Difficulty.Easy][2]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Number Of Bullets"] = 4;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Damage"] = 2;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Wall Width"] = 6;
            WeaponValues[SaveGame.Difficulty.Normal][2]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);

            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Fire Rate"] = 3f;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Number Of Bullets"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Wall Width"] = 8;
            WeaponValues[SaveGame.Difficulty.Hard][2]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 12);
            // Level 3
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Wall Width"] = 8;
            WeaponValues[SaveGame.Difficulty.Easy][3]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 18);

            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Damage"] = 3;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Wall Width"] = 12;
            WeaponValues[SaveGame.Difficulty.Normal][3]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 18);

            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Fire Rate"] = 2f;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Number Of Bullets"] = 3;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Damage"] = 4;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Wall Width"] = 15;
            WeaponValues[SaveGame.Difficulty.Hard][3]["Wall Fire"]["Rotational Delta"] = (float)(Math.PI / 18);
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
            FillSwingValues();
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