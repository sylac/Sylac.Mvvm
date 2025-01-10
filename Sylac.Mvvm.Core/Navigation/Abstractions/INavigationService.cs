using Sylac.Mvvm.Abstraction;
using System.Reactive;

namespace Sylac.Mvvm.Navigation.Abstractions;

public interface INavigationService
{
    void RegisterNavigationView<TPage, TViewModel, TParams>()
        where TPage : INavigationablePage
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters;

    public void RegisterNavigationView<TPage, TViewModel>()
        where TPage : INavigationablePage
        where TViewModel : IViewModel<IViewModelParameters>;

    IObservable<Unit> NavigateTo<TViewModel, TParams>(TParams parameters)
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters;

    IObservable<Unit> NavigateBack();
}
