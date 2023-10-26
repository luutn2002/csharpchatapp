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
            var dialog = new StartWindow()
            {
                DataContext = new StartWindowViewModel(),
            };

            dialog.ViewModel!.OpenMainCommand.Subscribe(result =>
            {
                var mw = new MainWindow
                {
                    DataContext = result,
                };
                desktop.MainWindow = mw;
                mw.Show();
                dialog.Close();
            });

            dialog.ViewModel!.OpenRegisterCommand.Subscribe(result =>
            {
                var reg = new RegisterWindow
                {
                    DataContext = result,
                };
                desktop.MainWindow = reg;
                reg.Show();
            });


            desktop.MainWindow = dialog;
        }

        base.OnFrameworkInitializationCompleted();
    }
}