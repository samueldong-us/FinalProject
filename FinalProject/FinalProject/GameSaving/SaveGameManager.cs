using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FinalProject.GameSaving
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
            if (SaveExists(name))
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(PathToFile(name))))
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
            return File.Exists(PathToFile(name));
        }

        public static void SaveGame(SaveGame saveGame)
        {
            if (saveGame.SaveName.Equals(""))
            {
                throw new Exception("A SaveGame Must Have Its Name Set");
            }
            if (File.Exists(PathToFile(saveGame.SaveName)))
            {
                File.Delete(PathToFile(saveGame.SaveName));
            }
            using (StreamWriter writer = new StreamWriter(File.Create(PathToFile(saveGame.SaveName))))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                serializer.Serialize(writer, saveGame);
            }
        }

        private static string PathToFile(string name)
        {
            return "./Saved Games/" + name + ".sav";
        }
    }
}