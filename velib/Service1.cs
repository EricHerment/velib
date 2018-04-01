using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Runtime.Caching;
using Newtonsoft.Json;

namespace velib
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        static string token = "b0e7b9c0530c7797ba8f5dfdff2ea74e47fcf958";
        private static string citiesRequest = "https://api.jcdecaux.com/vls/v1/contracts?apiKey=";
        private static string stationDataRequest = "https://api.jcdecaux.com/vls/v1/stations?contract=";
        private static string stationsRequest = "https://api.jcdecaux.com/vls/v1/stations?contract=";
        private int cacheBuffer = 15;
        ObjectCache cache = MemoryCache.Default;
        private List<Station> stations;
        private List<City> cities;

        public List<City> GetCities()
        {
            WebRequest request = WebRequest.Create(citiesRequest + token);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string serverResponse = reader.ReadToEnd();
            cities = JsonConvert.DeserializeObject<City[]>(serverResponse).ToList();

            return cities;
        }

        public string GetStationData(string stationName, string cityName)
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
            return output.ToString();
        }

        public List<Station> GetStations(string cityName)
        {
            if (!cache.Contains(cityName))
            {
                WebRequest request = WebRequest.Create(stationsRequest + cityName + "&apiKey=" + token);
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string serverResponse = reader.ReadToEnd();


                stations = JsonConvert.DeserializeObject<Station[]>(serverResponse).ToList();

                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheBuffer)
                };
                CacheItem item = new CacheItem(cityName, stations);
                cache.Add(item, policy);
            }

            else
            {
                stations = (List<Station>) cache.Get(cityName);
            }

            return stations;
        }
    }
}
