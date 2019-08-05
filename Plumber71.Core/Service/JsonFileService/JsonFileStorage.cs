using Newtonsoft.Json;
using System;
using System.IO;

namespace Plumber71.Core.Service.JsonFileService
{
    public class JsonFileStorage
    {
        private static readonly string DefaultDirectory = $"{Environment.CurrentDirectory}\\Resource\\Temp";
        public static void Save<T>(T chacheObject, string fileName = nameof(T) + ".json") where T : new()
        {
            string chacheText = JsonConvert.SerializeObject(chacheObject);
            string path = Path.Combine(DefaultDirectory, fileName);
            Directory.CreateDirectory(DefaultDirectory); //Access to the path 'D:\YandexDisk\Workspaces\Plumber71.Toolbox\Plumber71.TestConsole\bin\Debug\netcoreapp2.2\Temp\chacheObject' is denied.
            File.WriteAllText(path, chacheText);
        }
        public static T Load<T>(string fileName = nameof(T) + ".json") where T : new()
        {
            string path = Path.Combine(DefaultDirectory, fileName);

            if (!File.Exists(path)) return default;

            string chacheText = File.ReadAllText(path);
            T chacheObject = JsonConvert.DeserializeObject<T>(chacheText);
            return chacheObject;
        }
    }
}