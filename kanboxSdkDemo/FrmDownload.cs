using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using kanboxSDK;
using kanboxSDK.Config;

namespace kanboxSDKDemo
{
    public partial class FrmDownload : Form
    {
        private config cfg;
        private string FileName;
        private string Path;
        private long Finished;
        private long Total;
        private TimeSpan Start = new TimeSpan(DateTime.Now.Ticks);
        private Work Worker;
        private FileStream fs;
        public FrmDownload(config cfg, string FileName,string Path)
        {
            InitializeComponent();
            this.cfg = cfg;
            this.FileName = FileName;
            this.Path = Path;
            fs = new FileStream(FileName, FileMode.Create);
            Worker = new Work(cfg);
        }

        private void FrmDownload_Shown(object sender, EventArgs e)
        {
            Worker.DownloadFileCompleteEvent += Worker_DownloadFileCompleteEvent;
            Worker.DownloadFileProgressChangedEvent += Worker_DownloadFileProgressChangedEvent;
            Worker.DownloadFile(Path);
            timer1.Start();
        }

        void Worker_DownloadFileProgressChangedEvent(long fin, long total, byte[] buffer, int ThisRec)
        {
            this.prs.Invoke(new MethodInvoker(delegate
            {
                this.prs.Maximum=(int)total;
                this.prs.Value=(int)fin;
            }));
            this.Finished = fin;
            this.Total = total;
            fs.Write(buffer, 0, ThisRec);
        }

        void Worker_DownloadFileCompleteEvent(Exception ex)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.Close();
                fs.Close();
            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan Now = new TimeSpan(DateTime.Now.Ticks);
            labSpeed.Text = (Finished / (Now - Start).TotalSeconds / 1024).ToString() + "KB/s";
        }
    }
}
