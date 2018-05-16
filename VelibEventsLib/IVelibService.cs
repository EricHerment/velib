using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VelibEventsLib
{
    [ServiceContract(CallbackContract = typeof(IVelibServiceEvents))]
    interface IVelibService
    {
        [OperationContract]
        void SubscribeStationData(string stationName, string cityName, int refreshingTime);
    }
}
