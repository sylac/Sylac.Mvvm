using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sylac.Mvvm.Navigation.Abstractions;
using System.Reactive;

namespace Sylac.Mvvm.Example.ViewModels
{
    public sealed class MainPageViewModel : ReactiveObject
    {
        [Reactive]
        public string EnteredText { get; set; } = "";
        public ReactiveCommand<Unit, Unit> ButtonCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
        {
            ButtonCommand = ReactiveCommand.CreateFromObservable(() => navigationService
                .NavigateTo<ExamplePageViewModel, ExamplePageViewModelParameters>(new(EnteredText)));
        }
    }
}
