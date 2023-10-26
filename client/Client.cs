using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Net.Sockets;    
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace client;

class Client
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    /// <summary>
    /// Main methods check for connection and build UI
    /// </summary>
    /// <param name="args"></param>
    [STAThread]
    public static void Main(string[] args) {
        //CheckConnectionAsync().Wait();
        //CheckConnectionGrpc().Wait();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args); 
    }

    private static readonly IPEndPoint ipEndPoint = new(IPAddress.Parse("127.0.0.1"), 54321);
    private static readonly TcpClient client = new();

    /// <summary>
    /// This method connect created client to ip endpoint and receive a message from server
    /// </summary>
    /// <returns>
    /// None
    /// </returns>
    static async Task CheckConnectionAsync()
    {
        try{
            
            await client.ConnectAsync(ipEndPoint);
            await using NetworkStream stream = client.GetStream();

            var message = $"📅 {DateTime.Now} 🕛";
            var dateTimeBytes = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(dateTimeBytes);

            Console.WriteLine($"Sent message: \"{message}\"");

        } catch (SocketException)
        {
            client.Close();
        } finally{
            client.Close();
        }
    }

    static async Task CheckConnectionGrpc()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7259");
        var client = new Greeter.GreeterClient(channel);
        var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
        Console.WriteLine("Greeting: " + reply.Message);
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UseReactiveUI()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
