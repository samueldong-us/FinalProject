namespace FinalProject.GameResources
{
    public class SaveGame
    {
        public Character character;
        public int Credits;
        public Difficulty difficulty;
        public int HighestUnlockedStage;
        public string SaveName;

        public enum Character { Varlet, Oason, Dimmy }

        public enum Difficulty { Easy, Normal, Hard }
    }
}