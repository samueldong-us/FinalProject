using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FinalProject.GameResources
{
    internal static class SaveGameManager
    {
        public static void CreateSaveDirectory()
        {
            if (!Directory.Exists("./Saved Games/"))
            {
                Directory.CreateDirectory("./Saved Games/");
            }
        }

        public static List<string> GetAllSaves()
        {
            List<string> saveNames = new List<string>();
            string[] saveFiles = Directory.GetFiles("./Saved Games", "*.sav");
            foreach (string file in saveFiles)
            {
                saveNames.Add(Path.GetFileNameWithoutExtension(file));
            }
            return saveNames;
        }

        public static SaveGame GetSavedGame(string name)
        {
            string pathToFile = "./Saved Games/" + name;
            if (SaveExists(name))
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(pathToFile)))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                    return (SaveGame)serializer.Deserialize(reader);
                }
            }
            else
            {
                throw new FileNotFoundException("Saved Game Not Found", name);
            }
        }

        public static bool SaveExists(string name)
        {
            string pathToFile = "./Saved Games/" + name;
            return File.Exists(pathToFile);
        }

        public static void SaveGame(SaveGame saveGame)
        {
            if (saveGame.Equals(".sav"))
            {
                throw new Exception("A SaveGame Must Have Its Name Set");
            }
            string pathToFile = "./Saved Games/" + saveGame.SaveName + ".sav";
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);
            }
            using (StreamWriter writer = new StreamWriter(File.Create(pathToFile)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                serializer.Serialize(writer, saveGame);
            }
        }
    }
}