using System;
using System.Globalization;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace digitalsign.application.Sensor.Core
{
    public class Sensor
    {
        public string Name { get; set; }

        public RegistrationResult RegistrationResult { get; set; }

        private readonly Subject<SensorData> _internalData;

        public IObservable<SensorData> SensorData { get; set; }

        public Sensor(string name)
        {
            Name = name;
            _internalData = new Subject<SensorData>();
            SensorData = _internalData;
        }

        public void OnValueReceived(string data)
        {
            _internalData.OnNext(new SensorData
            {
                Data = data,
                Name = Name,
                State = RegistrationResult.State,
                Token =  RegistrationResult.Token
            });
        }

        public void ReceiveData(CancellationToken token)
        {
            var random = new Random();
            Task.Factory.StartNew(() =>
            {
                var temperature = 24.0;
                while (true)
                {

                    if (token.IsCancellationRequested) break;
                    temperature += (random.NextDouble() - 0.5) * -1;
                    OnValueReceived(temperature.ToString(CultureInfo.CurrentCulture));
                    Thread.Sleep(500);
                }
            }, token);
        }
    }

    public struct SensorData
    {
        public string Name { get; set; }

        public string Token { get; set; }

        public ValidationState State { get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Token}): produced {Data}";
        }
    }
}
