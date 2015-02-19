using System;
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
                //XmlSerializerオブジェクトを作成
                //オブジェクトの型を指定する
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(PluginConfig));
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false, new System.Text.UTF8Encoding(false)))
                {
                    //シリアル化し、XMLファイルに保存する
                    serializer.Serialize(sw, config);
                }
            }
            else
            {
                config.IsForcedToRun = true;
                config.TaskName = new List<string>();
                config.TaskName.Add("OverlayForm");
                config.TaskName.Add("OverlayForm");
            }

            return config;
        }

        public static void Save(string fileName, PluginConfig config)
        {
            //XMLファイルに保存する
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(PluginConfig));
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false, new System.Text.UTF8Encoding(false)))
            {
                serializer.Serialize(sw, config);
            }
        }
    }
}
