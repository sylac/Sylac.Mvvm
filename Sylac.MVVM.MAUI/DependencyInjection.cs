using Sylac.Mvvm.Core.Navigation;
using Sylac.Mvvm.Core.Navigation.Abstractions;
using Sylac.Mvvm.Maui.Navigation;

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
