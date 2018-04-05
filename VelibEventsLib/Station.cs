using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VelibEventsLib
{
    [DataContract]
    public class Station
    {
        
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int available_bikes { get; set; }
    }
}
