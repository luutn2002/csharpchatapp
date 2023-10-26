using ReactiveUI;
using System.Reactive;
using client.Services;

namespace client.ViewModels
{
    public class StartWindowViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, MainWindowViewModel> OpenMainCommand { get; }
        public ReactiveCommand<Unit, RegisterWindowViewModel> OpenRegisterCommand { get; }
        public StartWindowViewModel(Database db)
        {
            OpenMainCommand = ReactiveCommand.Create(() => new MainWindowViewModel());
            OpenRegisterCommand = ReactiveCommand.Create(() => new RegisterWindowViewModel(db));
        }
    }
}