using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace kanboxSDK.Config
{
    [DataContract]
    public class config
    {
        public static string client_id = "ac43b66cb7955c69982ca24d5d196080";
        public static string client_secret = "edb7ef1c8c8634397158a1fd9cf361ff";
        public static string OfficalWebSite = "http://www.kyfly.com";
        public static string cfgfile = "config.xml";
        [DataMember]
        public string code;
        [DataMember]
        public AuthInfo.Token token;

        public config()
        {            
            
        }

        public static void SaveConfig(string FileName,config cfg)
        {
            FileStream cfgfile = new FileStream(FileName, FileMode.Create);
            try
            {
                DataContractSerializer XmlSerializer = new DataContractSerializer(typeof(config));
                XmlSerializer.WriteObject(cfgfile, cfg);
            }
            finally
            {
                cfgfile.Close();
            }
        }

        public static config LoadConfig(string FileName)
        {
            FileStream cfgfile = new FileStream(FileName, FileMode.Open);
            try
            {
                DataContractSerializer cfgSerializer = new DataContractSerializer(typeof(config));
                config cfg = cfgSerializer.ReadObject(cfgfile) as config;
                return cfg;
            }
            finally
            {
                cfgfile.Close();
            }
        }
    }
}
