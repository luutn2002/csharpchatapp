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
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args); 
    }
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UseReactiveUI()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
