using Sylac.MVVM.Core.Navigation.Abstractions;
using System.Reactive;
using System.Reactive.Linq;

namespace Sylac.MVVM.Core.Navigation;

public class NavigationService(IPlatformNavigation platformNavigation) : INavigationService
{
    private readonly IPlatformNavigation _platformNavigation = platformNavigation;
    internal static Dictionary<Type, (string Page, Type ParametersType)> ViewModelsRegistry { get; } = [];

    public void RegisterNavigationView<TPage, TViewModel, TParams>()
        where TPage : INavigationablePage
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters
    {
        if (ViewModelsRegistry.TryAdd(typeof(TViewModel), (typeof(TPage).Name, typeof(TParams))))
        {
            _platformNavigation.RegisterPage<TPage>();
        }
    }

    public IObservable<Unit> NavigateTo<TViewModel, TParams>(TParams parameters)
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters
    {
        return Observable.Return(ViewModelsRegistry.GetValueOrDefault(typeof(TViewModel)))
            .Where(result =>
                result.Page != default &&
                result.ParametersType != default)
            .SelectMany(result =>
            {
                // is type check needed? Is it possible to pass a different type?
                if (parameters.GetType() != result.ParametersType)
                {
                    return Observable.Throw<Unit>(new ArgumentException("Invalid parameters type"));
                }

                return _platformNavigation.GoTo(result.Page, new Dictionary<string, object>() { { INavigationablePage.ParametersKey, parameters } });
            })
            .IsEmpty()
            .SelectMany(isEmpty =>
            {
                if (isEmpty)
                {
                    return Observable.Throw<Unit>(new ArgumentException("Error navigating to page"));
                }
                return Observable.Return(Unit.Default);
            });
    }

    public IObservable<Unit> NavigateBack()
    {
        return _platformNavigation.GoTo("..");
    }
}

