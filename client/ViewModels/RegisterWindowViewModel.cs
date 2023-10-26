using client.Models;
using client.Services;
using ReactiveUI;
using System.Reactive;

namespace client.ViewModels;

public class RegisterWindowViewModel : ViewModelBase
{
    public ReactiveCommand<bool, Unit> AccountRegisterCommand { get; }
    public ReactiveCommand<bool, Unit> RegisterUsername { get; set;}
    public ReactiveCommand<bool, Unit> RegisterPassword { get; set;}
    public RegisterWindowViewModel(Database db)
    {
        AccountRegisterCommand = ReactiveCommand.Create<bool>(!db.VerifyAccount(new Account(username, password)) => \
        db.RegisterNewAccount(username, password));
    }
}
