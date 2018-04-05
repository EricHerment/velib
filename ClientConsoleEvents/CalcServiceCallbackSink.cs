using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleEvents
{
    class CalcServiceCallbackSink : VelibServiceReference.IVelibServiceCallback
    {
       
        public void Calculated(string city, string station, string nbVelib)
        {
            Console.WriteLine("Ville: {0}", city);
            Console.WriteLine("Station: {0}", station);
            Console.WriteLine("Velos :{0}", nbVelib);
        }

        public void CalculationFinished()
        {
            Console.WriteLine("Calculation completed");
        }
    }
}
