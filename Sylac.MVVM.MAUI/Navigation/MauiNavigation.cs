using Sylac.MVVM.Core.Navigation.Abstractions;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Sylac.MVVM.MAUI.Navigation
{
    public class MauiNavigation : IPlatformNavigation
    {
        public void RegisterPage<TPage>() where TPage : INavigationablePage
            => Routing.RegisterRoute(typeof(TPage).Name, typeof(TPage));

        public IObservable<Unit> GoTo(string route) => Shell.Current
            .GoToAsync(route)
            .ToObservable();

        public IObservable<Unit> GoTo(string route, IDictionary<string, object> parameters) => Shell.Current
            .GoToAsync(route, parameters)
            .ToObservable();
    }
}
