using Sylac.Mvvm.Core.Navigation.Abstractions;

namespace Sylac.Mvvm.Example
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();

            MainPage = new AppShell();
            RegisterRoutes(navigationService);
        }
    }
}
