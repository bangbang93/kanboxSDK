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
    public partial class FrmUpload : Form
    {
        private config cfg;
        private string FileName;
        private FileStream fs;
        private Work Worker;
        private string Path;
        private long Finished;
        private long Total;
        private TimeSpan Start = new TimeSpan(DateTime.Now.Ticks);
        public FrmUpload(config cfg,string FileName,string Path)
        {
            InitializeComponent();
            this.cfg = cfg;
            this.FileName = FileName;
            this.Path = Path.Replace('\\', '/') + System.IO.Path.GetFileName(FileName);
            fs = new FileStream(FileName, FileMode.Open);
            Worker = new Work(cfg);
        }

        private void FrmUpload_Shown(object sender, EventArgs e)
        {
            Worker.UploadFileCompleteEvent += Worker_UploadFileCompleteEvent;
            Worker.UploadFileProgressChangedEvent += Worker_UploadFileProgressChangedEvent;
            Worker.UploadFile(Path, fs, fs.Length);
            this.prs.Maximum = (int)fs.Length;
            this.Total = fs.Length;
            timer1.Start();
        }

        void Worker_UploadFileProgressChangedEvent(long complete)
        {
            this.prs.Invoke(new MethodInvoker(delegate{ this.prs.Value = (int)complete;}));
            this.Finished = complete;
        }

        void Worker_UploadFileCompleteEvent(Exception ex)
        {
            this.Invoke(new MethodInvoker(delegate { this.Close(); }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan Now=new TimeSpan(DateTime.Now.Ticks);
            labSpeed.Text = (Finished / (Now-Start).TotalSeconds / 1024).ToString() + "KB/s";
        }

    }
}
