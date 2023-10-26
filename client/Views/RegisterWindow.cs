using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using client.ViewModels;

namespace client.Views;

public partial class RegisterWindow : ReactiveWindow<RegisterWindowViewModel>
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public RegisterWindow()
    {
        InitializeComponent();
    }
}