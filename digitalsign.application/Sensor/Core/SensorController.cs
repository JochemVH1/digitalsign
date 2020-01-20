using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using digitalsign.application.Sensor.DataProvider;

namespace digitalsign.application.Sensor.Core
{
    public class SensorController
    {
        private readonly ConcurrentDictionary<string, Sensor> _sensors;

        private readonly RegistrationManager _registrationManager;

        private readonly ReplaySubject<SensorData> _controller;

        public IDataProvider DataProvider { get; set; }
        public int BufferSize { get; set; }

        public SensorController()
        {
            _sensors = new ConcurrentDictionary<string, Sensor>();
            _controller = new ReplaySubject<SensorData>(BufferSize);
            _registrationManager = new RegistrationManager();
        }

        private RegistrationResult ValidateSensor(string token)
        {
            var validation = _registrationManager.Validate(token);
            if(validation.State.Equals(ValidationState.Validated))
            {
                return validation;
            }
            _registrationManager.Register(token);
            return _registrationManager.Validate(token);
        }

        private bool TryAdd(Sensor sensor)
        {
            if (!sensor.RegistrationResult.State.Equals(ValidationState.Validated)) return false;
            if (!_sensors.TryAdd(sensor.RegistrationResult.Token, sensor)) return false;
            sensor.SensorData.Subscribe(_controller);
            return true;
        }

        private bool TryGet(string token, out Sensor sensor)
        {
            try
            {
                sensor = _sensors[token];
                return true;
            }
            catch (Exception)
            {
                sensor = null;
                return false;
            }
        }


        public void Peek(string token)
        {
            try
            {
                var sub = _controller
                    .Where(x => x.State.Equals(ValidationState.Validated) && x.Token.Equals(token))
                    .TakeLast(1)
                    .Subscribe(x =>
                    {
                        Console.WriteLine($"{x.Name} ({x.Token}) produced: {x.Data}");
                    }, () =>
                    {
                        Console.WriteLine($"Completed");
                    });
                sub.Dispose();
            }
            catch (AggregateException ae)
            {
                ae.Handle((e) => true);
            }

        }

        public bool StartSensor(string sensorName, string receivedSensorToken, in CancellationToken token)
        {
            if (TryGet(receivedSensorToken, out var sensor))
            {
                sensor.ReceiveData(token);
                return true;
            }
            sensor = new Sensor(sensorName)
            {
                RegistrationResult = ValidateSensor(receivedSensorToken),
                DataProvider = DataProvider
            };
            if (!TryAdd(sensor)) return false;
            sensor.ReceiveData(token);
            return true;

        }
    }
}
