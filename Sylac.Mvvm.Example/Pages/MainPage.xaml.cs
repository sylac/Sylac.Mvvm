using Sylac.Mvvm.Example.ViewModels;
using Sylac.Mvvm.Maui.Controls;

namespace Sylac.Mvvm.Example.Pages;

public partial class MainPage : NavigationablePage
{
    public MainPage(MainPageViewModel mainPageViewModel)
        : base(mainPageViewModel)
    {
        InitializeComponent();
    }
}
