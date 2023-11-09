using Grpc.Core;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace client.ViewModels;

public class RegisterPanelViewModel : ViewModelBase
{
  public bool _isShowingRegisterPanel;
  public bool IsShowingRegisterPanel
  {
      get => _isShowingRegisterPanel;
      set => this.RaiseAndSetIfChanged(ref _isShowingRegisterPanel, value);
  }
  readonly ObservableAsPropertyHelper<bool> _isContentValid;
  public bool IsContentValid => _isContentValid.Value;
  public string RegisterUsername
  {
    get => _username;
    set => this.RaiseAndSetIfChanged(ref _username, value);
  }

  public string RegisterPassword
  {
    get => _password;
    set => this.RaiseAndSetIfChanged(ref _password, value);
  }
  public string RegisterAction
  {
    get => _action;
    set => this.RaiseAndSetIfChanged(ref _action, value);
  }
  public string RegisterServerMessage
  {
    get => _serverMessage;
    set => this.RaiseAndSetIfChanged(ref _serverMessage, value);
  }
  public ReactiveCommand<Unit, Task> AccountRegisterCommand { get; private set;}
  private IObservable<Task> _accountRegisterCommand()
  {
    return Observable.Start(() => {
      try
      {
        RegisterServerMessage = "Connecting to server...";
        var reply = _grpcCommand().Result;
        RegisterServerMessage = reply.Message ? $"Successfully {RegisterAction} user." : $"Failed to {RegisterAction}, please try again.";
      }
      catch (AggregateException e) {
        var inner = e.InnerException;
        if (inner is RpcException casted) {
            RegisterServerMessage = $"Cannot connect to server, \n status code: {casted.StatusCode}";
        }
      }           
      return Task.CompletedTask;
    });
  }
  public RegisterPanelViewModel()
  {
    RegisterAction = "register";

    IObservable<bool> isInputValid = this.WhenAnyValue(
      x => x.RegisterUsername, x => x.RegisterPassword, (username, password) => 
      !string.IsNullOrWhiteSpace(username) && username.Length > 7 && !username.Any(char.IsWhiteSpace) &&
      !string.IsNullOrWhiteSpace(password) && password.Length > 7 && !password.Any(char.IsWhiteSpace)
    );

    _isContentValid = isInputValid.Select(value => !value).ToProperty(this, x => x.IsContentValid);

    AccountRegisterCommand = ReactiveCommand.CreateFromObservable(_accountRegisterCommand, isInputValid);
  }
}
