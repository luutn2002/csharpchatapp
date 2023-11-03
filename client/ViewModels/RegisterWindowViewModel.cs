using ReactiveUI;
using System;
using System.Reactive;
using Grpc.Net.Client;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Grpc.Core;

namespace client.ViewModels;

public class RegisterWindowViewModel : ViewModelBase
{
  private string _registerUsername = string.Empty;
  public string RegisterUsername
  {
    get => _registerUsername;
    set => this.RaiseAndSetIfChanged(ref _registerUsername, value);
  }

  private string _registerPassword = string.Empty;
  public string RegisterPassword
  {
    get => _registerPassword;
    set => this.RaiseAndSetIfChanged(ref _registerPassword, value);
  }

  private string _serverMessage = string.Empty;
  public string ServerMessage
  {
    get => _serverMessage;
    set => this.RaiseAndSetIfChanged(ref _serverMessage, value);
  }
  public ReactiveCommand<Unit, Task> AccountRegisterCommand { get; private set;}
  public ReactiveCommand<Unit, StartWindowViewModel> OpenStartCommand { get; }
  public IObservable<Task> _accountRegisterCommand()
  {
    return Observable.Start(async() => 
    {
      try{
        ServerMessage = "Connecting to server...";
        using var channel = GrpcChannel.ForAddress("https://localhost:7259");
        var client = new UserCredentialSender.UserCredentialSenderClient(channel);
        var reply = await client.SendUserCredentialsAsync(
          new UserCredentials { 
            Username = RegisterUsername, 
            Password = RegisterPassword,
        }, deadline: DateTime.UtcNow.AddSeconds(5));
        ServerMessage = $"Successfully register user: {reply.Message}";
      }
      catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
      {
        ServerMessage = "Connection to server timmed out.";
      }
    }); 
  }
  public RegisterWindowViewModel()
  {
    IObservable<bool> isInputValid = this.WhenAnyValue(
      x => x.RegisterUsername, x => x.RegisterPassword, (username, password) => 
      !string.IsNullOrWhiteSpace(username) && username.Length > 7 && 
      !string.IsNullOrWhiteSpace(password) && password.Length > 7
    );
    OpenStartCommand = ReactiveCommand.Create(() => {
      return new StartWindowViewModel();
    });
    AccountRegisterCommand = ReactiveCommand.CreateFromObservable(_accountRegisterCommand, isInputValid);
  }
}
