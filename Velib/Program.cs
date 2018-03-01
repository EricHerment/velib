using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using Newtonsoft.Json.Linq;

namespace Velib
{
    class Program {

        static string token = "b0e7b9c0530c7797ba8f5dfdff2ea74e47fcf958";


        static string requestToServer() {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=Toulouse&apiKey=" + token);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string serverResponse = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return serverResponse;
        }

        static void Main(string[] args) {
            string research = "VALADE";
            string responseFromServer = requestToServer();
            JArray j = JArray.Parse(responseFromServer);
            int number = 0;
            string station = null;

            foreach (JObject item in j) {
                string name = (string) item.SelectToken("name");

                if (name.Contains(research)) {
                    number = (int) item.SelectToken("available_bikes");
                    station = name;
                    break;
                }
            }

            if (station != null) {
                Console.WriteLine("Station :" + station);
                Console.WriteLine("Nombre de vélos disponibles :" + number);
            }
            else
                Console.WriteLine("Nom invalide");
            

            Console.Read();
        }
    }
}
