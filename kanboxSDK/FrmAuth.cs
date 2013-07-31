using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Json;

using kanboxSDK.Util;
using kanboxSDK.Config;

namespace kanboxSDK
{
    public partial class FrmAuth : Form
    {
        public FrmAuth(ref config cfg)
        {
            InitializeComponent();
            this.cfg = cfg;
        }
        private config cfg;
        public static bool okay = false;
        public string code { private set; get; }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Hashtable data = new Hashtable();
            data.Add("response_type", "code");
            data.Add("client_id", config.client_id);
            data.Add("redirect_uri", config.OfficalWebSite);
            data.Add("user_language", "ZH");
            data.Add("user_platform", "windows");
            string geturl = Web.ParsToString(data);
            webBrowser1.Navigated += webBrowser1_Navigated;
            webBrowser1.Navigate("https://auth.kanbox.com/0/auth?" + geturl);
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString().IndexOf(config.OfficalWebSite) == 0)
            {
                this.webBrowser1.Stop();
                this.webBrowser1.Visible = false;
                //TODO error=invalid_request, access_denied 和 unsupported_response_type。 
                this.code = e.Url.ToString().Substring(e.Url.ToString().LastIndexOf("code="), e.Url.ToString().Length - e.Url.ToString().LastIndexOf("code=")).Replace("code=","");
                this.Close();
            }
        }

        private void FrmAuth_Shown(object sender, EventArgs e)
        {

        }





    }
}
