using Microsoft.Extensions.Logging;
using Sylac.Mvvm.Example.ViewModels;
using Sylac.Mvvm.Maui;

namespace Sylac.Mvvm.Example
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services
                .UseSylacMaui()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        static IServiceCollection RegisterViews(this IServiceCollection services)
        {
            return services
                .AddTransient<MainPage>()
                .AddTransient<MainPageViewModel>()
                .AddTransient<ExamplePage>()
                .AddTransient<ExamplePageViewModel>();
        }
    }
}
