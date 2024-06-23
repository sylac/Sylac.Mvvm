using Sylac.MVVM.MAUI.Navigation;
using Sylac.MVVM.Navigation;
using Sylac.MVVM.Navigation.Abstractions;

namespace Sylac.MVVM.MAUI
{
    public static class DependencyInjection
    {
        public static IServiceCollection UseSylacMAUI(this IServiceCollection services)
        {
            return services
                .AddTransient<IPlatformNavigation, MauiNavigation>()
                .AddTransient<INavigationService, NavigationService>();
        }
    }
}
