using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using ReactiveUI;

namespace client.ViewModels;

public class ViewModelBase : ReactiveObject
{
  public string _serverMessage = string.Empty;
  public string _username = string.Empty;
  public string _password = string.Empty;
  public string _action = string.Empty;

  public string ServerAddress = "https://localhost:7259";

  public async Task<Reply> _grpcCommand(){
    using var channel = GrpcChannel.ForAddress(ServerAddress);
    var client = new UserCredentialSender.UserCredentialSenderClient(channel);
    var reply = await client.SendUserCredentialsAsync(
      new UserCredentials { 
        Username = _username, 
        Password = _password,
        Action = _action
    }, deadline: DateTime.UtcNow.AddSeconds(5));
    return reply;
  }
}