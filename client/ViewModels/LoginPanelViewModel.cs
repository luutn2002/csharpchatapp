using Grpc.Core;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace client.ViewModels
{
    /// <summary>
    /// View model for login panel.
    /// </summary>
    public class LoginPanelViewModel : ViewModelBase
    {
        public bool _isShowingLoginPanel;
        public bool IsShowingLoginPanel
        {
            get => _isShowingLoginPanel;
            set => this.RaiseAndSetIfChanged(ref _isShowingLoginPanel, value);
        }
        public string LoginUsername
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string LoginPassword
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        public string LoginAction
        {
            get => _action;
            set => this.RaiseAndSetIfChanged(ref _action, value);
        }
        public string LoginServerMessage
        {
            get => _serverMessage;
            set => this.RaiseAndSetIfChanged(ref _serverMessage, value);
        }
        public ReactiveCommand<Unit, Task> AccountLoginCommand { get; private set;}

        /// <summary>
        /// An observable command to send "login" message to server including username and password.
        /// </summary>
        /// <returns>A Task.CompletedTask attribute.</returns>
        private IObservable<Task> _accountLoginCommand()
        {
            return Observable.Start(() => {
                try
                {
                    LoginServerMessage = "Connecting to server...";
                    var reply = _grpcCommand().Result;
                    LoginServerMessage = reply.Message ? $"Successfully {LoginAction} user." : $"Failed to {LoginAction}, please try again.";
                }
                catch (AggregateException e) {
                    var inner = e.InnerException;
                    if (inner is RpcException casted) {
                        LoginServerMessage = $"Cannot connect to server, \n status code: {casted.StatusCode}";
                    }
                }           
                return Task.CompletedTask;
            });
        }
        /// <summary>
        /// Constructor for login panel view model.
        /// </summary>
        public LoginPanelViewModel()
        {
            LoginAction = "login";
            IObservable<bool> isInputValid = this.WhenAnyValue(
            x => x.LoginUsername, x => x.LoginPassword, x => x.LoginAction, (username, password, action) => 
            !string.IsNullOrWhiteSpace(username) && username.Length > 7 && 
            !string.IsNullOrWhiteSpace(password) && password.Length > 7 && action == "login"
            );
            AccountLoginCommand = ReactiveCommand.CreateFromObservable(_accountLoginCommand, isInputValid);
        }
    }
}