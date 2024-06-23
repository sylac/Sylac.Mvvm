using System.Reactive;

namespace Sylac.MVVM.Core.Navigation.Abstractions
{
    public interface IPlatformNavigation
    {
        void RegisterPage<TPage>() where TPage : INavigationablePage;
        IObservable<Unit> GoTo(string route);
        IObservable<Unit> GoTo(string route, IDictionary<string, object> parameters);
    }
}
