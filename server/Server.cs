using System.Net.Sockets;    
using System.Text;
using System.Net;
using server.Logger;
using server.Services;
using server.Processor;

namespace server
{
    class Server
    {
        private static readonly IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 54321);
        private static readonly TcpListener listener = new(ipEndPoint);
        static readonly EventLogger logger = new();
        /// <summary>
        /// Accepting incomming connection and test connection by sending a message.
        /// </summary>
        /// <returns>
        /// None
        /// </returns>
        static async Task TestConnectionAcceptAsync()
        {
            try
            {    
                listener.Start();
                logger.GeneralLog(0, "Listener started");
                using TcpClient handler = await listener.AcceptTcpClientAsync();
                logger.GeneralLog(0, "Client accepted");
                await using NetworkStream stream = handler.GetStream();
                logger.GeneralLog(0, "Network stream received");

                var buffer = new byte[1_024];
                int received = await stream.ReadAsync(buffer);

                var message = Encoding.UTF8.GetString(buffer, 0, received);
                logger.GeneralLog(0, $"Message received: \"{message}\". Connection to server is visible.");
            }
            finally
            {
                listener.Stop();
                logger.GeneralLog(0, "Server stopped listening for test message.");
            }  
        }

        static async Task ProcessIncomingSignal()
        {
            try
            {    
                listener.Start();
                using TcpClient handler = await listener.AcceptTcpClientAsync();
                await using NetworkStream stream = handler.GetStream();
                
                while(true){
                    var buffer = new byte[8_192];
                    int receivedMessage = await stream.ReadAsync(buffer);

                    var message = Encoding.UTF8.GetString(buffer, 0, receivedMessage);
                    if (string.Compare(message, "TERM") == 0) 
                    {
                        Console.WriteLine("Client terminate connection. Aborting...");
                        break;
                    } else {

                    }
                }
            }
            finally
            {
                listener.Stop();
                Console.WriteLine("Server stopped listening");
            }  
        }
        static void Main(string[] args)
        {
            //DatabaseProcessor.TestConnectSQLServer();
            //TestConnectionAcceptAsync().Wait();
            //var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            //builder.Services.AddGrpc();

            //var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.MapGrpcService<GreeterService>();
            //app.Run();
        }
    } 
}