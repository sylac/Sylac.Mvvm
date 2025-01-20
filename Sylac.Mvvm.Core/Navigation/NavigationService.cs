using Sylac.Mvvm.Abstraction;
using Sylac.Mvvm.Navigation.Abstractions;
using System.Reactive;
using System.Reactive.Linq;

namespace Sylac.Mvvm.Navigation;

public sealed class NavigationService(IPlatformNavigation platformNavigation) : INavigationService
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

    public void RegisterNavigationView<TPage, TViewModel>()
        where TPage : INavigationablePage
        where TViewModel : IViewModel<IViewModelParameters>
    {
        var parametersType = typeof(TViewModel)
            .FindInterfaces((type, _) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IViewModel<>), null)
            .First()
            .GetGenericArguments()
            .First();

        if (ViewModelsRegistry.TryAdd(typeof(TViewModel), (typeof(TPage).Name, parametersType)))
        {
            _platformNavigation.RegisterPage<TPage>();
        }
    }

    public IObservable<Unit> NavigateTo<TViewModel, TParams>(TParams parameters)
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters
    {
        return Observable.Return(ViewModelsRegistry.GetValueOrDefault(typeof(TViewModel)))
            .SubscribeOn(_platformNavigation.SynchronizationContext)
            .Where(result => result != default)
            .SelectMany(result =>
                // is type check needed? Is it possible to pass a different type?
                parameters.GetType() != result.ParametersType
                    ? Observable.Throw<Unit>(new ArgumentException("Invalid parameters type"))
                    : _platformNavigation.GoTo(result.Page, new Dictionary<string, object>() { { INavigationablePage.ParametersKey, parameters } }))
            .IsEmpty()
            .SelectMany(isEmpty => isEmpty
                ? Observable.Throw<Unit>(new ArgumentException("Error navigating to page"))
                : Observable.Return(Unit.Default));
    }

    public IObservable<Unit> NavigateBack() => Observable.Return(Unit.Default)
        .SubscribeOn(_platformNavigation.SynchronizationContext)
        .SelectMany(_ => _platformNavigation.GoBack());
}

