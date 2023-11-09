using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using client.Views;
using System.Reactive.Linq;
using System;

namespace client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            //StartWindow start = new(){DataContext = new StartWindowViewModel()};
            //RegisterWindow reg = new(){DataContext = new RegisterWindowViewModel()};
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };

            /**
            start.ViewModel!.OpenMainCommand.Subscribe(result =>
            {
                main.DataContext = result;
                desktop.MainWindow = main;
                main.Show();
                start.Close();
                reg.Close();
            });

            start.ViewModel!.OpenRegisterCommand.Subscribe(result =>
            {
                reg.DataContext = result;
                desktop.MainWindow = reg;
                reg.Show();
            });**/
        }

        base.OnFrameworkInitializationCompleted();
    }
}