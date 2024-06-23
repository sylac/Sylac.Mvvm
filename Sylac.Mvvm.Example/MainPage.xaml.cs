using Sylac.Mvvm.Example.ViewModels;

namespace Sylac.Mvvm.Example
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }
    }

}
