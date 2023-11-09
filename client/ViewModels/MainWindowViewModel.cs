using System;
using System.Reactive;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace client.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _title = "Login";
    public string Title
    {
        get => _title;
        private set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private string _swapButtonName = "Register";
    public string SwapButtonName
    {
        get => _swapButtonName;
        private set => this.RaiseAndSetIfChanged(ref _swapButtonName, value);
    }
    private LoginPanelViewModel _loginViewModel;
    public LoginPanelViewModel LoginViewModel
    {
        get => _loginViewModel;
        private set => this.RaiseAndSetIfChanged(ref _loginViewModel, value);
    }
    private RegisterPanelViewModel _registerViewModel;
    public RegisterPanelViewModel RegisterViewModel
    {
        get => _registerViewModel;
        private set => this.RaiseAndSetIfChanged(ref _registerViewModel, value);
    }
    public ReactiveCommand<Unit, Unit> SwapRegisterAndLogin { get; }
    public MainWindowViewModel()
    {
        _loginViewModel = new LoginPanelViewModel();
        _registerViewModel = new RegisterPanelViewModel();
        _loginViewModel._isShowingLoginPanel = true;

        SwapRegisterAndLogin = ReactiveCommand.Create(() => {
            _loginViewModel.IsShowingLoginPanel = !_loginViewModel.IsShowingLoginPanel;
            _registerViewModel.IsShowingRegisterPanel = !_loginViewModel.IsShowingLoginPanel;
            SwapButtonName = _loginViewModel.IsShowingLoginPanel ? "Register": "Back to Login";
            Title = _loginViewModel.IsShowingLoginPanel ? "Login": "Register";

        });
    }
}
