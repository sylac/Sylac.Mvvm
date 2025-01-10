using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Sylac.Mvvm.Abstraction;
using Sylac.Mvvm.Navigation.Abstractions;

namespace Sylac.Mvvm.UnitTests
{
    public class RegisterMvvmPageExtensionsTests
    {
        [Fact]
        public void RegisterMvvmPage_WithTransientLifetime_ShouldRegisterPageAndViewModel()
        {
            // Arrange
            var services = new ServiceCollection();
            var navigationService = Substitute.For<INavigationService>();

            services.AddSingleton(navigationService);

            // Act
            services.RegisterMvvmPage<ExamplePage, ExamplePageViewModel>();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var page = serviceProvider.GetRequiredService<ExamplePage>();
            var viewModel = serviceProvider.GetRequiredService<ExamplePageViewModel>();

            Assert.NotNull(page);
            Assert.NotNull(viewModel);
            navigationService
                .Received()
                .RegisterNavigationView<ExamplePage, ExamplePageViewModel>();
        }

        private class ExamplePage : INavigationablePage { }
        private record ExamplePageParameters : IViewModelParameters;
        private class ExamplePageViewModel : IViewModel<ExamplePageParameters>
        {
            public void OnLoadedParameters(IViewModelParameters parameters) { }
            public void OnNavigatedFrom() { }
            public void OnNavigatedTo() { }
            public void OnNavigatingFrom() { }
            public void Initialize(IViewModelParameters parameter) => throw new NotImplementedException();

            public void OnNavigatingTo() { }
        }
    }
}
