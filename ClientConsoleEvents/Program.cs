using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcServiceCallbackSink objsink = new CalcServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);

            VelibServiceReference.VelibServiceClient objClient = new VelibServiceReference.VelibServiceClient(iCntxt);
            objClient.SubscribeCalculatedEvent();
            objClient.SubscribeCalculationFinishedEvent();

            Console.WriteLine("Donnez le nom de la ville");
            string ville = Console.ReadLine();
            Console.WriteLine("Donnez le nom de la station");
            string station = Console.ReadLine();
            objClient.GetStationData(station, ville);

           

            Console.WriteLine("Press any key to close ...");
            Console.ReadKey();
        }
    }
}
