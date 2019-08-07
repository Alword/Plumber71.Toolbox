using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plumber71.Core.Service.Woocomerce.Model
{
    public class RestConfig
    {
        public string Server { get; set; }
        public string UserKey { get; set; }
        public string SecretKey { get; set; }

        public static RestConfig GetDefaults()
        {
            string configFile = $"{Environment.CurrentDirectory}/Plumber/woo.secret.json";
            return JsonConvert.DeserializeObject<RestConfig>(File.ReadAllText(configFile));            
        }
    }
}
