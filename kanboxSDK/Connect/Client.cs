using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace kanboxSDK.Connect
{
    class Client:WebClient
    {
        #region 异常
        private WebException NoAuth = new WebException("授权过期或未授权");
        private WebException FileNotFound = new WebException("服务器上没有这个文件或者这是个文件夹");
        private WebException PathNotFound = new WebException("服务器上没有这个文件夹");
        #endregion
        #region 成员

        /// <summary>
        /// 配置
        /// </summary>
        private Config.config cfg;

        public bool GoAhead { get; set; }
        /// <summary>
        /// 是否添加认证头
        /// </summary>
        public bool UsingHeader
        {
            get
            {
                if (UsingHeader)
                    this.Headers.Add("Authorization", "Bearer " + cfg.token.access_token);
                else
                    this.Headers.Remove("Authorization");
                return UsingHeader;
            }
            set
            {
            }
        }
        #endregion 

        public Client(Config.config cfg)
        {
            UsingHeader = true;
            this.Headers.Add("Authorization", "Bearer " + cfg.token.access_token);
            this.cfg = cfg;
        }
        HttpWebRequest req;
        HttpWebResponse res;
        #region 下载
        delegate void StartDownloadHandle(HttpWebResponse res);
        public delegate void DownloadFileProgressChangedEventHandle(long Recived,long Total,byte[] buffer, int ThisRec);
        public event DownloadFileProgressChangedEventHandle DownloadFileProgressChangedEvent;
        public delegate void DownloadFileCompleteEventHandle();
        public event DownloadFileCompleteEventHandle DownloadFileComplete;
        public void DownloadFile(string path)
        {
            GoAhead = true;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(Config.Url.Download + path);
                req.Headers.Add("Authorization", "Bearer " + cfg.token.access_token);
                res = (HttpWebResponse)req.GetResponse();
                StartDownloadHandle StartDownloadDelegate = new StartDownloadHandle(StartDownload);
                StartDownloadDelegate.BeginInvoke(res, null, null);
            }
            catch (WebException ex)
            {
                if (ex.Message.IndexOf("401") != -1)
                {
                    throw NoAuth;
                }
                else if (ex.Message.IndexOf("404") != -1)
                {
                    throw FileNotFound;
                }
                else throw ex;
            }
        }

        private void StartDownload(HttpWebResponse res)
        {
            Stream s = res.GetResponseStream();
            long Recived=0;
            long Total = res.ContentLength;
            byte[] buffer = new byte[50 * 1024];
            while (true)
            {
                int bytes = s.Read(buffer, 0, 50 * 1024);
                Recived += bytes;
                if (DownloadFileProgressChangedEvent!=null)
                    DownloadFileProgressChangedEvent(Recived, Total, buffer, bytes);
                if (Recived ==res.ContentLength)
                    break;
            }
            if (DownloadFileComplete != null && GoAhead)
                DownloadFileComplete();
        }
        public void CancelDownload()
        {
            GoAhead = false;
        }
        #endregion

        #region 上传
        private delegate void ContinueUploadHandle(byte[] buffer,int length);
        public delegate void UploadBlockCompleteEventHandle(Client c);
        public event UploadBlockCompleteEventHandle UploadBlockCompleteEvent;
        public delegate void UploadFileCompleteEventHandle(Exception ex);
        public event UploadFileCompleteEventHandle UploadFileCompleteEvent;
        Stream UploadStream;
        public void UploadFile(string path,long Length)
        {
            req = (HttpWebRequest)WebRequest.Create(Config.Url.Upload + path);
            req.Headers.Add("Authorization", "Bearer " + cfg.token.access_token);
            req.Method = "POST";
            req.AllowWriteStreamBuffering = false;
            req.ContentLength = Length;
            req.ReadWriteTimeout = int.MaxValue;
            req.Timeout = int.MaxValue;
            UploadStream = req.GetRequestStream();
        }

        public void ContiuneUpload(byte[] buffer,int length)
        {
            UploadStream.Write(buffer, 0, length);
            //UploadStream.Write(buffer, 0, length);
            //UploadBlockCompleteEvent(this);
        }

        public void EndUpload()
        {
            try
            {
                UploadStream.Close();
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Message.IndexOf("401") != -1)
                    UploadFileCompleteEvent(NoAuth);
                else if (ex.Message.IndexOf("404") != -1)
                    UploadFileCompleteEvent(PathNotFound);
                else throw ex;
            }
            finally
            {
                UploadFileCompleteEvent(null);
            }
        }
        #endregion
    }
}
