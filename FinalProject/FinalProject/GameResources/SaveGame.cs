namespace FinalProject.GameResources
{
    public class SaveGame
    {
        public int Credits;
        public int HighestUnlockedStage;
        public string SaveName;

        public SaveGame()
        {
            SaveName = "";
            HighestUnlockedStage = 1;
            Credits = 0;
        }
    }
}