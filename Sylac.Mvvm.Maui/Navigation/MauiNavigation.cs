using Sylac.Mvvm.Navigation.Abstractions;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Sylac.Mvvm.Maui.Navigation
{
    public class MauiNavigation : IPlatformNavigation
    {
        public SynchronizationContext SynchronizationContext
        {
            get
            {
                try
                {
                    return MainThread.GetMainThreadSynchronizationContextAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to get the main thread synchronization context.", ex);
                }
            }
        }

        public void RegisterPage<TPage>() where TPage : INavigationablePage
            => Routing.RegisterRoute(typeof(TPage).Name, typeof(TPage));

        public IObservable<Unit> GoTo(string route) => Shell.Current
            .GoToAsync(route)
            .ToObservable();

        public IObservable<Unit> GoTo(string route, IDictionary<string, object> parameters) => Shell.Current
            .GoToAsync(route, parameters)
            .ToObservable();

        public IObservable<Unit> GoBack() => Shell.Current
            .GoToAsync("..")
            .ToObservable();
    }
}
