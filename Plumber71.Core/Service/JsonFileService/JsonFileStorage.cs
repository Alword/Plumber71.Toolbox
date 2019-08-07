using Newtonsoft.Json;
using System;
using System.IO;

namespace Plumber71.Core.Service.JsonFileService
{
    public class JsonFileStorage
    {
        private static readonly string DefaultDirectory = $"{Environment.SpecialFolder.LocalApplicationData}\\Resource\\Temp";
        public static void Save<T>(T chacheObject, string fileName = null) where T : new()
        {
            string path = GetObjectPath<T>(fileName);
            string chacheText = JsonConvert.SerializeObject(chacheObject);
            Directory.CreateDirectory(DefaultDirectory); 
            File.WriteAllText(path, chacheText);
        }
        public static T Load<T>(string fileName = null) where T : new()
        {
            string path = GetObjectPath<T>(fileName);
            if (!File.Exists(path)) return default;
            string chacheText = File.ReadAllText(path);
            T chacheObject = JsonConvert.DeserializeObject<T>(chacheText);
            return chacheObject;
        }

        private static string GetObjectPath<T>(string fileName)
        {
            fileName = fileName ?? $"{typeof(T).GUID}.json";
            return Path.Combine(DefaultDirectory, fileName);
        }
    }
}