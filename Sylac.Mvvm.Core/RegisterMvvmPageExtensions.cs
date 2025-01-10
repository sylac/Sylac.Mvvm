using Microsoft.Extensions.DependencyInjection;
using Sylac.Mvvm.Abstraction;
using Sylac.Mvvm.Navigation.Abstractions;

namespace Sylac.Mvvm;

public static class RegisterMvvmPageExtensions
{
    public static IServiceCollection RegisterMvvmPage<TPage, TViewModel>(
        this IServiceCollection services,
        ServiceLifetime pageLifetime = ServiceLifetime.Transient,
        ServiceLifetime viewModelLifetime = ServiceLifetime.Transient)
        where TPage : class, INavigationablePage
        where TViewModel : class, IViewModel<IViewModelParameters>
    {
        services.Add(new(typeof(TPage), null, typeof(TPage), pageLifetime));
        services.Add(new(typeof(TViewModel), null, typeof(TViewModel), viewModelLifetime));
        services.BuildServiceProvider()
            .GetRequiredService<INavigationService>()
            .RegisterNavigationView<TPage, TViewModel>();

        return services;
    }
}
