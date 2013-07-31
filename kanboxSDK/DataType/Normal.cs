using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace kanboxSDK.DataType
{
    [DataContract]
    class Normal
    {
        [DataMember]
        public string status;
        [DataMember(IsRequired = false)]
        public string errorCode;
    }
}
