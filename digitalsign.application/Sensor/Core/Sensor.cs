using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Subjects;
using System.Threading;
using digitalsign.application.Sensor.DataProvider;

namespace digitalsign.application.Sensor.Core
{
    public class Sensor
    {
        private string Name { get; }
        public RegistrationResult RegistrationResult { get; set; }

        private readonly Subject<SensorData> _internalData;
        public IObservable<SensorData> SensorData { get; set; }
        public IDataProvider DataProvider { get; set; }

        public Sensor(string name)
        {
            Name = name;
            _internalData = new Subject<SensorData>();
            SensorData = _internalData;
        }

        private void OnValueReceived(string data)
        {
            Debug.WriteLine(data);
            Data = data;
            _internalData.OnNext(new SensorData
            {
                Data = data,
                Name = Name,
                State = RegistrationResult.State,
                Token =  RegistrationResult.Token
            });
        }

        public string Data { get; set; }

        public void ReceiveData(CancellationToken token)
        {
            DataProvider.Receive(OnValueReceived, token);
        }
    }

    public struct SensorData : IEquatable<SensorData>
    {
        public string Name { get; set; }

        public string Token { get; set; }

        public ValidationState State { get; set; }
        public string Data { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SensorData data &&
                   Name == data.Name &&
                   Token == data.Token &&
                   State == data.State &&
                   Data == data.Data;
        }

        public bool Equals([AllowNull] SensorData other)
        {
            return Name == other.Name &&
                   Token == other.Token &&
                   State == other.State &&
                   Data == other.Data;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Token, State, Data);
        }

        public override string ToString()
        {
            return $"{Name} ({Token}): produced {Data}";
        }


        public static bool operator ==(SensorData left, SensorData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SensorData left, SensorData right)
        {
            return !(left == right);
        }
    }
}
