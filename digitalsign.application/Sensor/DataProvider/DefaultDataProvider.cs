using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace digitalsign.application.Sensor.DataProvider
{
    public class DefaultDataProvider : IDataProvider
    {
        public void Receive(Action<string> onValueReceived, CancellationToken token)
        {
            var random = new Random();
            Task.Factory.StartNew(() =>
            {
                var someValue = 24.0;
                while (true)
                {

                    if (token.IsCancellationRequested) break;
                    someValue += (random.NextDouble() - 0.5) * -1;
                    onValueReceived(someValue.ToString(CultureInfo.CurrentCulture));
                    token.WaitHandle.WaitOne(500);
                }
            }, token, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
        }
    }
}
