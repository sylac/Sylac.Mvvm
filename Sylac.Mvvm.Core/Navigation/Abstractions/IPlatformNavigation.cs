using System.Reactive;

namespace Sylac.Mvvm.Navigation.Abstractions
{
    public interface IPlatformNavigation
    {
        SynchronizationContext SynchronizationContext { get; }

        void RegisterPage<TPage>() where TPage : INavigationablePage;
        IObservable<Unit> GoTo(string route);
        IObservable<Unit> GoTo(string route, IDictionary<string, object> parameters);
        IObservable<Unit> GoBack();
    }
}
