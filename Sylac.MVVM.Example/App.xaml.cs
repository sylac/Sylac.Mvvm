using Sylac.MVVM.Core.Navigation.Abstractions;

namespace Sylac.MVVM.Example
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
