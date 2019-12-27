using System;
using System.Threading;

namespace digitalsign.application.Sensor.DataProvider
{
    public interface IDataProvider
    { 
        void Receive(Action<string> onValueReceived, CancellationToken token);
    }
}
