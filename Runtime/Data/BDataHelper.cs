using OdinSerializer;
using System;
using System.IO;
using UnityEngine;

namespace Bounce.Framework
{
    public static class BDataHelper
    {
        static readonly string s_rootFolder = "Bounce.Framework";

        public static void Save<T>(T data, string filePath)
        {
            byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);

            // Check if root folder exist
            string directoryPath = Path.Combine(Application.persistentDataPath, s_rootFolder);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, s_rootFolder, filePath), bytes);
        }

        public static T Load<T>(string filePath) where T : class
        {
            try
            {
                string path = Path.Combine(Application.persistentDataPath, s_rootFolder, filePath);

                if (!File.Exists(path))
                    return null;

                byte[] bytes = File.ReadAllBytes(path);

                return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);
            }
            catch (Exception e)
            {
                BDebug.Log("Something wrong when load data: {0}", e);
                return null;
            }
        }

        public static void Delete(string filePath)
        {
            string path = Path.Combine(Application.persistentDataPath, s_rootFolder, filePath);

            if (!File.Exists(path))
                return;

            File.Delete(path);
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Bounce/Clear Data")]
#endif
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();

            string path = Path.Combine(Application.persistentDataPath, s_rootFolder);

            var info = new DirectoryInfo(path);

            if (!info.Exists)
                return;

            var files = info.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                files[i].Delete();
            }
        }
    }
}