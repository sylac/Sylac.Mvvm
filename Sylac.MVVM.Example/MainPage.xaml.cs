using Sylac.MVVM.Example.ViewModels;

namespace Sylac.MVVM.Example
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
