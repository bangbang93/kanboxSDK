using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Net;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;

using kanboxSDK.Util;
using kanboxSDK.DataType;
using kanboxSDK.Connect;
using kanboxSDK.Config;

namespace kanboxSDK
{
    public class Work
    {
        #region 属性
        public Config.config cfg { private set; get; }
        public string hash { private set; get; }
        private string local;
        private string path;
        private bool GoAhead;
        #endregion

        #region 成员
        private HttpWebRequest req;
        Client Worker;
        #endregion

        #region 异常
        private WebException NoAuth = new WebException("授权过期或未授权");
        private WebException FileNotFound = new WebException("服务器上没有这个文件或者这是个文件夹");
        private WebException PathNotFound = new WebException("服务器上没有这个文件夹");
        #endregion
        public Work(Config.config cfg)
        {
            this.cfg=cfg;
            this.hash = "";
        }
        #region 文件列表
        public delegate void ListFileCompleteEventHandle(DataTable dt);
        public event ListFileCompleteEventHandle ListFileCompleteEvent;
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="path">获取的目录</param>
        /// <returns>文件列表DataTable</returns>
        public void ListFile(string path="/")
        {
            StringBuilder url = new StringBuilder(Config.Url.FileList);
            Client Worker=new Client(cfg);
            Worker.QueryString.Add("path", path);
            Worker.QueryString.Add("hash", hash);
            Worker.DownloadStringAsync(new Uri(url.ToString()));
            Worker.DownloadStringCompleted += ListFileComplete;
        }
        private void ListFileComplete(object sender,DownloadStringCompletedEventArgs e)
        {
            if (e.Error!= null)
            {
                if (e.Error.Message.IndexOf("401") != -1)
                {
                    throw NoAuth;
                }
            }
            string FileInfoJson = e.Result;
            byte[] buffer=Encoding.UTF8.GetBytes(FileInfoJson);
            Stream FileInfo = new MemoryStream(buffer);
            DataContractJsonSerializer FileInfoJsonSer = new DataContractJsonSerializer(typeof(List));
            List FileList = FileInfoJsonSer.ReadObject(FileInfo) as List;
            if (FileList.status == "nochange")
            {
                ListFileCompleteEvent(null);
                return;
            }
            hash = FileList.hash;
            DataTable dt = new DataTable();
            dt.Columns.Add("fullPath");
            dt.Columns.Add("modificationDate");
            dt.Columns.Add("fileSize");
            dt.Columns.Add("isFolder");
            foreach (DataType.FileInfo File in FileList.contents)
            {
                DataRow dr = dt.NewRow();
                dr["fullPath"] = File.fullPath;
                dr["modificationDate"] = File.modificationDate;
                dr["fileSize"] = File.fileSize;
                dr["isFolder"] = File.isFolder;
                dt.Rows.Add(dr);
            }
            ListFileCompleteEvent(dt);
        }
        #endregion

        #region 下载
        public delegate void DownloadFileCompleteEventHandle(Exception ex);
        /// <summary>
        /// 下载完成
        /// </summary>
        public event DownloadFileCompleteEventHandle DownloadFileCompleteEvent;
        public delegate void DownloadFileProgressChangedEventhandle(long fin, long total,byte[] buffer,int ThisRec);
        /// <summary>
        /// 下载进度变化
        /// </summary>
        public event DownloadFileProgressChangedEventhandle DownloadFileProgressChangedEvent;
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path">远程路径</param>
        public void DownloadFile(string path)
        {
            GoAhead = true;
            this.path = path;
            Worker = new Client(cfg);
            Worker.DownloadFile(path);
            Worker.DownloadFileProgressChangedEvent += Worker_DownloadFileProgressChangedEvent;
            Worker.DownloadFileComplete += Worker_DownloadFileComplete;
        }

        void Worker_DownloadFileComplete()
        {
            DownloadFileCompleteEvent(null);
        }

        void Worker_DownloadFileProgressChangedEvent(long Recived, long Total, byte[] buffer,int ThisRec)
        {
            DownloadFileProgressChangedEvent(Recived, Total, buffer,ThisRec);
        }
        public void CancelDownlaod()
        {
            GoAhead = false;
            Worker.GoAhead = false;
        }



        
        #endregion

        #region 上传
        public delegate void UploadFileCompleteEventHandle(Exception ex);
        /// <summary>
        /// 上传完成
        /// </summary>
        public event UploadFileCompleteEventHandle UploadFileCompleteEvent;
        public delegate void UploadFileProgressChangedEventHandle(long complete);
        /// <summary>
        /// 上传进度变化
        /// </summary>
        public event UploadFileProgressChangedEventHandle UploadFileProgressChangedEvent;
        private delegate void UploadThreadDelegate(Client c);
        private Stream source;
        private int block = 50 * 1024;
        private long Send = 0;
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">上传的路径，包括文件名</param>
        /// <param name="source">上传源</param>
        /// <param name="Length">文件长度</param>
        public void UploadFile(string path,Stream source,long Length)
        {
            GoAhead = true;
            this.path = path;
            this.source = source;
            Worker = new Client(cfg);
            Worker.UploadFile(path, Length);
            Worker.UploadBlockCompleteEvent += UploadBlockComplete;
            Worker.UploadFileCompleteEvent += UploadFileComplete;
            UploadThreadDelegate UploadThread = new UploadThreadDelegate(UploadBlockComplete);
            UploadThread.BeginInvoke(Worker, null, null);

        }

        void UploadFileComplete(Exception ex)
        {
            if (UploadFileCompleteEvent != null)
                UploadFileCompleteEvent(ex);
        }

        void UploadBlockComplete(Client c)
        {
            while (GoAhead)
            {
                byte[] buffer = new byte[block];
                int bytes = source.Read(buffer, 0, block);
                Send += bytes;
                if (bytes == 0)
                {
                    c.EndUpload();
                    return;
                }
                if (UploadFileProgressChangedEvent != null)
                    UploadFileProgressChangedEvent(Send);
                c.ContiuneUpload(buffer, bytes);
            }
        }

        public void CancelUpload()
        {
            this.GoAhead = false;
            Worker.GoAhead = false;
        }

        #endregion

        #region 删除
        public delegate void DeleteFileCompleteEventHandle(string status);
        public event DeleteFileCompleteEventHandle DeleteFileCompleteEvent;
        public void DeleteFile(string path)
        {
            StringBuilder Url = new StringBuilder(Config.Url.Delete);
            Url.Append(path);
            Client Worker = new Client(cfg);
            Worker.DownloadStringCompleted += DeleteFileComplete;
            Worker.DownloadStringAsync(new Uri(Url.ToString()));

        }

        void DeleteFileComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (e.Error.Message.IndexOf("401") != -1)
                {
                    throw NoAuth;
                }
                else
                    if (e.Error.Message.IndexOf("404") != -1)
                    {
                        throw FileNotFound;
                    }
            }
            DeleteFileCompleteEvent(e.Result);
        }
        #endregion
    }
}
