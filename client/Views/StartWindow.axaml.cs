using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using client.ViewModels;

namespace client.Views;

public partial class StartWindow : ReactiveWindow<StartWindowViewModel>
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public StartWindow()
    {
        InitializeComponent();
    }
}