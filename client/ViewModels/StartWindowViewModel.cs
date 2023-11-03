using ReactiveUI;
using System.Reactive;

namespace client.ViewModels
{
    public class StartWindowViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, MainWindowViewModel> OpenMainCommand { get; }
        public ReactiveCommand<Unit, RegisterWindowViewModel> OpenRegisterCommand { get; }
        public StartWindowViewModel()
        {
            OpenMainCommand = ReactiveCommand.Create(() => new MainWindowViewModel());
            OpenRegisterCommand = ReactiveCommand.Create(() => new RegisterWindowViewModel());
        }
    }
}