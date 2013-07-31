using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Collections;
using System.Web;
using System.Net;
using System.Runtime.Serialization.Json;

using kanboxSDK.Util;
using kanboxSDK.Config;

namespace kanboxSDK.AuthInfo
{
    [DataContract]
    public class Token
    {
        [DataMember]
        public string access_token, expires_in, refresh_token, scope;
        [DataMember]
        public string token_type;
        public static Token GetNew(config cfg)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://auth.kanbox.com/0/token");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded ";
            Hashtable data = new Hashtable();
            data.Add("grant_type", "authorization_code");
            data.Add("client_id", config.client_id);
            data.Add("client_secret", config.client_secret);
            data.Add("code", cfg.code);
            data.Add("redirect_uri", config.OfficalWebSite);
            byte[] postData = Encoding.UTF8.GetBytes(Web.ParsToString(data));
            string str = Web.ParsToString(data);
            req.ContentLength = postData.LongLength;
            Stream postStream = req.GetRequestStream();
            postStream.Write(postData, 0, postData.Length);
            postStream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream ReturnStream = res.GetResponseStream();
            StreamReader ReturnJsonStream = new StreamReader(ReturnStream);
            DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(AuthInfo.Token));
            str = ReturnJsonStream.ReadToEnd();
            Stream s = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return JsonSerializer.ReadObject(s) as AuthInfo.Token;
        }

        public static Token Reflush(config cfg)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://auth.kanbox.com/0/token");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded ";
                Hashtable data = new Hashtable();
                data.Add("grant_type", "refresh_token");
                data.Add("client_id", config.client_id);
                data.Add("client_secret", config.client_secret);
                data.Add("refresh_token", cfg.token.refresh_token);
                byte[] postData = Encoding.UTF8.GetBytes(Web.ParsToString(data));
                string str = Web.ParsToString(data);
                req.ContentLength = postData.LongLength;
                Stream postStream = req.GetRequestStream();
                postStream.Write(postData, 0, postData.Length);
                postStream.Close();
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream ReturnStream = res.GetResponseStream();
                StreamReader ReturnJsonStream = new StreamReader(ReturnStream);
                DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(AuthInfo.Token));
                str = ReturnJsonStream.ReadToEnd();
                Stream s = new MemoryStream(Encoding.UTF8.GetBytes(str));
                return JsonSerializer.ReadObject(s) as AuthInfo.Token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
