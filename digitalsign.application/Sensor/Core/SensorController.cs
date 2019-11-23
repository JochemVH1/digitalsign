using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using digitalsign.console;

namespace digitalsign.application.Sensor.Core
{
    public class SensorController
    {
        private readonly ConcurrentDictionary<string, Sensor> _sensors;

        private readonly RegistrationManager _registrationManager = RegistrationManager.Instance();

        private readonly ReplaySubject<SensorData> _controller;

        private static SensorController _sensorController;

        public static SensorController Instance()
        {
            return _sensorController ??= new SensorController();
        }

        private SensorController()
        {
            _sensors = new ConcurrentDictionary<string, Sensor>();
            _controller = new ReplaySubject<SensorData>(10);
        }

        public RegistrationResult ValidateSensor(string token)
        {
            var validation = _registrationManager.Validate(token);
            if(validation.State.Equals(ValidationState.Validated))
            {
                return validation;
            }
            _registrationManager.Register(token);
            return _registrationManager.Validate(token);
        }

        public bool TryAdd(Sensor sensor)
        {
            if (!sensor.RegistrationResult.State.Equals(ValidationState.Validated)) return false;
            if (!_sensors.TryAdd(sensor.RegistrationResult.Token, sensor)) return false;
            sensor.SensorData.Subscribe(_controller);
            return true;
        }

        public bool TryGet(string token, out Sensor sensor)
        {
            try
            {
                sensor = _sensors[token];
                return true;
            }
            catch(Exception)
            {
                sensor = null;
                return false;
            }
        }


        public void Monitor()
        {
            _controller
                .Where(x => x.State.Equals(ValidationState.Validated))
                .Inspect("controller");
        }

        public void Peek(string token)
        {
            var sub = _controller
                .Where(x => x.State.Equals(ValidationState.Validated) && x.Token.Equals(token))
                .FirstAsync()
                .Subscribe(x => Console.WriteLine($"{x.Name} ({x.Token}) produced: {x.Data}"), () => { });
            sub.Dispose();
        }
    }
}
