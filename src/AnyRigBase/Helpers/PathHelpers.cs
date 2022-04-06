using System;
using System.IO;

namespace AnyRigBase.Helpers
{
    public class PathHelpers
    {
        // C:\Users\xxx\AppData\Roaming\IW1QLH\AnyRigLibrary
        public static string GetDataFolder()
        {
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IW1QLH", "AnyRigLibrary");
            Directory.CreateDirectory(dataPath);
            return dataPath;
        }

        public static string GetRigsFolder()
        {
            //return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rigs");
            string rigsFolder = Path.Combine(GetDataFolder(), "Rigs");
            Directory.CreateDirectory(rigsFolder);
            return rigsFolder;
        }

        public static string ConfigPath()
        {
            return Path.Combine(GetDataFolder(), "AnyRigLibrary.json");
        }


    }
}
