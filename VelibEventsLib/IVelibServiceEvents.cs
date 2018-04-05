using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VelibEventsLib
{
    
    interface IVelibServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void Calculated(string city, string station, string nbVelib);

        [OperationContract(IsOneWay = true)]
        void CalculationFinished();
        
    }
}

