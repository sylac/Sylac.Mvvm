using System.Reactive;

namespace Sylac.Mvvm.Core.Navigation.Abstractions;

public interface INavigationService
{
    void RegisterNavigationView<TPage, TViewModel, TParams>()
        where TPage : INavigationablePage
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters;

    IObservable<Unit> NavigateTo<TViewModel, TParams>(TParams parameters)
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters;

    IObservable<Unit> NavigateBack();
}
