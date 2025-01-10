using NSubstitute;
using Sylac.Mvvm.Abstraction;
using Sylac.Mvvm.Navigation;
using Sylac.Mvvm.Navigation.Abstractions;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Sylac.Mvvm.UnitTests
{
    public class NavigationServiceTests
    {
        [Fact]
        public void RegisterThreeGenericParameters_ShouldRegisterNavigationService()
        {
            // Arrange
            var shellWrapper = Substitute.For<IPlatformNavigation>();
            var navigationService = new NavigationService(shellWrapper);

            // Act
            navigationService.RegisterNavigationView<TestPage, TestViewModel, TestViewModelParameters>();

            // Assert
            Assert.True(NavigationService.ViewModelsRegistry.ContainsKey(typeof(TestViewModel)));
            Assert.Equal(nameof(TestPage), NavigationService.ViewModelsRegistry[typeof(TestViewModel)].Page);
            Assert.Equal(typeof(TestViewModelParameters), NavigationService.ViewModelsRegistry[typeof(TestViewModel)].ParametersType);

            NavigationService.ViewModelsRegistry.Clear();
        }

        [Fact]
        public void RegisterTwoGenericParameters_ShouldRegisterNavigationService()
        {
            // Arrange
            var shellWrapper = Substitute.For<IPlatformNavigation>();
            var navigationService = new NavigationService(shellWrapper);

            // Act
            navigationService.RegisterNavigationView<TestPage, TestViewModel>();

            // Assert
            Assert.True(NavigationService.ViewModelsRegistry.ContainsKey(typeof(TestViewModel)));
            Assert.Equal(nameof(TestPage), NavigationService.ViewModelsRegistry[typeof(TestViewModel)].Page);
            Assert.Equal(typeof(TestViewModelParameters), NavigationService.ViewModelsRegistry[typeof(TestViewModel)].ParametersType);

            NavigationService.ViewModelsRegistry.Clear();
        }

        [Fact]
        public async Task NavigateTo_ShouldNavigateToPage()
        {
            // Arrange
            var shellWrapper = Substitute.For<IPlatformNavigation>();
            var navigationService = new NavigationService(shellWrapper);
            navigationService.RegisterNavigationView<TestPage, TestViewModel, TestViewModelParameters>();

            // Act
            var result = await navigationService
                .NavigateTo<TestViewModel, TestViewModelParameters>(new("TestString"))
                .ToTask();

            // Assert
            Assert.Equal(Unit.Default, result);
            await shellWrapper
                .Received()
                .GoTo(nameof(TestPage), Arg.Any<Dictionary<string, object>>())
                .ToTask();

            NavigationService.ViewModelsRegistry.Clear();
        }

        [Fact]
        public async Task NavigateTo_ShouldThrowArgumentException_WhenViewModelIsNotRegistered()
        {
            // Arrange
            var shellWrapper = Substitute.For<IPlatformNavigation>();
            var navigationService = new NavigationService(shellWrapper);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => navigationService
                    .NavigateTo<TestViewModel2, TestViewModelParameters2>(new("TestString"))
                    .ToTask());

            NavigationService.ViewModelsRegistry.Clear();
        }

        [Fact]
        public async Task NavigateTo_ShouldThrowArgumentException_IfParametersTypeIsInvalid()
        {
            // Arrange
            var shellWrapper = Substitute.For<IPlatformNavigation>();
            var navigationService = new NavigationService(shellWrapper);

            if (!NavigationService.ViewModelsRegistry.TryAdd(typeof(TestViewModel), ("TestPage", typeof(TestViewModelParameters2))))
            {
                Assert.Fail("Failed to add TestViewModel to ViewModelsRegistry");
            }

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => navigationService
                    .NavigateTo<TestViewModel, TestViewModelParameters>(new("TestString"))
                    .ToTask());

            NavigationService.ViewModelsRegistry.Clear();
        }
        [Fact]
        public async Task NavigateTo_ShouldThrowArgumentException_IfNavigationFails()
        {
            // Arrange
            var shellWrapper = Substitute.For<IPlatformNavigation>();
            var navigationService = new NavigationService(shellWrapper);
            navigationService.RegisterNavigationView<TestPage, TestViewModel, TestViewModelParameters>();

            shellWrapper.GoTo(nameof(TestPage), Arg.Any<Dictionary<string, object>>())
                .Returns(Observable.Throw<Unit>(new Exception("Error navigating to page")));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => navigationService
                    .NavigateTo<TestViewModel, TestViewModelParameters>(new("TestString"))
                    .ToTask());

            NavigationService.ViewModelsRegistry.Clear();
        }

        [Fact]
        public async Task NavigateBack_ShouldNavigateBack()
        {
            // Arrange
            var shellWrapper = Substitute.For<IPlatformNavigation>();
            var navigationService = new NavigationService(shellWrapper);

            // Act
            var result = await navigationService.NavigateBack().ToTask();

            // Assert
            Assert.Equal(Unit.Default, result);
            await shellWrapper.Received().GoTo("..").ToTask();
        }

        private record TestViewModelParameters(string TestString) : IViewModelParameters;
        private class TestViewModel : IViewModel<TestViewModelParameters>
        {
            public TestViewModelParameters? Parameters { get; } = null;

            public void OnNavigatedFrom() { }
            public void OnNavigatedTo() { }
            public void OnNavigatingFrom() { }
            public void Initialize(IViewModelParameters parameter) => throw new NotImplementedException();

            public void OnNavigatingTo() { }
        }

        private record TestViewModelParameters2(string TestString) : IViewModelParameters;
        private class TestViewModel2 : IViewModel<TestViewModelParameters2>
        {
            public TestViewModelParameters2? Parameters { get; } = null;

            public void OnNavigatedFrom() { }
            public void OnNavigatedTo() { }
            public void OnNavigatingFrom() { }
            public void Initialize(IViewModelParameters parameter) => throw new NotImplementedException();

            public void OnNavigatingTo() { }
        }

        private class TestPage : INavigationablePage
        {
            public const string ParametersKey = "Parameters";
        }
    }
}