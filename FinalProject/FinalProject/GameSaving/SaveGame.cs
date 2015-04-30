namespace FinalProject.GameSaving
{
    public class SaveGame
    {
        public enum Character { Varlet, Oason, Dimmy }

        public enum Difficulty { Easy, Normal, Hard }

        public Character character;

        public int Credits = 1000;

        public int Damage = 1;

        public Difficulty difficulty;

        public int FireRate = 1;

        public int HighestUnlockedStage = 1;

        public int MovementSpeed = 1;

        public string SaveName;

        public int Shields = 1;

        public int WeaponStrength = 1;
    }
}