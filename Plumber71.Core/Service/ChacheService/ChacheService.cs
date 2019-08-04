using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Plumber71.Core.Service.ChacheService
{
    public class ChacheService
    {
        private static readonly string DefaultDirectory = $"{Environment.CurrentDirectory}\\Resource\\Temp";
        public static void WriteChache<T>(T chacheObject,string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName)) fileName = $"{nameof(chacheObject)}.json";

            string chacheText = JsonConvert.SerializeObject(chacheObject);
            string path = Path.Combine(DefaultDirectory, fileName);

            Directory.CreateDirectory(DefaultDirectory); //Access to the path 'D:\YandexDisk\Workspaces\Plumber71.Toolbox\Plumber71.TestConsole\bin\Debug\netcoreapp2.2\Temp\chacheObject' is denied.
            File.WriteAllText(path, chacheText);
        }
    }
}