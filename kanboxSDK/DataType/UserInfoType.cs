using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace kanboxSDK.DataType
{
    [DataContract]
    public class UserInfoType
    {
        [DataMember]
        public string email;
        [DataMember]
        public long spaceQuota, spaceUsed;
    }
}
