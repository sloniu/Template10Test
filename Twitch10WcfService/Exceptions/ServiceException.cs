using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Twitch10WcfService.Exceptions
{
    [DataContract]
    public class ServiceException
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}