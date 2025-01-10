using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sylac.Mvvm.Abstraction;
using Sylac.Mvvm.Navigation.Abstractions;

namespace Sylac.Mvvm.Example.ViewModels;

/// <summary>
/// The parameters required to initialize the <see cref="ExamplePageViewModel"/>.
/// </summary>
public sealed record ExamplePageViewModelParameters(string InitialText) : IViewModelParameters;

/// <summary>
/// Implementation of the example page view model.
/// </summary>
public sealed class ExamplePageViewModel
    : ViewModelBase<ExamplePageViewModelParameters>
{
    private INavigationService NavigationService { get; }

    [Reactive]
    public string EnteredText { get; set; } = string.Empty;

    public ReactiveCommand<Unit, Unit> ShowSecondExamplePageCommand { get; }

    public ExamplePageViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        ShowSecondExamplePageCommand = ReactiveCommand.CreateFromObservable(() => NavigationService
            .NavigateTo<SecondExamplePageViewModel, SecondExamplePageViewModelParameters>(new())
            .Catch((Exception exception) => Observable.Return(Unit.Default)));
    }

    public override void OnLoadedParameters(ExamplePageViewModelParameters parameters) => EnteredText = parameters.InitialText;
    public override void OnNavigatedTo() => EnteredText = "Navigated to ExamplePageViewModel";
}
