using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VelibEventsLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class VelibService : IVelibService
    {
        static Action<string, string, string> m_Event1 = delegate { };

        static string token = "b0e7b9c0530c7797ba8f5dfdff2ea74e47fcf958";
        
        private static string stationDataRequest = "https://api.jcdecaux.com/vls/v1/stations?contract=";
        
        private List<Station> stations;

        public void GetStationData(string stationName, string cityName)
        {
            WebRequest request = WebRequest.Create(stationDataRequest + cityName + "&apiKey=" + token);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string serverResponse = reader.ReadToEnd();

            string research = stationName;

            stations = JsonConvert.DeserializeObject<Station[]>(serverResponse).ToList();
            Station stationSearched = null;

            foreach (Station station in stations)
            {
                string name = station.name;

                if (name.ToLower().Contains(research.ToLower()))
                {
                    stationSearched = station;
                    break;
                }
            }

            StringBuilder output = new StringBuilder();

            if (stationSearched != null)
            {
                output.AppendLine("Station :" + stationSearched.name);
                output.AppendLine("Nombre de vélos disponibles :" + stationSearched.available_bikes);
            }
            else
            {

                output.AppendLine("Cette station n'existe pas");

            }
            

            m_Event1(cityName, stationName, output.ToString());
            
        }

        

        public void SubscribeStationData(string stationName, string cityName, int refreshingTime)
        {
            IVelibServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IVelibServiceEvents>();
            m_Event1 += subscriber.Calculated;

            Task.Run(() => {
                while (true)
                {
                    GetStationData(stationName, cityName);
                    Thread.Sleep(refreshingTime * 1000);
                }
            });
            

        }
    }
}
