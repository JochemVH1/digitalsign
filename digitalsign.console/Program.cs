using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using digitalsign.application.Sensor.Core;

namespace digitalsign.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = SensorController.Instance();
            //controller.Monitor();
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var receivedSensorToken = Guid.NewGuid().ToString();
            var sensorName = "Sensor1";
            var sensorName2 = "Sensor2";
            var receivedSensorToken2 = Guid.NewGuid().ToString();

            if(controller.TryGet(receivedSensorToken, out var sensor))
            {
                sensor.ReceiveData(token);
            }else
            {
                sensor = new Sensor(sensorName)
                {
                    RegistrationResult = controller.ValidateSensor(receivedSensorToken)
                };
                if (controller.TryAdd(sensor))
                {
                    sensor.ReceiveData(token);
                }
            }
            if (controller.TryGet(receivedSensorToken2, out var sensor2))
            {
                sensor2.ReceiveData(token);
            }
            else
            {
                sensor2 = new Sensor(sensorName2)
                {
                    RegistrationResult = controller.ValidateSensor(receivedSensorToken2)
                };
                if (controller.TryAdd(sensor2))
                {
                    sensor2.ReceiveData(token);
                }
            }
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested) break;
                    controller.Peek(receivedSensorToken);
                    controller.Peek(receivedSensorToken2);
                    Thread.Sleep(5000);
                }
            }, token);

            Task.Factory.StartNew(action: () =>
            {
                var serverSocket = new TcpListener(IPAddress.Any, 8888);
                serverSocket.Start();
                var clientSocket = serverSocket.AcceptTcpClient();

                while (true)
                {
                    try
                    {
                        if (token.IsCancellationRequested) break;
                        var networkStream = clientSocket.GetStream();
                        var bytesFrom = new byte[10025];
                        networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                        var dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                        var serverResponse = "Last Message from client" + dataFromClient;
                        var sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }, token);


            Console.ReadKey();
            cts.Cancel();
            cts.Dispose();
        }
    }
}
