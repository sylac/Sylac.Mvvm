using Sylac.MVVM.Core.Navigation;
using Sylac.MVVM.Core.Navigation.Abstractions;
using Sylac.MVVM.MAUI.Navigation;

namespace Sylac.MVVM.MAUI;

public static class DependencyInjection
{
    public static IServiceCollection UseSylacMAUI(this IServiceCollection services)
    {
        return services
            .AddTransient<IPlatformNavigation, MauiNavigation>()
            .AddTransient<INavigationService, NavigationService>();
    }
}
