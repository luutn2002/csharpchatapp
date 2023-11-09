using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace client.Views;

public partial class LoginPanel : UserControl
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public LoginPanel()
    {
        InitializeComponent();
    }
}