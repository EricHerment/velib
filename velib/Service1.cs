using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace velib
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        static string token = "b0e7b9c0530c7797ba8f5dfdff2ea74e47fcf958";


        public string GetData(string stationName)
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=Toulouse&apiKey=" + token);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string serverResponse = reader.ReadToEnd();

            string research = stationName;
            string responseFromServer = serverResponse;
            JArray j = JArray.Parse(responseFromServer);
            int number = 0;
            string station = null;

            foreach (JObject item in j)
            {
                string name = (string) item.SelectToken("name");

                if (name.ToLower().Contains(research.ToLower()))
                {
                    number = (int) item.SelectToken("available_bikes");
                    station = name;
                    break;
                }
            }

            StringBuilder output = new StringBuilder();

            if (station != null)
            {
                output.AppendLine("Station :" + station);
                output.AppendLine("Nombre de vélos disponibles :" + number);
            }
            else
            {
                output.AppendLine("Invalid name");
                output.AppendLine("Type 'stations' for a list of the stations available");
            }
               

            return output.ToString();

        }

        
    }
}
