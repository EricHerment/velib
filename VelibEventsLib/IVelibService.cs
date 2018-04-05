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
        void GetStationData(string stationName, string cityName);

        [OperationContract]
        void SubscribeCalculatedEvent();

        [OperationContract]
        void SubscribeCalculationFinishedEvent();

        /*[OperationContract]
        List<Station> GetStations(string cityName);

        [OperationContract]
        List<City> GetCities();*/
    }
}
