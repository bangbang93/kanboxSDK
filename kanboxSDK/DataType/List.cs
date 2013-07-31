using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace kanboxSDK.DataType
{
    [DataContract]
    class List
    {
        [DataMember]
        public string status, hash;
        [DataMember]
        public FileInfo[] contents;
    }

    [DataContract]
    class FileInfo
    {
        [DataMember]
        public string fullPath, modificationDate;
        [DataMember]
        public long fileSize;
        [DataMember]
        public bool isFolder;
    }
}
