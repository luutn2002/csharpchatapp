using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using client.ViewModels;

namespace client.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public MainWindow()
    {
        InitializeComponent();
    }
}