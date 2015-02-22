using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BringToTop
{
    [Serializable]
    public class PluginConfig
    {
        public bool IsForcedToRun { get; set; }
        public List<string> TaskName { get; set; }

        public static PluginConfig Load(string fileName)
        {
            PluginConfig config = new PluginConfig();

            if (System.IO.File.Exists(fileName))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(PluginConfig));
                using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName, new System.Text.UTF8Encoding(false)))
                {
                    config = (PluginConfig)serializer.Deserialize(sr);
                }
            }
            else
            {
                config.IsForcedToRun = true;
                config.TaskName = new List<string>();
                config.TaskName.Add("Overlay");
                config.TaskName.Add("Overlay");
            }
            
            return config;
        }

        public static void Save(string fileName, PluginConfig config)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(PluginConfig));
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false, new System.Text.UTF8Encoding(false)))
            {
                serializer.Serialize(sw, config);
            }
        }
    }
}
