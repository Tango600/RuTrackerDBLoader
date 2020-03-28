using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuTrackerDBLoader
{
    public class Config
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Cogepage { get; set; }
    }

    public static class ConfigReader
    {
        public static Config Config { get; private set; }
        public const string ConfigFileName = "DB.xml";

        public static void LoadConfig(string xmlFile)
        {
            using (var xmlStream = new StreamReader(xmlFile))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
                Config = (Config)xmlSerializer.Deserialize(xmlStream);
            }
        }

        public static void InitConfig(string xmlFile)
        {
            using (var xmlStream = new StreamWriter(xmlFile))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
                xmlSerializer.Serialize(xmlStream, new Config { UserName = "sysdba", Password = "masterkey", Cogepage = "UTF8" });
            }
        }
    }
}
