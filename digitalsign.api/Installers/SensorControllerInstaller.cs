using System;
using System.Threading;
using System.Threading.Tasks;
using digitalsign.application.Sensor.Core;
using digitalsign.application.Sensor.DataProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digitalsign_api.Installers
{
    public class SensorControllerInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            //var sensorController = new SensorController
            //{
            //    BufferSize = 10,
            //    DataProvider = new DefaultDataProvider()
            //};
            //var cts = new CancellationTokenSource();
            //var token = cts.Token;
            //var sensorName = "Sensor1";
            //var receivedSensorToken = Guid.NewGuid().ToString();
            //var sensorName2 = "Sensor2";
            //var receivedSensorToken2 = Guid.NewGuid().ToString();

            //sensorController.StartSensor(sensorName, receivedSensorToken, token);
            //sensorController.StartSensor(sensorName2, receivedSensorToken2, token);
            //services.AddSingleton(sensorController);
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (token.IsCancellationRequested) break;
            //        sensorController.Peek(receivedSensorToken);
            //        sensorController.Peek(receivedSensorToken2);
            //        token.WaitHandle.WaitOne(5000);
            //    }
            //}, token, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
        }
    }
}
