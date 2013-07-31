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
using kanboxSDK;
using kanboxSDK.AuthInfo;
using kanboxSDK.DataType;

namespace kanboxSDKDemo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        Hashtable ColWidth = new Hashtable();
        private config cfg;
        public static bool okay = false;
        FrmAuth Auth;
        private UserInfoType UserInfo;
        private string CurrectDirectory = "/";
        private void FrmMain_Load(object sender, EventArgs e)
        {
            ColWidth.Add("fullPath", 240);
            ColWidth.Add("modificationDate", 200);
            ColWidth.Add("fileSize", 100);
            ColWidth.Add("isFolder", 30);
            try
            {
                if (File.Exists(config.cfgfile))
                {
                    cfg = config.LoadConfig(config.cfgfile);
                }
                else
                    throw new Exception();
            }
            catch
            {
                cfg = new config();
                Auth = new FrmAuth(ref cfg);
                Auth.ShowDialog();
                cfg.code = Auth.code;
                cfg.token = Token.GetNew(cfg);
                config.SaveConfig(config.cfgfile, cfg);
            }
            GetInfo();
            labUser.Text = UserInfo.email;
            prsUsage.Maximum = (int)UserInfo.spaceQuota;
            prsUsage.Value = (int)UserInfo.spaceUsed;
            labUsage.Text = (UserInfo.spaceUsed / 1024 / 1024) + "MB/" + (UserInfo.spaceQuota / 1024 / 1024) + "MB";
            Work Worker = new Work(cfg);
            Worker.ListFileCompleteEvent += Worker_ListFileCompleteEvent;
            Worker.ListFile();

        }

        void Worker_ListFileCompleteEvent(DataTable dt)
        {
            listFile.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                ColumnHeader Header = listFile.Columns.Add(col.ColumnName, col.ColumnName, (int)ColWidth[col.ColumnName]);
                Header.Name = col.ColumnName;
            }
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem Item = listFile.Items.Add(row[0].ToString());
                for (int i = 1; i < dt.Columns.Count;i++ )
                {
                    ListViewItem.ListViewSubItem SubItem=new ListViewItem.ListViewSubItem(Item,row[i].ToString());
                    SubItem.Name = dt.Columns[i].ColumnName;
                    Item.SubItems.Add(SubItem);
                }
            }
        }

        private void GetInfo()
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.kanbox.com/0/info");
                req.Method = "GET";
                req.Headers.Add("Authorization", "Bearer " + cfg.token.access_token);
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader ReturnStream = new StreamReader(res.GetResponseStream());
                string Json = ReturnStream.ReadToEnd();
                Stream JsonStream = new MemoryStream(Encoding.UTF8.GetBytes(Json));
                DataContractJsonSerializer JsonSer = new DataContractJsonSerializer(typeof(UserInfoType));
                UserInfo = JsonSer.ReadObject(JsonStream) as UserInfoType;
            }
            catch (WebException ex)
            {
                if (ex.Message.IndexOf("401") != -1)
                {
                    cfg.token = Token.Reflush(cfg);
                    config.SaveConfig(config.cfgfile, cfg);
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cfg.code != null)
                {
                    okay = true;
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            string Path;
            if (listFile.SelectedItems[0].SubItems[listFile.Columns["isFolder"].Text].Text.ToLower() == "true")
                Path = listFile.SelectedItems[0].Text;
            else
                Path = System.IO.Path.GetDirectoryName(listFile.SelectedItems[0].Text);
            if (Path.Last() != '/')
            {
                Path += "/";
            }
            FrmUpload Upload = new FrmUpload(cfg,of.FileName,Path);
            Upload.ShowDialog();
            Work Worker = new Work(cfg);
            Worker.ListFileCompleteEvent+=Worker_ListFileCompleteEvent;
            Worker.ListFile();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            string FileName;
            if (listFile.SelectedItems[0].SubItems[listFile.Columns["isFolder"].Text].Text.ToLower() == "true")
            {
                MessageBox.Show("文件夹不能下载");
                return;
            }
            else
                FileName = System.IO.Path.GetFileName(listFile.SelectedItems[0].Text);
            sf.FileName = FileName;
            sf.ShowDialog();
            FileName = sf.FileName;
            FrmDownload Download = new FrmDownload(cfg, FileName, listFile.SelectedItems[0].Text);
            Download.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Work Worker = new Work(cfg);
            Worker.DeleteFileCompleteEvent += Worker_DeleteFileCompleteEvent;
            Worker.DeleteFile(listFile.SelectedItems[0].Text);
        }

        void Worker_DeleteFileCompleteEvent(string status)
        {
            Work Worker = new Work(cfg);
            Worker.ListFileCompleteEvent += Worker_ListFileCompleteEvent;
            Worker.ListFile();
        }

        private void listFile_DoubleClick(object sender, EventArgs e)
        {
            if (listFile.SelectedItems[0].SubItems[listFile.Columns["isFolder"].Text].Text.ToLower() == "true")
            {
                Work Worker = new Work(cfg);
                Worker.ListFileCompleteEvent += Worker_ListFileCompleteEvent;
                Worker.ListFile(listFile.SelectedItems[0].SubItems[0].Text);
                CurrectDirectory = listFile.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void btnUpFolder_Click(object sender, EventArgs e)
        {
            if (CurrectDirectory == "/")
                MessageBox.Show("不能再向上了");
            else
            {
                CurrectDirectory = CurrectDirectory.Substring(0, CurrectDirectory.LastIndexOf('/'));
                Work Worker = new Work(cfg);
                Worker.ListFileCompleteEvent += Worker_ListFileCompleteEvent;
                Worker.ListFile(CurrectDirectory);
            }
        }

    }
}
