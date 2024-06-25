using Sylac.Mvvm.Maui.Navigation;
using Sylac.Mvvm.Navigation;
using Sylac.Mvvm.Navigation.Abstractions;

namespace Sylac.Mvvm.Maui;

public static class DependencyInjection
{
    public static IServiceCollection UseSylacMaui(this IServiceCollection services)
    {
        return services
            .AddTransient<IPlatformNavigation, MauiNavigation>()
            .AddTransient<INavigationService, NavigationService>();
    }
}
