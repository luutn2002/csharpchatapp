using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace client.Views;

public partial class RegisterPanel : UserControl
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public RegisterPanel()
    {
        InitializeComponent();
    }
}